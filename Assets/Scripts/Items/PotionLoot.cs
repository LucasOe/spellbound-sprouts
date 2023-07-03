using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionLoot : MonoBehaviour
{
    int amount = 0;
    public string name;
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
    }
}
