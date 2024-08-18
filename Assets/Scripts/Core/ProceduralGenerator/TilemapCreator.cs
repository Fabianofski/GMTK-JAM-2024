// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 18.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace F4B1.Core.ProceduralGenerator
{
    public class TilemapCreator : MonoBehaviour
    {
        private int[,] map;
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase[] tiles;
        [SerializeField] private int size;
        [SerializeField] private float scale;
        [SerializeField] private int octaves;

        private void Awake()
        {
            var terrainGenerator = new TerrainGenerator();
            
            var position = new Vector2(Random.Range(0, 100), Random.Range(0, 100));
            var terrainNoise = terrainGenerator.GenerateNoiseMap(size, size, scale, octaves, position);
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
                    var biome = distance > 8 ? map[x, y] : 1;
                    var tile = tiles[biome];
                    tilemap.SetTile(new Vector3Int(x - size / 2, y - size / 2, 0), tile);
                }
            }
        }
    }
}