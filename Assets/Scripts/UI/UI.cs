using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UI : MonoBehaviour
{
    public GameManager GameManager;

    Button _herb;
    Button _plant;
    Button _harvest;
    Button _buttonSkip;
    VisualElement _day;
    VisualElement _defensivePlants;
    VisualElement _herbs;
    VisualElement _wheel;
    VisualElement _nightWand;
    VisualElement _currentHealth;
    GroupBox _itemTypeWrap;
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

    //Clock
    VisualElement _face;
    Label _countdown;
    private int enemyCount;

    private void OnEnable()
    {
        // Subscribe to events
        GameManager.DayStart += OnDayStart;
        GameManager.NightStart += OnNightStart;
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
        _buttonSkip = root.Q<Button>("ButtonSkip");
        _defensivePlants = root.Q<VisualElement>("DefensivePlants");
        _herbs = root.Q<VisualElement>("Herbs");
        _wheel = root.Q<VisualElement>("Wheel");
        _nightWand = root.Q<VisualElement>("NightWand");
        _currentHealth = root.Q<VisualElement>("CurrentHealth");
        _day = root.Q<VisualElement>("Day");
        _itemTypeWrap = root.Q<GroupBox>("ItemTypeWrap");
        Label _1PAmount = root.Q<Label>("1PAmount");
        Label _2PAmount = root.Q<Label>("2PAmount");
        Label _3PAmount = root.Q<Label>("3PAmount");
        Label _4PAmount = root.Q<Label>("4PAmount");
        Label _1HAmount = root.Q<Label>("1HAmount");
        Label _2HAmount = root.Q<Label>("2HAmount");
        Label _3HAmount = root.Q<Label>("3HAmount");
        Label _4HAmount = root.Q<Label>("4HAmount");
        _countdown = root.Q<Label>("Countdown");
        _face = root.Q<VisualElement>("Face");
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

    void Start()
    {
        RefreshAmounts();

        _buttonSkip.clicked += () => {
            GameManager.TimeManger.SkipDay();
        };
    }

    void Update()
    {
        float _faceRotation = !GameManager.IsNight
            ? Mathf.Lerp(-90.0f, -270.0f, GameManager.TimeManger.timer.GetPercent()) // Day
            : Mathf.Lerp(90.0f, -90.0f, (float)GameManager.Enemies.Count / enemyCount); // Night

        _face.transform.rotation = Quaternion.Euler(0, 0, _faceRotation);
        _countdown.text = GameManager.TimeManger.timer ? GameManager.TimeManger.timer.DisplayTime() : "00:00";

        // Set Health
        _currentHealth.style.width = GameManager.Player.currentHealth * 2;
    }

    void OnDayStart(int day)
    {
        enemyCount = 0;
        _nightWand.RemoveFromClassList("visible");
        _day.RemoveFromClassList("hidden");
    }

    void OnNightStart(int day)
    {
        GameManager.Spawners.ForEach((spawner) => enemyCount += spawner.GetEnemyCount(day));
        _nightWand.AddToClassList("visible");
        _day.AddToClassList("hidden");
    }

    public void RefreshAmounts()
    {
        var inventory = GameManager.Player.Inventory;
        for (int i = 0; i < inventory.seeds.Length; i++)
        {
            _seedAmountLabels[i].text = inventory.seeds[i]._amount.ToString();
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
                _defensivePlants.RemoveFromClassList("open");
                _herbs.RemoveFromClassList("open");
                _wheel.AddToClassList("harvest");
                _wheel.RemoveFromClassList("herb");
                _itemTypeWrap.style.left = -54; 
                break;
            case PlantSeeds:
                _harvest.RemoveFromClassList("active");
                _herb.RemoveFromClassList("active");
                _plant.AddToClassList("active");
                _defensivePlants.AddToClassList("open");
                _herbs.RemoveFromClassList("open");
                _wheel.RemoveFromClassList("harvest");
                _wheel.RemoveFromClassList("herb");
                _wheel.AddToClassList("plant");
                _itemTypeWrap.style.left = -78; 
                Timer timer = this.CreateTimer(.3f);
                timer.RunOnFinish((state) =>
                {
                    _itemTypeWrap.AddToClassList("instantAnimation");
                    _wheel.AddToClassList("instantAnimation");
                    Timer timer2 = this.CreateTimer(.01f);
                    _itemTypeWrap.style.left = 0; 
                    timer2.RunOnFinish((state) =>
                    {
                        _itemTypeWrap.RemoveFromClassList("instantAnimation");
                    });
                    timer2.StartTimer();
                    _wheel.RemoveFromClassList("plant");
                        _wheel.RemoveFromClassList("instantAnimation");
                });
                timer.StartTimer();
                break;
            case HerbSeeds:
                _harvest.RemoveFromClassList("active");
                _herb.AddToClassList("active");
                _plant.RemoveFromClassList("active");
                _defensivePlants.RemoveFromClassList("open");
                _herbs.AddToClassList("open");
                _wheel.RemoveFromClassList("harvest");
                _wheel.AddToClassList("herb");
                _itemTypeWrap.style.left = -27; 
                break;
            default:
                break;
        }
    }
}
