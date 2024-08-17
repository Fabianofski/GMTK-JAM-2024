// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 17.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections;
using System.Linq;
using F4B1.UI;
using TMPro;
using UnityEngine;

namespace F4B1.Core
{
    [Serializable]
    public class PlantResources
    {
        public string id;
        public int neededAmount;
        public int storedAmount;
        public int capacity;
        [HideInInspector] public UINeededResource ui;

        public void UpdateUI()
        {
            ui.UpdateNeededResourceUI(storedAmount, capacity, id);
        }
    }

    public class ProductionPlant : MonoBehaviour
    {
        [Header("Production")] 
        [SerializeField] private PlantResources[] neededResources = { };
        [SerializeField] private GameObject uiNeededResourcePrefab;
        [SerializeField] private Transform uiNeededResourceParent;
        [SerializeField] private float productionTime = 1;
        [SerializeField] private int produceAmount = 1;
        private bool producing = false;

        [Header("Output")] [SerializeField] private int stored = 20;
        [SerializeField] private int capacity = 30;
        [SerializeField] private string resourceId;
        [SerializeField] private TextMeshProUGUI capacityText;

        private void Start()
        {
            StartCoroutine(Produce());
            capacityText.text = $"{stored}/{capacity}";

            foreach (var neededResource in neededResources)
            {
                var go = Instantiate(uiNeededResourcePrefab, uiNeededResourceParent);
                neededResource.ui = go.GetComponentInChildren<UINeededResource>();
                neededResource.UpdateUI();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var waggon = other.GetComponentInChildren<Waggon>();
            if (!waggon) return;

            EmptyWaggon(waggon);
            FillWaggon(waggon);
        }

        private IEnumerator Produce()
        {
            if (producing || stored >= capacity) yield break;
            
            foreach (var resource in neededResources)
                if (resource.neededAmount > resource.storedAmount)
                {
                    producing = false; 
                    yield break;
                }

            producing = true;
            foreach (var resource in neededResources)
            {
                resource.storedAmount -= resource.neededAmount;
                resource.UpdateUI();
            }

            stored += produceAmount;
            stored = Mathf.Min(stored, capacity);
            capacityText.text = $"{stored}/{capacity}";
            
            yield return new WaitForSeconds(productionTime);
            producing = false;
            StartCoroutine(Produce());
        }

        private void EmptyWaggon(Waggon waggon)
        {
            var waggonResourceId = waggon.GetResourceId();
            var plantResource = neededResources.FirstOrDefault(x => x.id == waggonResourceId);
            if (plantResource == null) return;

            var diff = plantResource.capacity - plantResource.storedAmount;
            diff = waggon.Empty(diff);

            plantResource.storedAmount += diff;
            plantResource.UpdateUI();

            StartCoroutine(Produce());
        }

        private void FillWaggon(Waggon waggon)
        {
            stored -= waggon.Fill(resourceId, stored);
            capacityText.text = $"{stored}/{capacity}";
        }
    }
}