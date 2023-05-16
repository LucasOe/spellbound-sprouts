using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Tile : MonoBehaviour {
    [SerializeField] public GameObject _highlight;
    [SerializeField] public GameObject _active;
    public GameObject _content;

    bool isActive;

    void OnMouseEnter() {
        _highlight.SetActive(true);
    }
 
    void OnMouseExit() {
        _highlight.SetActive(false);
    }

    public void Activate() {
        _active.SetActive(true);
    }
}
