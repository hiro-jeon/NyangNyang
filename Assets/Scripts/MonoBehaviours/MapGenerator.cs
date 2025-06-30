using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public int width = 50;
    public int height = 50;

    public Tilemap tilemap;
    public Tile groundTile;
    public Tile waterTile;
    public Tile wallTile;

    [Range(0f, 1f)]
    public float waterChance = 0.05f;
    [Range(0f, 1f)]
    public float wallClusterChange = 0.2f;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        tilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                // 기본은 땅
                tilemap.SetTile(pos, groundTile);

                if (Random.value < waterChance && IsNearEdge(x, y))
                {
                    SpreadWater(pos, 3);
                }

                else if (Random.value < wallClusterChange)
                {
                    PlaceWallCluster(pos, Random.Range(2, 5));
                }
            }
        }
    }

    bool IsNearEdge(int x, int y)
    {
        return (x < 5 || y < 5 || x > width - 5 || y > height - 5);
    }

    void SpreadWater(Vector3Int center, int radius)
    {
        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = -radius; dy <= radius; dy++)
            {
                if (dx * dx + dy * dy <= radius * radius)
                {
                    Vector3Int pos = new Vector3Int(center.x + dx, center.y + dy, 0);
                    tilemap.SetTile(pos, waterTile);
                }
            }
        }
    }

    void PlaceWallCluster(Vector3Int center, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3Int offset = center + new Vector3Int(Random.Range(-1, 2), Random.Range(-1, 2), 0);
            tilemap.SetTile(offset, wallTile);
        }
    }
}
