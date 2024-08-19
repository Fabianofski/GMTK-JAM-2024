// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 19.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace F4B1.Core
{
    public class IntersectionChanger : MonoBehaviour
    {

        [SerializeField] private Sprite up;
        [SerializeField] private Sprite down;
        [SerializeField] private Sprite left;
        [SerializeField] private Sprite right;
        [SerializeField] private Sprite all;
        [SerializeField] private Vector2[] possibleDirections;
        private Dictionary<Vector2, Sprite> directionSprites;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private int direction;
        public Train train { get; set; }
        private bool setup;

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            if (setup) return;
            var tilemap = GameObject.FindGameObjectWithTag("RailTilemap").GetComponent<Tilemap>();
            possibleDirections = TrainNavigator.GetPossibleDirectionsList(tilemap, Vector3Int.RoundToInt(transform.position));
            directionSprites = new Dictionary<Vector2, Sprite>()
            {
                { Vector2.zero, all },
                { Vector2.up, up },
                { Vector2.down, down },
                { Vector2.left, left },
                { Vector2.right, right }
            };
            setup = true;
        }

        public void ChangeDirection()
        {
            direction++;
            if (direction >= possibleDirections.Length) direction = 0;
            UpdateIntersection();
        }

        private void UpdateIntersection()
        {
            Setup();
            var selectedDirection = possibleDirections[direction];
            spriteRenderer.sprite = directionSprites[selectedDirection];

            train.SetIntersectionDirection(transform.position, selectedDirection);
        }

        public void SetDirection(Vector2 dir)
        {
            for (var i = 0; i < possibleDirections.Length; i++)
                if (possibleDirections[i] == dir)
                {
                    direction = i;
                    break;
                }
            UpdateIntersection();
        }

    }
}