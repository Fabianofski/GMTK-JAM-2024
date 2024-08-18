// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 18.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace F4B1.Core.ProceduralGenerator
{
    [Serializable]
    public class BiomeSpawnRate
    {
        public float buildingRate;
        public float metal;
        public float wood;
    }

    public class TilemapCreator : MonoBehaviour
    {
        [Header("Tilemaps")] [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase[] tiles;
        [SerializeField] private BiomeSpawnRate[] spawnRates;
        [FormerlySerializedAs("productionPlant")] [SerializeField] private Transform plantParent;
        [SerializeField] private GameObject metal;
        [SerializeField] private GameObject wood;
        [SerializeField] private LayerMask mask;

        [Header("Noise")] private int[,] map;
        [SerializeField] private int size;
        [SerializeField] private float scale;
        [SerializeField] private int octaves;


        private void Awake()
        {
            var terrainGenerator = new TerrainGenerator();

            var position = new Vector2(Random.Range(0, 100), Random.Range(0, 100));
            var terrainNoise = terrainGenerator.GenerateNoiseMap(size, size, scale, octaves * 4, position);
            var terrainMap = map = terrainGenerator.NormalizeTerrain(terrainNoise, position);

            position = new Vector2(Random.Range(0, 100), Random.Range(0, 100));
            var biomeNoise = terrainGenerator.GenerateNoiseMap(size, size, scale, octaves, position);
            var biomeMap = terrainGenerator.NormalizeBiome(biomeNoise);

            map = terrainGenerator.MergeTerrainWithBiome(terrainMap, biomeMap);

            FillTilemap();
        }

        private void FillTilemap()
        {
            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    var distance = Mathf.Sqrt(Mathf.Pow(x - size / 2, 2) + Mathf.Pow(y - size / 2, 2));
                    var biome = distance > 12 ? map[x, y] : 1;
                    var tile = tiles[biome];
                    var pos = new Vector3Int(x - size / 2, y - size / 2, 0);
                    tilemap.SetTile(pos, tile);

                    if (distance < 6) continue;

                    var buildingPos = new Vector2(pos.x + 0.5f, pos.y + 0.5f);
                    var hit = Physics2D.BoxCast(buildingPos, Vector2.one * 3, 0, Vector2.zero, 1, mask.value);
                    if (hit) continue;

                    var rate = spawnRates[biome];
                    var random = Random.Range(0f, 1f);
                    if (random < rate.buildingRate)
                    {
                        random = Random.Range(0f, 1f);
                        Instantiate(random < rate.metal ? metal : wood, buildingPos, Quaternion.identity, plantParent);
                    }
                }
            }
        }
    }
}