// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 19.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace F4B1.Core
{
    public class IntersectionChanger : MonoBehaviour
    {

        [SerializeField] private Sprite up;
        [SerializeField] private Sprite down;
        [SerializeField] private Sprite left;
        [SerializeField] private Sprite right;
        [SerializeField] private Vector2[] possibleDirections;
        private Dictionary<Vector2, Sprite> directionSprites;
        private SpriteRenderer spriteRenderer;
        private int direction;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            directionSprites = new Dictionary<Vector2, Sprite>()
            {
                { Vector2.up, up },
                { Vector2.down, down },
                { Vector2.left, left },
                { Vector2.right, right }
            };
        }

        public void SetPossibleDirections(Vector2[] directions)
        {
            possibleDirections = directions;
        }
        
        public void ChangeDirection()
        {
            direction++;
            if (direction >= possibleDirections.Length) direction = 0;
            var selectedDirection = possibleDirections[direction];
            spriteRenderer.sprite = directionSprites[selectedDirection];
        }

    }
}