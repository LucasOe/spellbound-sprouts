using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;
 
public class Tile : MonoBehaviour {
    public GameManager GameManager;
    [SerializeField] public GameObject _highlight;
    [SerializeField] public GameObject _disabled;
    public Plant Plant;
    public float distance;

    bool isActive;

    void OnMouseEnter() {
        float distance = GameManager.Player.distanceBetween.distanceTotal;
        if(distance < 10) {
            _highlight.SetActive(true);
        } else {
            _disabled.SetActive(true);
        }
    }
 
    void OnMouseExit() {
        _highlight.SetActive(false);
        _disabled.SetActive(false);
    }
}
