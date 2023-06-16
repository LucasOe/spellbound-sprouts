using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject highlight;
    public GameObject disabled;
    public Plant Plant;
    public float distance;

    void OnMouseEnter()
    {
        // Only Tiles within 10 units to the player get highlighted
        highlight.SetActive(GameManager.Player.distanceToCursor <= 10);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
        disabled.SetActive(false);
    }
}
