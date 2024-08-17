// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 16.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.Collections.Generic;
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
        private Animator animator;
        private LineRenderer lineRenderer;
        private static readonly int Y = Animator.StringToHash("y");
        private static readonly int X = Animator.StringToHash("x");

        private void Start()
        {
            navigator = FindObjectOfType<TrainNavigator>();
            lineRenderer = GetComponentInChildren<LineRenderer>();
            UpdateTrainLinePath();
            animator = GetComponent<Animator>();
            UpdateAnimator();
        }

        private void UpdateTrainLinePath()
        {
            var points = new List<Vector3>() { targetPos };
            var visitedStates = new HashSet<(Vector3 position, Vector3 direction)> { (targetPos, direction) };

            var linePosition = targetPos;
            var lineDirection = direction;
            bool visitedBefore;

            do
            {
                lineDirection = navigator.GetNewDirection(Vector3Int.RoundToInt(linePosition), lineDirection);
                linePosition += lineDirection;
                points.Add(linePosition);

                var currentState = (linePosition, lineDirection);
                visitedBefore = visitedStates.Contains(currentState);
                visitedStates.Add(currentState);
            } while (lineDirection != Vector2.zero && !visitedBefore && points.Count <= 100);

            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }

        private void Update()
        {
            if (!ReachedTargetPos() && !reachedDeadEnd) Move();
            else CalculateNewPosition();
        }

        private void Move()
        {
            transform.Translate(direction * (speed * Time.deltaTime ));
        }

        private void CalculateNewPosition()
        {
            var pos = Vector3Int.RoundToInt(transform.position);
            var newDirection = navigator.GetNewDirection(pos, direction);

            if (direction != newDirection)
                transform.position = Vector3Int.RoundToInt(transform.position);

            reachedDeadEnd = newDirection == Vector2.zero;

            if (reachedDeadEnd) return;
            direction = newDirection;
            targetPos += direction;
            UpdateAnimator();
            UpdateTrainLinePath();
        }

        private void UpdateAnimator()
        {
            animator.SetFloat(X, direction.x);
            animator.SetFloat(Y, direction.y);
        }

        private bool ReachedTargetPos()
        {
            var reachedX = direction.x > 0 ? targetPos.x <= transform.position.x : targetPos.x >= transform.position.x;
            var reachedY = direction.y > 0 ? targetPos.y <= transform.position.y : targetPos.y >= transform.position.y;

            return reachedX && reachedY;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(targetPos, new Vector2(0.2f, 0.2f));
        }
    }
}