using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Plane : MonoBehaviour {
    [SerializeField] public GameObject _highlight;
    [SerializeField] public GameObject _active;
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
