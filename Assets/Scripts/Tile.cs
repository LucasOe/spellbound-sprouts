using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject highlight;
    public GameObject disabled;
    public Plant Plant;

    public void Setup(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void OnMouseEnter()
    {
        // Only Tiles within 10 units to the player get highlighted
        highlight.SetActive(gameManager.Player.distanceToCursor <= 10);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
