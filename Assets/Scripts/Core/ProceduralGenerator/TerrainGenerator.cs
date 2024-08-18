// /**
//  * This file is part of: GMTK-JAM-2024
//  * Created: 18.08.2024
//  * Copyright (C) 2024 Fabian Friedrich
//  * Distributed under the terms of the MIT license (cf. LICENSE.md file)
//  **/

using UnityEngine;

namespace F4B1.Core.ProceduralGenerator
{
    public class TerrainGenerator
    {
        public enum BiomeType
        {
            Forest = 1,
            Desert = 2,
            Clay = 3
        }

        public enum BlockType
        {
            Water = 0,
            Ground = 1
        }

        public float[,] GenerateNoiseMap(int width, int height, float scale,
            int octaves, Vector2 position)
        {
            var noiseMap = new float[width, height];
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var sampleX = (float)x / width * scale;
                var sampleY = (float)y / height * scale;
                var noise = 0f;
                var frequency = 0f;
                for (var oct = 1; oct < octaves; oct *= 2)
                {
                    frequency += 1f / oct;
                    noise += 1f / oct * Mathf.PerlinNoise(oct * (sampleX + position.x * scale),
                        oct * (sampleY + position.y * scale));
                }

                noise /= frequency;
                noiseMap[x, y] = noise;
            }

            return noiseMap;
        }

        public int[,] NormalizeTerrain(
            float[,] terrainMap, Vector2 position
        )
        {
            var width = terrainMap.GetLength(0);
            var height = terrainMap.GetLength(1);
            var map = new int[width, height];

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                map[x, y] = terrainMap[x, y] > 0.2f ? (int)BlockType.Ground : (int)BlockType.Water;
            }

            return map;
        }

        public int[,] NormalizeBiome(
            float[,] biomeMap
        )
        {
            var width = biomeMap.GetLength(0);
            var height = biomeMap.GetLength(1);
            var map = new int[width, height];

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                if (biomeMap[x, y] < 0.35f)
                    map[x, y] = (int)BiomeType.Clay;
                else if (biomeMap[x, y] < 0.53f)
                    map[x, y] = (int)BiomeType.Forest;
                else
                    map[x, y] = (int)BiomeType.Desert;
            return map;
        }

        public int[,] MergeTerrainWithBiome(
            int[,] terrainMap, int[,] biomeMap
        )
        {
            var width = terrainMap.GetLength(0);
            var height = terrainMap.GetLength(1);
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                if (terrainMap[x, y] == 1)
                    terrainMap[x, y] = biomeMap[x, y];
            return terrainMap;
        }
    }
}