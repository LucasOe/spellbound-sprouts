using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UI : MonoBehaviour
{
    Button _herb;
    Button _plant;
    Button _harvest;
    List<Button> _plantSeeds = new List<Button>();
    List<Button> _herbSeeds = new List<Button>();
    List<Button> _seeds;
    Button _1P;
    Button _2P;
    Button _3P;
    Button _4P;
    Button _1H;
    Button _2H;
    Button _3H;
    Button _4H;

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
        Button _1H = root.Q<Button>("1H");
        Button _2H = root.Q<Button>("2H");
        Button _3H = root.Q<Button>("3H");
        Button _4H = root.Q<Button>("4H");
        _plantSeeds.Add(_1P);
        _plantSeeds.Add(_2P);
        _plantSeeds.Add(_3P);
        _plantSeeds.Add(_4P);
        _herbSeeds.Add(_1H);
        _herbSeeds.Add(_2H);
        _herbSeeds.Add(_3H);
        _herbSeeds.Add(_4H);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void selectActiveSeed(int i, Tool activeTool)
    {
        int j = 0;
        _seeds = _plantSeeds;
        switch (activeTool)
        {
            case Inventory.Harvest:
                break;
            case Inventory.Plant:
                _seeds = _plantSeeds;
                break;
            case Inventory.Herb:
                _seeds = _herbSeeds;
                break;
            default:
                break;
        }
        foreach (var seed in _seeds)
        {
            if (j == i)
            {
                seed.AddToClassList("active");
            }
            else
            {
                seed.RemoveFromClassList("active");
            }
            j++;
        }
    }

    public void ToggleToolType(Tool activeTool)
    {
        switch (activeTool)
        {
            case Inventory.Harvest:
                _harvest.AddToClassList("active");
                _herb.RemoveFromClassList("active");
                _plant.RemoveFromClassList("active");
                break;
            case Inventory.Plant:
                _harvest.RemoveFromClassList("active");
                _herb.RemoveFromClassList("active");
                _plant.AddToClassList("active");
                break;
            case Inventory.Herb:
                _harvest.RemoveFromClassList("active");
                _herb.AddToClassList("active");
                _plant.RemoveFromClassList("active");
                break;
            default:
                break;
        }
    }
}
