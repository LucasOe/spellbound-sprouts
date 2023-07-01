using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GridData
{
    public Vector3 position;
    public int Width;
    public int Depth;
}

public class Grid : MonoBehaviour
{
    public GameManager gameManager;
    public Tile Tile;
    public GridData[] GridData;

    void Start()
    {
        foreach (var data in GridData)
        {
            for (int x = 0; x < data.Width; x++)
            {
                for (int z = 0; z < data.Depth; z++)
                {
                    var size = Tile.transform.localScale;
                    Vector3 position = new Vector3(data.position.x + x * size.x, data.position.y, data.position.z + z * size.z)
                        - new Vector3(data.Width * size.x / 2, 0, data.Depth * size.z / 2); // Offset by half so data.position is the center

                    Tile tile = gameManager.CreateTile(Tile, position, Quaternion.identity, this);
                    tile.name = string.Format("Tile[{0:00},{1:00}]", x, z);
                }
            }
        }
    }
}