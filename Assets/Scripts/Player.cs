using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject player;
    public new Camera camera;
    public Inventory inventory;
    public UI ui;
    public Grid grid;

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
            distanceToCursor = this.GetDistance(hitInfo.collider.GetComponent<MonoBehaviour>());
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
            if (!tile.Plant && inventory.activeItem != Inventory.ActiveItem.Harvest)
            {
                Plant tileContent = GameManager.CreatePlant(inventory.GetPlant(), tile.transform.position, Quaternion.identity, tile);
                tile.Plant = tileContent;
            }
            else if (tile.Plant && inventory.activeItem == Inventory.ActiveItem.Harvest)
            {
                GameManager.DestroyPlant(tile.Plant);
            }
        }
    }

    public void OnSelectHarvest(InputValue value)
    {
        if (inventory.activeItem != Inventory.ActiveItem.Harvest)
        {
            inventory.SetItem(Inventory.ActiveItem.Harvest);
            ui.ToggleToolType(inventory.activeItem);
        }
        else
        {
            inventory.SetItem(Inventory.ActiveItem.Herb);
            ui.ToggleToolType(inventory.activeItem);
        }
    }


    public void OnSelectHerb(InputValue value)
    {
        inventory.SetItem(Inventory.ActiveItem.Herb);
        ui.ToggleToolType(inventory.activeItem);
    }

    public void OnSelectPlant(InputValue value)
    {
        inventory.SetItem(Inventory.ActiveItem.Plant);
        ui.ToggleToolType(inventory.activeItem);
    }

    public void OnSelectItem1(InputValue value)
    {
        inventory.SetItemIndex(0);
        ui.selectActiveItem(0);
    }

    public void OnSelectItem2(InputValue value)
    {
        inventory.SetItemIndex(1);
        ui.selectActiveItem(1);
    }

    public void OnSelectItem3(InputValue value)
    {
        inventory.SetItemIndex(2);
        ui.selectActiveItem(2);
    }

    public void OnSelectItem4(InputValue value)
    {
        inventory.SetItemIndex(3);
        ui.selectActiveItem(3);
    }

    public void OnSkipDay(InputValue value)
    {
        if (!GameManager.IsNight)
        {
            Debug.Log("Skipped to Night");
            GameManager.TimeManger.StartNight(GameManager.Day);
        }
    }
}
