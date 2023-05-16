using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UIElements;
using static Inventory;

public class UI : MonoBehaviour
{
    Label _herb; 
    Label _plant;
   
    private void OnEnable() 
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _herb = root.Q<Label>("Herb");
        _plant = root.Q<Label>("Plant");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePlantType(ActiveItem itemType) {
        if(itemType == ActiveItem.Herb) 
        {
            _herb.AddToClassList("active");
            _plant.RemoveFromClassList("active");
        }
        else {
            _plant.AddToClassList("active");
            _herb.RemoveFromClassList("active");
        }
    }
}
