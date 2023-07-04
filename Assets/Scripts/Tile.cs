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
        if (!PauseMenu.isPaused)
        {
            if (gameManager.Player.distanceToCursor <= gameManager.Player.Range && !gameManager.IsNight)
            {
                highlight.SetActive(true);
                if (Plant)
                    Plant.SetHealthbarActive(true);
            }
        }
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
        if (Plant && gameManager.IsNight) 
        {
            Plant.SetHealthbarActive(false);
        }
    }
}
