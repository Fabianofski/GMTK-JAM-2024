// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 16.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace F4B1.Core
{
    public class TrainNavigator : MonoBehaviour
    {
        [Header("Tilemaps")] [SerializeField] private Tilemap tilemap;
        [SerializeField] private Grid grid;


        public Vector2 GetNewDirection(Vector3Int currentPosition, Vector2 direction)
        {
            var possibleDirections = new Dictionary<Vector2, bool>
            {
                [Vector2.up] = tilemap.HasTile(currentPosition + Vector3Int.up),
                [Vector2.down] = tilemap.HasTile(currentPosition + Vector3Int.down),
                [Vector2.right] = tilemap.HasTile(currentPosition + Vector3Int.right),
                [Vector2.left] = tilemap.HasTile(currentPosition + Vector3Int.left)
            };

            var leftTangent = new Vector2(-direction.y, direction.x);
            var rightTangent = new Vector2(direction.y, -direction.x);

            if (possibleDirections[direction])
                return direction;
            else if (possibleDirections[leftTangent])
                return leftTangent;
            else if (possibleDirections[rightTangent])
                return rightTangent;
            else
                return Vector2.zero;
        }
    }
}