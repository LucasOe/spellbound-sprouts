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
        var ray = camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 300f))
        {
            var target = new Vector3(hitInfo.point.x, player.transform.position.y, hitInfo.point.z);
            player.transform.LookAt(target);
            distanceToCursor = Utils.GetDistance(player, hitInfo.transform.gameObject);
        }
    }

    public void OnFire(InputValue value)
    {
        Ray ray = camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100))
        {
            var clickedObject = hitInfo.transform.gameObject;

            // Player clicks on enemy
            if (clickedObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Damage(damage);
            }

            //Loops through tiles
            for (int i = 0; i < grid.tiles.GetLength(0); i++)
            {
                for (int j = 0; j < grid.tiles.GetLength(1); j++)
                {
                    Tile tile = grid.tiles[i, j];

                    if (tile.gameObject == clickedObject)
                    {
                        HandleFieldInteraction(tile, tile.gameObject.transform.position);
                    }
                }
            }
        }
    }

    public void HandleFieldInteraction(Tile tile, Vector3 pos)
    {
        if (!tile.Plant && tile.distance < 10 && inventory.activeItem != Inventory.ActiveItem.Harvest)
        {
            //Place plant: Tile is empty and < 10 away
            Plant tileContent = GameManager.CreatePlant(inventory.GetPlant(), pos, Quaternion.identity);
            tile.Plant = tileContent;
        }
        else if (tile.Plant && tile.distance < 10 && inventory.activeItem == Inventory.ActiveItem.Harvest)
        {
            //Harvest plant: Tile is filled with plant and < 10 away
            GameManager.DestroyPlant(tile.Plant);
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
        Debug.Log("Test");
    }
}
