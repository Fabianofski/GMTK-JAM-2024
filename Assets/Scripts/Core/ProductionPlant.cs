// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 17.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Linq;
using F4B1.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            ui.UpdateNeededResourceUI(storedAmount, neededAmount, capacity, id);
        }
    }

    public class ProductionPlant : MonoBehaviour
    {
        [Header("Production")] 
        [SerializeField] private PlantResources[] neededResources = { };
        [SerializeField] private GameObject uiNeededResourcePrefab;
        [SerializeField] private Transform uiNeededResourceParent;
        [SerializeField] private float productionTime = 1;
        private float timer;
        [SerializeField] private bool plantHasEnoughResources;
        [SerializeField] private int produceAmount = 1;
        [SerializeField] private Image progress;

        [Header("Output")] 
        [SerializeField] private int stored = 20;
        [SerializeField] private int capacity = 30;
        [SerializeField] private string resourceId;
        [SerializeField] private TextMeshProUGUI capacityText;
        [SerializeField] private TextMeshProUGUI storedText;
        [SerializeField] private Color gold;

        private void Start()
        {
            UpdateText();

            foreach (var neededResource in neededResources)
            {
                var go = Instantiate(uiNeededResourcePrefab, uiNeededResourceParent);
                neededResource.ui = go.GetComponentInChildren<UINeededResource>();
                neededResource.UpdateUI();
            }

            timer = productionTime;
            CheckResources();
        }

        private void UpdateText()
        {
            storedText.text = $"{stored}"; 
            storedText.color = stored == capacity ? gold : Color.white;
            capacityText.text = $"/{capacity}";
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var waggon = other.GetComponentInChildren<Waggon>();
            if (!waggon) return;

            EmptyWaggon(waggon);
            FillWaggon(waggon);
        }

        private void Update()
        {
            if (stored == capacity) return;
            
            if (timer <= 0)
            {
                timer = productionTime;
                progress.fillAmount = 1 - timer / productionTime;
                ProduceItem();
            } 
            else if (plantHasEnoughResources)
            {
                timer -= Time.deltaTime;
                progress.fillAmount = 1 - timer / productionTime;
            }
        }

        private void ProduceItem()
        {
            foreach (var resource in neededResources)
            {
                resource.storedAmount -= resource.neededAmount;
                resource.UpdateUI();
            }

            stored += produceAmount;
            stored = Mathf.Min(stored, capacity);
            UpdateText();
            
            CheckResources();
        }

        private void CheckResources()
        {
            plantHasEnoughResources = neededResources.All(resource => resource.neededAmount <= resource.storedAmount);
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

            CheckResources();
        }

        private void FillWaggon(Waggon waggon)
        {
            stored -= waggon.Fill(resourceId, stored);
            UpdateText();
        }
    }
}