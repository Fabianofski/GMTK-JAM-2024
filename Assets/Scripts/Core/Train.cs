// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 16.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.Collections.Generic;
using System.Linq;
using F4B1.Audio;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core
{
    public class Train : MonoBehaviour
    {
        [Header("Movement")] private TrainNavigator navigator;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Vector2 direction;
        [SerializeField] private bool reachedDeadEnd;
        [SerializeField] private Vector2 targetPos;
        [SerializeField] private float speed = 10;
        [SerializeField] private BoolVariable gamePaused;
        private List<Vector3> trainPath;

        [Header("Intersections")] [SerializeField]
        private Vector2ValueList intersections;

        [SerializeField] private GameObject intersectionChanger;
        [SerializeField] private Transform intersectionParent;
        private Dictionary<Vector2, Vector2> savedDirections = new Dictionary<Vector2, Vector2>();

        [Header("Waggons")] [SerializeField] private int waggonCount;
        [SerializeField] private GameObject waggonPrefab;
        [SerializeField] private Transform waggonParent;
        private readonly List<Vector2> lastDirections = new List<Vector2>();
        private readonly List<Waggon> waggons = new List<Waggon>();
        [SerializeField] private StringVariable selectedUpgrade;
        [SerializeField] private FactoryVariable selectedFactory;

        [Header("Animations")] private Animator animator;
        private static readonly int Y = Animator.StringToHash("y");
        private static readonly int X = Animator.StringToHash("x");

        [Header("Sounds")] 
        [SerializeField] private SoundEvent playSound;
        [SerializeField] private Sound waggonAttaching;
        [SerializeField] private AudioSource trainSounds;
        [SerializeField] private AudioSource whistleSound;
        
        private void Start()
        {
            navigator = FindObjectOfType<TrainNavigator>();
            var pos = Vector3Int.RoundToInt(transform.position);
            direction = navigator.GetNewDirection(pos, direction);
            targetPos = (Vector2)(Vector3) pos + direction;
            
            UpdateTrainLinePath();
            animator = GetComponent<Animator>();
            UpdateAnimator();
            AddAllWaggons();
            
            SpawnIntersectionChangers();
        }

        private void AddAllWaggons()
        {
            for (var i = 1; i <= waggonCount; i++)
                AddWaggon(transform.position - (Vector3)(direction * i));
        }

        public void CheckForWaggonUpgrade(bool toggle)
        {
            if (!toggle || selectedUpgrade.Value != "waggon") return;

            var lastWaggon = waggons[^1];
            var waggonPos = lastWaggon ? lastWaggon.transform.position : transform.position;
            var dir = (Vector3) lastDirections[^1];
            playSound.Raise(waggonAttaching);
            AddWaggon(waggonPos - dir);
            waggonCount++;
            waggons[waggonCount - 1].SetOldDirection(dir);
            lastDirections.Add(dir);
            
            selectedFactory.Value.UpgradeUsed();
            selectedUpgrade.SetValue("none");
        }
        
        private void AddWaggon(Vector3 position)
        {
            var waggon = Instantiate(waggonPrefab, position, Quaternion.identity, waggonParent);
            if (lastDirections.Count < waggonCount) lastDirections.Add(direction);
            waggons.Add(waggon.GetComponent<Waggon>());
        }

        public void UpdateTrainLinePath()
        {
            var points = new List<Vector3>() { targetPos };
            var visitedStates = new HashSet<(Vector3 position, Vector3 direction)> { (targetPos, direction) };

            var linePosition = targetPos;
            var lineDirection = direction;
            bool visitedBefore;

            do
            {
                if (savedDirections.TryGetValue(linePosition, out var savedDirection) &&
                    !AreVectorsOpposite(savedDirection, lineDirection))
                    lineDirection = savedDirection;
                else
                    lineDirection = navigator.GetNewDirection(Vector3Int.RoundToInt(linePosition), lineDirection);
                linePosition += lineDirection;
                points.Add(linePosition);

                var currentState = (linePosition, lineDirection);
                visitedBefore = visitedStates.Contains(currentState);
                visitedStates.Add(currentState);
            } while (lineDirection != Vector2.zero && !visitedBefore && points.Count <= 100);

            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
            trainPath = points;
        }

        public void SpawnIntersectionChangers()
        {
            for (var i = 0; i < intersectionParent.childCount; i++)
                Destroy(intersectionParent.GetChild(i).gameObject);

            foreach (var intersection in intersections)
            {
                // if (!trainPath.Contains(intersection)) return;
                var go = Instantiate(intersectionChanger, intersection, Quaternion.identity, intersectionParent);
                var changer = go.GetComponent<IntersectionChanger>();
                changer.train = this;
                if (savedDirections.TryGetValue(intersection, out var savedDirection))
                    changer.SetDirection(savedDirection);
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
            transform.Translate(direction * (speed * Time.deltaTime));
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
            if (savedDirections.TryGetValue((Vector3)pos, out var savedDirection))
                if (!AreVectorsOpposite(savedDirection, direction))
                    newDirection = savedDirection;

            if (direction != newDirection)
                transform.position = Vector3Int.RoundToInt(transform.position);

            reachedDeadEnd = newDirection == Vector2.zero;

            if (reachedDeadEnd)
            {
                targetPos = Vector2Int.RoundToInt(transform.position);
                trainSounds.Stop();
                return;
            }
            if (!trainSounds.isPlaying)
            {
                trainSounds.Play();
                whistleSound.Play();
            }

            lastDirections.Insert(0, direction);
            if (lastDirections.Count > waggonCount)
                lastDirections.RemoveAt(lastDirections.Count - 1);

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

        public void SetIntersectionDirection(Vector2 transformPosition, Vector2 selectedDirection)
        {
            savedDirections[transformPosition] = selectedDirection;
        }

        bool AreVectorsOpposite(Vector2 a, Vector2 b)
        {
            return Vector2.Dot(a.normalized, b.normalized) == -1;
        }
    }
}