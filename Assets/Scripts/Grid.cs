using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public GameObject Tile;

    public int Width = 20;
    public int Depth = 20;
    public GameObject[,] tiles;
    
    void Start() {
        tiles = new GameObject[Width, Depth];
        for (uint x = 0; x < Width; x++) {
            for (uint z = 0; z < Depth; z++) {
                GameObject tileInstance = Instantiate(Tile, new Vector3(-22.6f + x * 2, 0.02f, -22.6f + z * 2), Quaternion.identity);
                tileInstance.name = "Square: " + x + " " + z;
                tiles[x,z] = tileInstance;
            }
        } 
        Tile.SetActive(false);
    }
}