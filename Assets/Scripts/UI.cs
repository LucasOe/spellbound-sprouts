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
    List<Button> _activeItems = new List<Button>();
    Button _1P;
    Button _2P;
    Button _3P;
    Button _4P;
   
    private void OnEnable() 
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _herb = root.Q<Button>("Herb");
        _plant = root.Q<Button>("Plant");
        _harvest = root.Q<Button>("Harvest");
        Button _1P = root.Q<Button>("1P");
        Button _2P = root.Q<Button>("2P");
        Button _3P = root.Q<Button>("3P");
        Button _4P = root.Q<Button>("4P");
        _activeItems.Add(_1P);
        _activeItems.Add(_2P);
        _activeItems.Add(_3P);
        _activeItems.Add(_4P);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectActiveItem(int i) {
      int j = 0;
      foreach (var _activeItem in _activeItems) {
          if(j == i) {
                _activeItem.AddToClassList("active");
          }
          else {
            _activeItem.RemoveFromClassList("active");
          }
        j++;
      }
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
