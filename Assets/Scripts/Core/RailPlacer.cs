// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 16.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Linq;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace F4B1.Core
{
    [Serializable]
    public class RailTile
    {
        public string railId;
        public TileBase tile;
    }

    public class RailPlacer : MonoBehaviour
    {
        [SerializeField] private IntVariable railCount;
        [SerializeField] private StringVariable selectedItem;
        
        [Header("Tilemap")] [SerializeField] private Tilemap tilemap;
        [SerializeField] private Grid grid;
        [SerializeField] private RailTile[] railTiles;
        [SerializeField] private TileBase defaultTile;


        [Header("Mouse")] private Vector2 mouseWorldPos;
        private bool leftClicking;

        private void Update()
        {
            if (!leftClicking || IsPointerOverUI()) return;
            
            if (selectedItem.Value == "rails") PlaceTile();
            else if (selectedItem.Value == "bulldozer") RemoveTile();
        }

        private void PlaceTile()
        {
            if (railCount.Value <= 0) return;
            
            var cell = grid.WorldToCell(mouseWorldPos);
            if (tilemap.HasTile(cell)) return;
            var tile = GetCorrectTile(cell);
            tilemap.SetTile(cell, tile);
            UpdateSurroundingTiles(cell);
            
            railCount.Subtract(1);
        }

        private void UpdateSurroundingTiles(Vector3Int cell)
        {
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    var surroundedCell = new Vector3Int(cell.x + i, cell.y + j, 0);
                    if (!tilemap.HasTile(surroundedCell)) continue;
                    var tile = GetCorrectTile(surroundedCell);
                    tilemap.SetTile(surroundedCell, tile);
                }
            }
        }

        private TileBase GetCorrectTile(Vector3Int cell)
        {
            var up = tilemap.HasTile(cell + Vector3Int.up);
            var down = tilemap.HasTile(cell + Vector3Int.down);
            var right = tilemap.HasTile(cell + Vector3Int.right);
            var left = tilemap.HasTile(cell + Vector3Int.left);

            var id = "";
            if (!right && !left)
                id = "vertical";
            else if (!up && !down)
                id = "horizontal";
            else
            {
                id += up ? "u" : "";
                id += down ? "d" : "";
                id += right ? "r" : "";
                id += left ? "l" : "";
            }

            var result = railTiles.FirstOrDefault(tile => tile.railId == id);
            var tile = result != null ? result.tile : defaultTile;
            return tile;
        }

        private void RemoveTile()
        {
            var cell = grid.WorldToCell(mouseWorldPos);
            if (!tilemap.HasTile(cell)) return;
            tilemap.SetTile(cell, null);
            UpdateSurroundingTiles(cell);
            railCount.Add(1);
        }

        public void OnMouseMove(InputValue value)
        {
            var mousePos = value.Get<Vector2>();
            mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        }

        public void OnClick(InputValue value)
        {
            leftClicking = value.isPressed;
        }

        private bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}