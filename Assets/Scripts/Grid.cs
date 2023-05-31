using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    [SerializeField] GameObject _tile;

    private static int _width = 20, _depth = 20;
    public uint Height = 1;
    public GameObject[,] tiles = new GameObject[_width, _depth];
    
    void Start() {
        for (uint x = 0; x < _width; ++x) {
            for (uint y = 0; y < Height; ++y)
            {
                for (uint z = 0; z < _depth; ++z)
                {
                    if (x > 0 && x < _width - 1 && 
                        y > 0 && y < Height - 1 && 
                        z > 0 && z < _depth - 1) 
                        continue;

                    GameObject tileInstance = Instantiate(_tile, new Vector3(-22.6f + x * 2, 0.02f, -22.6f + z * 2), Quaternion.identity);
                    tileInstance.name = "Square: " + x + " " + z;
                    tiles[x,z] = tileInstance;
                }
            }
        } 
        _tile.SetActive(false);
    }
}