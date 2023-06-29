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
    List<Label> _seedAmountLabels = new List<Label>();

    //Plant Types
    Button _1P;
    Button _2P;
    Button _3P;
    Button _4P;
    Label _1PAmount;
    Label _2PAmount;
    Label _3PAmount;
    Label _4PAmount;

    //Herbs Types
    Button _1H;
    Button _2H;
    Button _3H;
    Button _4H;
    Label _1HAmount;
    Label _2HAmount;
    Label _3HAmount;
    Label _4HAmount;

    public Inventory inventory;

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
        Label _1PAmount = root.Q<Label>("1PAmount");
        Label _2PAmount = root.Q<Label>("2PAmount");
        Label _3PAmount = root.Q<Label>("3PAmount");
        Label _4PAmount = root.Q<Label>("4PAmount");
        Label _1HAmount = root.Q<Label>("1HAmount");
        Label _2HAmount = root.Q<Label>("2HAmount");
        Label _3HAmount = root.Q<Label>("3HAmount");
        Label _4HAmount = root.Q<Label>("4HAmount");
        _plantSeeds.Add(_1P);
        _plantSeeds.Add(_2P);
        _plantSeeds.Add(_3P);
        _plantSeeds.Add(_4P);
        _herbSeeds.Add(_1H);
        _herbSeeds.Add(_2H);
        _herbSeeds.Add(_3H);
        _herbSeeds.Add(_4H);
        _seedAmountLabels.Add(_1PAmount);
        _seedAmountLabels.Add(_2PAmount);
        _seedAmountLabels.Add(_3PAmount);
        _seedAmountLabels.Add(_4PAmount);
        _seedAmountLabels.Add(_1HAmount);
        _seedAmountLabels.Add(_2HAmount);
        _seedAmountLabels.Add(_3HAmount);
        _seedAmountLabels.Add(_4HAmount);
    }
    // Start is called before the first frame update
    void Start()
    {
        refreshAmounts();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void refreshAmounts() {
        for(int i = 0; i < inventory.seeds.Length; i++) {
            _seedAmountLabels[i].text = inventory.seeds[i].getAmount().ToString();
        }
    }

    public void selectActiveSeed(int i, Tool activeTool)
    {
        int j = 0;
        _seeds = _plantSeeds;
        switch (activeTool)
        {
            case Harvest:
                break;
            case PlantSeeds:
                _seeds = _plantSeeds;
                break;
            case HerbSeeds:
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
            case Harvest:
                _harvest.AddToClassList("active");
                _herb.RemoveFromClassList("active");
                _plant.RemoveFromClassList("active");
                break;
            case PlantSeeds:
                _harvest.RemoveFromClassList("active");
                _herb.RemoveFromClassList("active");
                _plant.AddToClassList("active");
                break;
            case HerbSeeds:
                _harvest.RemoveFromClassList("active");
                _herb.AddToClassList("active");
                _plant.RemoveFromClassList("active");
                break;
            default:
                break;
        }
    }
}
