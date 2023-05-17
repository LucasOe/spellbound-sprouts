using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;
 
public class Tile : MonoBehaviour {
    [SerializeField] public GameObject _highlight;
    [SerializeField] public GameObject _disabled;
    public GameObject _content;
    private Player player;
    public float distance;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    bool isActive;

    void OnMouseEnter() {
        distance = player.distanceBetween.distanceTotal;
        if(distance < 10) {
        _highlight.SetActive(true);
        }
        else {
            _disabled.SetActive(true);
        }
    }
 
    void OnMouseExit() {
        _highlight.SetActive(false);
        _disabled.SetActive(false);
    }
}
