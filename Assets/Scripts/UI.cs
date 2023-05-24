using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UIElements;
using static Inventory;

public class UI : MonoBehaviour
{
    Button _herb; 
    Button _plant;
    Button _harvest;
   
    private void OnEnable() 
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _herb = root.Q<Button>("Herb");
        _plant = root.Q<Button>("Plant");
        _harvest = root.Q<Button>("Harvest");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleToolType(ActiveItem activeItem) {
        switch(activeItem) 
        {
          case ActiveItem.Harvest:
            _harvest.AddToClassList("active");
            _herb.RemoveFromClassList("active");
            _plant.RemoveFromClassList("active");
            break;
          case ActiveItem.Plant:
            _harvest.RemoveFromClassList("active");
            _herb.RemoveFromClassList("active");
            _plant.AddToClassList("active");
            break;
          case ActiveItem.Herb:
            _harvest.RemoveFromClassList("active");
            _herb.AddToClassList("active");
            _plant.RemoveFromClassList("active");
            break;
          default:
            break;
        }
    }
}
