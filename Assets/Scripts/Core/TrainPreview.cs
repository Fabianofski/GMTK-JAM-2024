// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 20.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityEngine;

namespace F4B1.Core
{
    public class TrainPreview : MonoBehaviour
    {

        private TrainNavigator navigator;
        private Animator animator;
        private static readonly int Y = Animator.StringToHash("y");
        private static readonly int X = Animator.StringToHash("x");
        private Vector2 direction;

        public void PreviewMode()
        {
            
        }
        
        private void Start()
        {
            navigator = FindObjectOfType<TrainNavigator>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var pos = Vector3Int.RoundToInt(transform.position);
            direction = navigator.GetNewDirection(pos, Vector2.right);
            if (direction == Vector2.zero) direction = Vector2.right;
            UpdateAnimator();
        }
        
        private void UpdateAnimator()
        {
            animator.SetFloat(X, direction.x);
            animator.SetFloat(Y, direction.y);
        }
    }
}