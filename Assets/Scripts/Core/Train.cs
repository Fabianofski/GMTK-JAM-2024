// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 16.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityEngine;

namespace F4B1.Core
{
    public class Train : MonoBehaviour
    {
        [SerializeField] private Vector2 direction;
        [SerializeField] private bool reachedDeadEnd;
        [SerializeField] private Vector2 targetPos;
        [SerializeField] private float speed = 10;
        
        private TrainNavigator navigator;

        private void Start()
        {
            navigator = FindObjectOfType<TrainNavigator>();
        }

        private void Update()
        {
            if (!ReachedTargetPos() && !reachedDeadEnd) Move(); 
            else CalculateNewPosition();
        }

        private void Move()
        {
            transform.Translate(direction * (speed / 500)); 
        }

        private void CalculateNewPosition()
        {
            Debug.Log("Reached Target Pos!");

            var pos = Vector3Int.RoundToInt(transform.position);
            var newDirection = navigator.GetNewDirection(pos, direction);

            if (direction != newDirection)
                transform.position = Vector3Int.RoundToInt(transform.position);

            reachedDeadEnd = newDirection == Vector2.zero;

            if (reachedDeadEnd) return;
            direction = newDirection;
            targetPos += direction;
        }

        private bool ReachedTargetPos()
        {
            var reachedX = direction.x > 0 ? targetPos.x <= transform.position.x : targetPos.x >= transform.position.x;
            var reachedY = direction.y > 0 ? targetPos.y <= transform.position.y : targetPos.y >= transform.position.y;;

            return reachedX && reachedY;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(targetPos, new Vector2(0.2f, 0.2f));
        }
    }
}
