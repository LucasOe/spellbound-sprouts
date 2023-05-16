using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Inventory;
using static UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private new Camera camera;
    [SerializeField] private Inventory inventory;
    [SerializeField] private UI ui;


    [SerializeField] private float movementSpeed = 25.0f;
    [SerializeField] private float damage = 4.0f;
    
    private new Rigidbody rigidbody;
    private Vector3 velocity = new Vector3();
    private Vector2 mousePosition;

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        // Roatate velocity vector so that the player moves relative to the camera
        var relativeVelocity = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * velocity;
        // Move player
        // transform.Translate(relativeVelocity * movementSpeed * Time.deltaTime);
        rigidbody.MovePosition(transform.position + relativeVelocity * movementSpeed * Time.deltaTime);   
    }

    public void OnMove(InputValue value) {
        var vector = value.Get<Vector2>();
        velocity = new Vector3(vector.x, 0, vector.y);
    }

    public void OnLook(InputValue value) {
        mousePosition = value.Get<Vector2>();
        var ray = camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 300f)) {
            var target = new Vector3(hitInfo.point.x, player.transform.position.y, hitInfo.point.z);
            player.transform.LookAt(target);
        }
    }

    public void OnFire(InputValue value) {
        Ray ray = camera.ScreenPointToRay( mousePosition );

        if( Physics.Raycast( ray, out RaycastHit hitInfo, 100 ) ) {
            var clickedObject = hitInfo.transform.gameObject;

            // Player clicks on enemy
            if(clickedObject.TryGetComponent(out Enemy enemy)) {
                enemy.Damage(damage);
            }
            
            // Place plants
            var grid = GameObject.Find("Field").GetComponent<Grid>();
            GameObjectExtended tile = null;
            for(int i = 0; i < grid.tiles.GetLength(0); i++)  {
                for(int j = 0; j < grid.tiles.GetLength(1); j++)  {
                    tile = grid.tiles[i, j];
                    if(tile.gameObject.name == clickedObject.name) {
                        if(!tile.gameObject.GetComponent<Tile>()._content) 
                        {
                            //tile.toggleActive();
                            Vector3 position = tile.gameObject.transform.position;
                            tile.gameObject.GetComponent<Tile>()._content = inventory.getPlant();
                            GameObject tileContent = Instantiate(tile.gameObject.GetComponent<Tile>()._content, position, Quaternion.identity);
                        }    

                    }
                }
            }
        }
    } 

    
    public void OnSelectHerb(InputValue value) {
        inventory.SetItem(ActiveItem.Herb);
        ui.TogglePlantType(ActiveItem.Herb);
    }

    
    public void OnSelectPlant(InputValue value) {
        inventory.SetItem(ActiveItem.Plant);
        ui.TogglePlantType(ActiveItem.Plant);
    }
}
