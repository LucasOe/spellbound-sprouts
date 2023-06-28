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
                    Debug.Log(data.position.x + x * size.x);
                    gameManager.CreateTile(Tile, new Vector3(data.position.x + x * size.x, data.position.y, data.position.z + z * size.z), Quaternion.identity, this.transform);
                }
            }
        }
    }
}