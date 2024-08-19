// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 16.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core
{
    public class Train : MonoBehaviour
    {
        [Header("Movement")]
        private TrainNavigator navigator;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Vector2 direction;
        [SerializeField] private bool reachedDeadEnd;
        [SerializeField] private Vector2 targetPos;
        [SerializeField] private float speed = 10;
        [SerializeField] private BoolVariable gamePaused;
        
        [Header("Intersections")]
        [SerializeField] private Vector2[] intersections;
        [SerializeField] private GameObject intersectionChanger;
        [SerializeField] private Transform intersectionParent;
        private Dictionary<Vector2, Vector2[]> intersectionDirections;

        [Header("Waggons")] 
        [SerializeField] private int waggonCount;
        [SerializeField] private GameObject waggonPrefab;
        [SerializeField] private Transform waggonParent;
        private readonly List<Vector2> lastDirections = new List<Vector2>();
        private readonly List<Waggon> waggons = new List<Waggon>();
        
        [Header("Animations")]
        private Animator animator;
        private static readonly int Y = Animator.StringToHash("y");
        private static readonly int X = Animator.StringToHash("x");

        private void Start()
        {
            navigator = FindObjectOfType<TrainNavigator>();
            UpdateTrainLinePath();
            animator = GetComponent<Animator>();
            UpdateAnimator();
            AddAllWaggons();
        }

        private void AddAllWaggons()
        {
            for (var i = 1; i <= waggonCount; i++)
                AddWaggon(transform.position - (Vector3)(direction * i));
        }

        private void AddWaggon(Vector3 position)
        {
            var waggon = Instantiate(waggonPrefab, position, Quaternion.identity, waggonParent);
            lastDirections.Add(direction);
            waggons.Add(waggon.GetComponent<Waggon>());
        }

        public void UpdateTrainLinePath()
        {
            var points = new List<Vector3>() { targetPos };
            var visitedStates = new HashSet<(Vector3 position, Vector3 direction)> { (targetPos, direction) };
            intersectionDirections = new Dictionary<Vector2, Vector2[]>();

            var linePosition = targetPos;
            var lineDirection = direction;
            bool visitedBefore;

            var pathIntersections = new List<Vector2>();

            do
            {
                lineDirection = navigator.GetNewDirection(Vector3Int.RoundToInt(linePosition), lineDirection);
                linePosition += lineDirection;
                points.Add(linePosition);

                var possibleDirections =
                    navigator.GetPossibleDirectionsList(Vector3Int.RoundToInt(linePosition), lineDirection);
                if (possibleDirections.Length >= 3 && !pathIntersections.Contains(linePosition))
                {
                    pathIntersections.Add(linePosition);
                    intersectionDirections.Add(linePosition, possibleDirections);
                }
                
                var currentState = (linePosition, lineDirection);
                visitedBefore = visitedStates.Contains(currentState);
                visitedStates.Add(currentState);
            } while (lineDirection != Vector2.zero && !visitedBefore && points.Count <= 100);

            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
            intersections = pathIntersections.ToArray();
            SpawnIntersectionChangers();
        }

        private void SpawnIntersectionChangers()
        {
            for (var i = 0; i < intersectionParent.childCount; i++)
               Destroy(intersectionParent.GetChild(i).gameObject); 
            
            foreach (var intersection in intersections)
            {
                var go = Instantiate(intersectionChanger, intersection, Quaternion.identity, intersectionParent);
                var possibleDirections = intersectionDirections[intersection];
                go.GetComponent<IntersectionChanger>().SetPossibleDirections(possibleDirections);
            } 
        }

        private void Update()
        {
            if (gamePaused.Value) return;
            
            if (!ReachedTargetPos() && !reachedDeadEnd) Move();
            else CalculateNewPosition();
        }

        private void Move()
        {
            transform.Translate(direction * (speed * Time.deltaTime ));
            for (var i = 0; i < waggonCount; i++)
            {
                var waggon = waggons[i];
                var waggonDir = lastDirections[i];
                waggon.Move(speed, waggonDir); 
            }
        }

        private void CalculateNewPosition()
        {
            var pos = Vector3Int.RoundToInt(transform.position);
            var newDirection = navigator.GetNewDirection(pos, direction);

            if (direction != newDirection)
                transform.position = Vector3Int.RoundToInt(transform.position);

            reachedDeadEnd = newDirection == Vector2.zero;

            if (reachedDeadEnd)
            {
                targetPos = Vector2Int.RoundToInt(transform.position);
                return;
            }
            
            lastDirections.Insert(0, direction);
            if (lastDirections.Count > waggonCount)
                lastDirections.RemoveAt(lastDirections.Count - 1);
            
            direction = newDirection;
            targetPos += direction;
            UpdateAnimator();
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