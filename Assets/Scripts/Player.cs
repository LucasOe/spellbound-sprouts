using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject player;
    public new Camera camera;
    public Inventory Inventory;
    public UI ui;

    public float movementSpeed = 25.0f;
    public float damage = 4.0f;

    private new Rigidbody rigidbody;
    private Vector3 velocity = new();
    private Vector2 mousePosition;
    public float distanceToCursor;

    public AudioSource AudioSource;
    public AudioClip AttackAudioClip;

    private void Start()
    {
        Inventory.EmptyInventory();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Roatate velocity vector so that the player moves relative to the camera
        var relativeVelocity = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * velocity;
        // Move player
        // transform.Translate(relativeVelocity * movementSpeed * Time.deltaTime);
        rigidbody.MovePosition(transform.position + movementSpeed * Time.deltaTime * relativeVelocity);
    }

    public void OnMove(InputValue value)
    {
        var vector = value.Get<Vector2>();
        velocity = new Vector3(vector.x / 2, 0, vector.y / 2);
    }

    public void OnLook(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
        Ray ray = camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 300f))
        {
            var target = new Vector3(hitInfo.point.x, player.transform.position.y, hitInfo.point.z);
            player.transform.LookAt(target);

            if (TryGetComponent(out MonoBehaviour targetObject))
                distanceToCursor = this.GetDistance(targetObject);
        }
    }

    public void OnFire(InputValue value)
    {
        Ray ray = camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100))
        {
            var clickedObject = hitInfo.transform.gameObject;

            if (clickedObject.TryGetComponent(out Enemy enemy))
                ClickedEnemy(enemy);

            if (clickedObject.TryGetComponent(out Tile tile))
                ClickedTile(tile);
        }
    }

    public void ClickedEnemy(Enemy enemy)
    {
        enemy.Damage(damage);
        AudioSource.PlayOneShot(AttackAudioClip);
    }

    public void ClickedTile(Tile tile)
    {
        if (distanceToCursor <= 10 && !GameManager.IsNight)
        {
            HandleTileInteraction(tile);
        }
    }

    private void HandleTileInteraction(Tile tile)
    {
        Tool ActiveTool = Inventory.activeTool;

        if (!tile.Plant && ActiveTool != Inventory.Harvest && Inventory.GetPlant().seed.amount > 0)
        {
            Plant tileContent = GameManager.CreatePlant(Inventory.GetPlant(), tile.transform.position, Quaternion.identity, tile);
            tile.Plant = tileContent;
            tileContent.seed.PlantSeed();
            ui.RefreshAmounts();
        }
        else if (tile.Plant && ActiveTool == Inventory.Harvest)
        {
            GameManager.DestroyPlant(tile.Plant);
        }
    }

    public void OnSelectHarvest(InputValue value)
    {
        if (Inventory.activeTool != Inventory.Harvest)
        {
            Inventory.SetTool(Inventory.Harvest);
            ui.ToggleToolType(Inventory.activeTool);
        }
        else
        {
            Inventory.SetTool(Inventory.HerbSeeds);
            ui.ToggleToolType(Inventory.activeTool);
        }
    }


    public void OnSelectHerb(InputValue value)
    {
        Inventory.SetTool(Inventory.HerbSeeds);
        ui.ToggleToolType(Inventory.activeTool);
    }

    public void OnSelectPlant(InputValue value)
    {
        Inventory.SetTool(Inventory.PlantSeeds);
        ui.ToggleToolType(Inventory.activeTool);
    }

    public void OnSelectItem1(InputValue value)
    {
        Inventory.SetSeed(0);
        ui.selectActiveSeed(0, Inventory.activeTool);
    }

    public void OnSelectItem2(InputValue value)
    {
        Inventory.SetSeed(1);
        ui.selectActiveSeed(1, Inventory.activeTool);
    }

    public void OnSelectItem3(InputValue value)
    {
        Inventory.SetSeed(2);
        ui.selectActiveSeed(2, Inventory.activeTool);
    }

    public void OnSelectItem4(InputValue value)
    {
        Inventory.SetSeed(3);
        ui.selectActiveSeed(3, Inventory.activeTool);
    }

    public void OnSkipDay(InputValue value)
    {
        if (!GameManager.IsNight)
        {
            Debug.Log("Skipped to Night");
            GameManager.TimeManger.SkipDay();
        }
    }
}
