using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UIElements;
using static Inventory;

public class UI : MonoBehaviour
{
    Button _herb; 
    Button _plant;
   
    private void OnEnable() 
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _herb = root.Q<Button>("Herb");
        _plant = root.Q<Button>("Plant");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePlantType() {
            _herb.ToggleInClassList("active");
            _plant.ToggleInClassList("active");
    }
}
