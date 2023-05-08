using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    [SerializeField] GameObject _plane;

    private static int _width = 20, _depth = 20;
    public uint Height = 1;
    public GameObjectExtended[,] plane = new GameObjectExtended[_width, _depth];
    
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

                    GameObject planeInstance = Instantiate(_plane, new Vector3(-22.6f + x * 5, 0.02f, -22.6f + z * 5), Quaternion.identity);
                    planeInstance.name = "Square: " + x + " " + z;
                    GameObjectExtended planeExtended = new GameObjectExtended(planeInstance);
                    plane[x,z] = planeExtended;
                }
            }
        } 
        _plane.SetActive(false);
    }
}

public class GameObjectExtended {
    private bool isActive = false;
    public GameObject gameObject = new GameObject();


    public GameObjectExtended(GameObject _pGameobject) {
        this.gameObject = _pGameobject;
    }

    public void toggleActive() {
        this.isActive = !isActive;
        gameObject.GetComponent<Plane>().Activate();
    }
}