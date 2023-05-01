using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Plane : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private GameObject _highlight;
 
    void OnMouseEnter() {
        _highlight.SetActive(true);
    }
 
    void OnMouseExit() {
        _highlight.SetActive(false);
    }
}