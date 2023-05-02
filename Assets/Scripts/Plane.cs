using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Plane : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private GameObject _highlight;
    bool isActive;

    void OnMouseEnter() {
            Debug.Log("Hello: " + gameObject.name);
            _highlight.SetActive(true);
    }
 
    void OnMouseExit() {
        _highlight.SetActive(false);
            Debug.Log("Bye: " + gameObject.name);
    }
}