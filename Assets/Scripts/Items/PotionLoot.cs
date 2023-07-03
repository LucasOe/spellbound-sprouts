using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionLoot : MonoBehaviour
{
    int amount = 0;
    public string name;
    public TextMeshProUGUI ingredient;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAmount(int i) {
        amount += i;
        Debug.Log(amount);
        ingredient.GetComponent<TextMesh>().text = amount.ToString();
    }
}
