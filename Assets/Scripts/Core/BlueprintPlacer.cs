// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 19.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Linq;
using F4B1.Audio;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.EventSystems;

namespace F4B1.Core
{
    public class BlueprintPlacer : MonoBehaviour
    {
        [SerializeField] private StringVariable selectedItem;
        [SerializeField] private StringVariable selectedBlueprint;
        [SerializeField] private Vector2Variable mousePos;
        [SerializeField] private GameObject[] blueprints;

        [SerializeField] private Vector2Variable previewPos;
        [SerializeField] private GameObjectVariable previewGo;
        [SerializeField] private BoolVariable previewValid;

        [SerializeField] private SoundEvent soundEvent;
        [SerializeField] private Sound placeSound;

        [SerializeField] private LayerMask mask;
        private void Update()
        {
            if (selectedItem.Value != "blueprints") return;
            
            var blueprint = GetGameObjectByName(selectedBlueprint.Value);
            if (!blueprint) return;

            var roundedPos = Vector3Int.RoundToInt(mousePos.Value);
            var tilePos = new Vector3(roundedPos.x + 0.5f, roundedPos.y - 0.5f, 0);

            var hit = Physics2D.BoxCast(tilePos, Vector2.one * 1.8f, 0, Vector2.zero, 0f, mask.value);

            previewValid.SetValue(!hit);
            previewPos.SetValue(tilePos);
            previewGo.SetValue(blueprint);
        }

        public void LeftClick(bool isPressed)
        {
            if (!isPressed || selectedItem.Value != "blueprints" && IsPointerOverUI()) return;

            var blueprint = GetGameObjectByName(selectedBlueprint.Value);
            if (!blueprint) return;

            var roundedPos = Vector3Int.RoundToInt(mousePos.Value);
            var tilePos = new Vector3(roundedPos.x + 0.5f, roundedPos.y - 0.5f, 0);

            var hit = Physics2D.BoxCast(tilePos, Vector2.one, 0, Vector2.zero, 0f, mask.value);
            if (hit) return;
            
            Instantiate(blueprint, tilePos, Quaternion.identity);
            soundEvent.Raise(placeSound);

            selectedBlueprint.SetValue("none");
            selectedItem.SetValue("none");
            previewGo.Reset();
        }

        public void RightClick(bool isPressed)
        {
            if (!isPressed || selectedItem.Value != "blueprints") return;
            
            selectedBlueprint.SetValue("none");
            selectedItem.SetValue("none");
            previewGo.Reset();
        }

        private GameObject GetGameObjectByName(string goName)
        {
            return blueprints.FirstOrDefault(blueprint => blueprint.name == goName);
        }

        private bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}