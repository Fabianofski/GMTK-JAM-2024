// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 19.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.Linq;
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

        public void LeftClick(bool isPressed)
        {
            if (!isPressed || selectedItem.Value != "blueprints" || IsPointerOverUI()) return;

            var blueprint = GetGameObjectByName(selectedBlueprint.Value);
            if (!blueprint) return;

            var roundedPos = Vector3Int.RoundToInt(mousePos.Value);
            var tilePos = new Vector3(roundedPos.x + 0.5f, roundedPos.y + 0.5f, 0);
            Instantiate(blueprint, tilePos, Quaternion.identity);
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