using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

public GameObject _plane;
[SerializeField] private int _width, _depth;
public uint Height = 1;
    // Start is called before the first frame update
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

                var plane = Instantiate(_plane, new Vector3(-21 + x * 5, 0.02f, -21 + z * 5), Quaternion.identity);
            }
        }
    } 
    _plane.SetActive(false);
    }
}
