// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 16.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
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
        [Header("Tilemap")] [SerializeField] private Tilemap tilemap;
        [SerializeField] private Grid grid;
        [SerializeField] private RailTile[] railTiles;


        [Header("Mouse")] private Vector2 mouseWorldPos;
        private bool leftClicking;
        private bool rightClicking;

        private void Update()
        {
            if (leftClicking) PlaceTile();
            else if (rightClicking) RemoveTile();
        }

        private void PlaceTile()
        {
            var cell = grid.WorldToCell(mouseWorldPos);
            // if (tilemap.HasTile(cell)) return;
            var tile = GetCorrectTile(cell);
            tilemap.SetTile(cell, tile);
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
                
            var tile = railTiles.First(tile => tile.railId == id).tile;
            return tile;
        }

        private void RemoveTile()
        {
            var cell = grid.WorldToCell(mouseWorldPos);
            tilemap.SetTile(cell, null);
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

        public void OnRightClick(InputValue value)
        {
            rightClicking = value.isPressed;
        }
    }
}