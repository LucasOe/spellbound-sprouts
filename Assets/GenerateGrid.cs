using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{

public GameObject block;
public uint Width = 100;
public uint Height = 1;
public uint Depth = 100;
    // Start is called before the first frame update
    void Start() {
          for (uint x = 0; x < Width; ++x) {
        for (uint y = 0; y < Height; ++y)
        {
            for (uint z = 0; z < Depth; ++z)
            {
                if (x > 0 && x < Width - 1 && 
                    y > 0 && y < Height - 1 && 
                    z > 0 && z < Depth - 1) 
                    continue;

                Instantiate(block, new Vector3(x*10,y,z*10), Quaternion.identity);
            }
        }
    } 
    }
}
