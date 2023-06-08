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

    private readonly bool isActive;

    void OnMouseEnter()
    {
        float distance = GameManager.Player.distanceBetween.distanceTotal;
        if (distance < 10)
        {
            highlight.SetActive(true);
        }
        else
        {
            disabled.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
        disabled.SetActive(false);
    }
}
