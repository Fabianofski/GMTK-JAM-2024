// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 17.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections.Generic;
using System.Linq;
using F4B1.UI;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Tilemaps;
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
        [Header("Blueprint")]
        [SerializeField] private PlantResources[] neededBlueprintResources = { };
        [SerializeField] private Sprite blueprintSprite;
        private bool blueprint = true;
        private bool preview = false;
        private SpriteRenderer spriteRenderer;
        private Sprite defaultSprite;
        
        [Header("Production")] 
        [SerializeField] private PlantResources[] neededResources = { };
        [SerializeField] private GameObject uiNeededResourcePrefab;
        [SerializeField] private Transform uiNeededResourceParent;
        [SerializeField] private float productionTime = 1;
        private float timer;
        [SerializeField] private bool plantHasEnoughResources;
        [SerializeField] private int produceAmount = 1;
        [SerializeField] private Image progress;
        [SerializeField] private BoolVariable gamePaused;
        
        [Header("Connections")]
        [SerializeField] private TileBase connectionTile;
        private bool plantIsConnected;
        private readonly List<Vector2> connections = new();
        private Tilemap connectionMap;

        [Header("Output")] 
        [SerializeField] private int stored = 20;
        [SerializeField] private int capacity = 30;
        [SerializeField] private string resourceId;
        [SerializeField] private TextMeshProUGUI capacityText;
        [SerializeField] private TextMeshProUGUI storedText;
        [SerializeField] private Color gold;

        public int GetStoredAmount() => stored;


        public void PreviewMode()
        {
            preview = true;
        }
        
        private void Start()
        {
            UpdateText();

            spriteRenderer = GetComponent<SpriteRenderer>();
            defaultSprite = spriteRenderer.sprite;
            spriteRenderer.sprite = blueprint ? blueprintSprite : defaultSprite;

            var tilemapGo = GameObject.FindGameObjectWithTag("ConnectionMap");
            connectionMap = tilemapGo.GetComponent<Tilemap>();
            
            CreateNeededResourceUI(blueprint ? neededBlueprintResources : neededResources);

            if (preview)
            {
                gameObject.layer = 2;
                if (TryGetComponent<Factory>(out var factory))
                    Destroy(factory);
                Destroy(this);
            }

            timer = productionTime;
            CheckResources();
        }

        private void CreateNeededResourceUI(PlantResources[] resources)
        {
            for (var i = 0; i < uiNeededResourceParent.childCount; i++)
               Destroy(uiNeededResourceParent.GetChild(i).gameObject);
            
            foreach (var neededResource in resources)
            {
                var go = Instantiate(uiNeededResourcePrefab, uiNeededResourceParent);
                neededResource.ui = go.GetComponentInChildren<UINeededResource>();
                neededResource.UpdateUI();
            }
        }

        private void UpdateText()
        {
            storedText.text = $"{stored}"; 
            storedText.color = stored == capacity ? gold : Color.white;
            capacityText.text = $"/{capacity}";
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var waggon = other.transform.parent.GetComponentInChildren<Waggon>();
            if (!waggon) return;

            EmptyWaggon(waggon);
            FillWaggon(waggon);
        }

        private void Update()
        {
            if (gamePaused.Value) return;
            
            if (stored == capacity || !plantIsConnected || blueprint)
            {
                timer = productionTime;
                progress.fillAmount = 0;
                return;
            }
            
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
            if (blueprint)
            {
                var blueprintHasEnoughResources = neededBlueprintResources.All(resource => resource.neededAmount <= resource.storedAmount);
                if (blueprintHasEnoughResources) ExitBlueprintState();
            }
            else 
                plantHasEnoughResources = neededResources.All(resource => resource.neededAmount <= resource.storedAmount);
        }

        private void ExitBlueprintState()
        {
            blueprint = false;
            spriteRenderer.sprite = defaultSprite;
            CreateNeededResourceUI(neededResources);
            CheckResources();
        }

        private void EmptyWaggon(Waggon waggon)
        {
            var waggonResourceId = waggon.GetResourceId();
            var resources = blueprint ? neededBlueprintResources : neededResources;
            var plantResource = resources.FirstOrDefault(x => x.id == waggonResourceId);
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

        public void ClearStorage()
        {
            stored = 0;
            UpdateText();
        }

        public void SetConnection(Vector2 cell, bool connect)
        {
            if (connect) 
                connections.Add(cell);
            else
                connections.Remove(cell);
            connectionMap.SetTile(Vector3Int.RoundToInt(cell), connect ? connectionTile : null);
            plantIsConnected = connections.Count > 0;
        }
    }
}