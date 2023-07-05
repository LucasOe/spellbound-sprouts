using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UI : MonoBehaviour
{
    public GameManager GameManager;
    public AudioSource Clicker;
    public AudioClip Click;

    Button _herb;
    Button _plant;
    Button _harvest;
    Button _buttonSkip;
    VisualElement _day;
    VisualElement _defensivePlants;
    VisualElement _herbs;
    Label _harvestLabel;
    VisualElement _wheel;
    VisualElement _nightWand;
    VisualElement _currentHealth;
    List<VisualElement> _plantSeeds = new List<VisualElement>();
    List<VisualElement> _herbSeeds = new List<VisualElement>();
    List<VisualElement> _seeds;
    List<Label> _seedAmountLabels = new List<Label>();
    Label _plantSeedName;
    Label _herbSeedName;


    //Plant Types
    VisualElement _1P;
    VisualElement _2P;
    VisualElement _3P;
    VisualElement _4P;
    Label _1PAmount;
    Label _2PAmount;
    Label _3PAmount;
    Label _4PAmount;

    //Herbs Types
    VisualElement _1H;
    VisualElement _2H;
    VisualElement _3H;
    VisualElement _4H;
    Label _1HAmount;
    Label _2HAmount;
    Label _3HAmount;
    Label _4HAmount;

    //Clock
    VisualElement _face;
    Label _countdown;
    private float enemyCount;
    private float enemiesKilled;

    private void OnEnable()
    {
        // Subscribe to events
        GameManager.DayStart += OnDayStart;
        GameManager.NightStart += OnNightStart;
        GameManager.DestroyedEnemy += OnDestroyedEnemy;

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _herb = root.Q<Button>("Herb");
        _plant = root.Q<Button>("Plant");
        _harvest = root.Q<Button>("Harvest");
        VisualElement _1P = root.Q<VisualElement>("1P");
        VisualElement _2P = root.Q<VisualElement>("2P");
        VisualElement _3P = root.Q<VisualElement>("3P");
        VisualElement _4P = root.Q<VisualElement>("4P");
        VisualElement _1H = root.Q<VisualElement>("1H");
        VisualElement _2H = root.Q<VisualElement>("2H");
        VisualElement _3H = root.Q<VisualElement>("3H");
        VisualElement _4H = root.Q<VisualElement>("4H");
        _buttonSkip = root.Q<Button>("ButtonSkip");
        _defensivePlants = root.Q<VisualElement>("DefensivePlants");
        _herbs = root.Q<VisualElement>("Herbs");
        _wheel = root.Q<VisualElement>("Wheel");
        _harvestLabel = root.Q<Label>("Harvest");
        _nightWand = root.Q<VisualElement>("NightWand");
        _currentHealth = root.Q<VisualElement>("CurrentHealth");
        _day = root.Q<VisualElement>("Day");
        Label _1PAmount = root.Q<Label>("1PAmount");
        Label _2PAmount = root.Q<Label>("2PAmount");
        Label _3PAmount = root.Q<Label>("3PAmount");
        Label _4PAmount = root.Q<Label>("4PAmount");
        Label _1HAmount = root.Q<Label>("1HAmount");
        Label _2HAmount = root.Q<Label>("2HAmount");
        Label _3HAmount = root.Q<Label>("3HAmount");
        Label _4HAmount = root.Q<Label>("4HAmount");
        _plantSeedName = root.Q<Label>("PlantSeedName");
        _herbSeedName = root.Q<Label>("HerbSeedName");
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
        _seeds = _plantSeeds;
    }

    void Start()
    {
        RefreshAmounts();
        SetItemName(0);
        _buttonSkip.clicked += () =>
        {
            GameManager.Player.OnSkipDay();
        };
    }

    void Update()
    {
        float _faceRotation = !GameManager.IsNight
            ? Mathf.Lerp(-90.0f, -270.0f, GameManager.TimeManger.timer.GetPercent()) // Day
            : Mathf.Lerp(90.0f, -90.0f, (enemyCount - enemiesKilled) / enemyCount); // Night

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
        enemiesKilled = 0;
        _nightWand.AddToClassList("visible");
        _day.AddToClassList("hidden");
    }

    void OnDestroyedEnemy(Enemy enemy)
    {
        enemiesKilled += 1;
    }

    public void RefreshAmounts()
    {
        var inventory = GameManager.Player.InventoryDrops;
        for (int i = 0; i < 8; i++)
        {
            _seedAmountLabels[i].text = inventory.GetAmount(i).ToString();
        }
    }

    public void SelectActiveSeed(int i, Inventory.Tool activeTool)
    {
        if (!GameManager.IsNight)
        {
            if (activeTool == Inventory.Tool.Plant)
            {
                _seeds = _plantSeeds;
            }
            if (activeTool == Inventory.Tool.Herb)
            {
                _seeds = _herbSeeds;
            }

            SetItemName(i);

            _seeds.ForEach((seed) =>
            {
                seed.RemoveFromClassList("active");
                ClickSound(0.3f);
            });
            _seeds[i].AddToClassList("active");
        }
    }

    public void ToggleToolType(Inventory.Tool activeTool)
    {
        ClickSound(1f, 0.7f);
        switch (activeTool)
        {
            case Inventory.Tool.Harvest:
                _harvest.AddToClassList("active");
                _herb.RemoveFromClassList("active");
                _plant.RemoveFromClassList("active");
                _defensivePlants.RemoveFromClassList("open");
                _herbs.RemoveFromClassList("open");
                _harvestLabel.AddToClassList("open");
                _wheel.AddToClassList("harvest");
                _wheel.RemoveFromClassList("herb");
                break;
            case Inventory.Tool.Plant:
                _harvest.RemoveFromClassList("active");
                _herb.RemoveFromClassList("active");
                _plant.AddToClassList("active");
                _defensivePlants.AddToClassList("open");
                _herbs.RemoveFromClassList("open");
                _harvestLabel.RemoveFromClassList("open");
                _wheel.RemoveFromClassList("harvest");
                _wheel.RemoveFromClassList("herb");
                _wheel.AddToClassList("plant");
                Timer timer = this.CreateTimer(.3f);
                timer.RunOnFinish((state) =>
                {
                    _wheel.AddToClassList("instantAnimation");
                    Timer timer2 = this.CreateTimer(.01f);
                    timer2.StartTimer();
                    _wheel.RemoveFromClassList("plant");
                    _wheel.RemoveFromClassList("instantAnimation");
                });
                timer.StartTimer();
                break;
            case Inventory.Tool.Herb:
                _harvest.RemoveFromClassList("active");
                _herb.AddToClassList("active");
                _plant.RemoveFromClassList("active");
                _defensivePlants.RemoveFromClassList("open");
                _herbs.AddToClassList("open");
                _harvestLabel.RemoveFromClassList("open");
                _wheel.RemoveFromClassList("harvest");
                _wheel.AddToClassList("herb");
                break;
            default:
                break;
        }
    }

    public void ClickSound(float vol, float pitch = 1f)
    {
        Clicker.pitch = pitch * Random.Range(0.8f, 1.2f);
        Clicker.PlayOneShot(Click, vol);
    }

    public void SetItemName(int i)
    {
        _plantSeedName.text = "Abwehr: " + GameManager.Player.InventoryDrops.ItemAmounts[i].ItemData.DisplayName;
        _herbSeedName.text = "Kraut: " + GameManager.Player.InventoryDrops.ItemAmounts[i + 4].ItemData.DisplayName;
    }
}
