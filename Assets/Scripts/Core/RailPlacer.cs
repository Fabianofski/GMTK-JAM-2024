// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 16.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.EventSystems;
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
        [SerializeField] private VoidEvent railNetworkUpdated;
        [SerializeField] private Vector2ValueList intersections;

        [Header("Tilemap")] 
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Grid grid;
        [SerializeField] private RailTile[] railTiles;
        [SerializeField] private TileBase defaultTile;
        [SerializeField] private GameObject railRemover;
        private readonly Dictionary<Vector3Int, GameObject> railRemovers = new();
        [SerializeField] private LayerMask plantMask;

        [Header("Mouse")] 
        [SerializeField] private Vector2Variable mousePos;
        private bool leftClicking;

        private void Update()
        {
            if (!leftClicking || IsPointerOverUI()) return;

            if (selectedItem.Value == "rails") PlaceTile();
            else if (selectedItem.Value == "bulldozer") MarkTileAsRemoved();
        }

        private void PlaceTile()
        {
            if (railCount.Value <= 0) return;

            var cell = grid.WorldToCell(mousePos.Value);
            if (tilemap.HasTile(cell)) return;
            var tile = GetCorrectTile(cell);
            tilemap.SetTile(cell, tile);
            CheckForIntersections(cell);
            UpdateSurroundingTiles(cell);
            UpdateSurroundingPlants((Vector3)cell, true);

            railNetworkUpdated.Raise();
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
                    CheckForIntersections(surroundedCell);
                }
            }
        }

        private void UpdateSurroundingPlants(Vector2 cell, bool connect)
        {
            var plant = GetSurroundingPlant((Vector3)cell);
            if (plant)
                plant.SetConnection((Vector3)cell, connect);
        }

        private void CheckForIntersections(Vector3Int pos)
        {
            var possibleDirections = TrainNavigator.GetPossibleDirectionsList(tilemap, pos);
            var cell = new Vector2(pos.x, pos.y);
            if (possibleDirections.Length >= 3)
            {
                if (!intersections.Contains(cell)) 
                    intersections.Add(cell);
            }
            else if (intersections.Contains(cell))
                intersections.Remove(cell);
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

        private void MarkTileAsRemoved()
        {
            var cell = grid.WorldToCell(mousePos.Value);
            if (!tilemap.HasTile(cell) || railRemovers.ContainsKey(cell)) return;
            var remover = Instantiate(railRemover, cell, Quaternion.identity, transform);
            railRemovers.Add(cell, remover);
        }

        public void RemoveTile(Vector2 position)
        {
            var cell = Vector3Int.RoundToInt(position);
            tilemap.SetTile(cell, null);
            if (intersections.Contains(position)) intersections.Remove(position);
            UpdateSurroundingTiles(cell);
            railCount.Add(1);
            railNetworkUpdated.Raise();
            railRemovers.Remove(cell);
            UpdateSurroundingPlants(position, false);
        }


        private bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        private ProductionPlant GetSurroundingPlant(Vector2 pos)
        {
            var hit = Physics2D.Raycast(pos, Vector2.one, 0, plantMask.value);
            if (!hit) return null;
            return hit.transform.GetComponent<ProductionPlant>();
        }

        public void LeftClick(bool pressed)
        {
            leftClicking = pressed;
        }
    }
}