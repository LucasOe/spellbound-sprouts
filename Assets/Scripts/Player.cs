using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject player;
    public new Camera camera;
    public CharacterController Controller;
    public Inventory Inventory;
    public UI ui;
    public Animator Animator;

    public float movementSpeed = 25.0f;
    public float damage = 4.0f;
    public float Range = 10.0f;
    public float MaxHealth = 100f;
    public float currentHealth = 100f;

    private Vector3 velocity = new();
    private Vector2 mousePosition;
    public float distanceToCursor;
    private Enemy targetEnemy;

    public AudioSource AudioSource;
    public AudioSource AudioSourceItems;
    public AudioClip AttackAudioClip;
    public AttackParticle AttackParticle;

    private void Start()
    {
        Inventory.ResetInventory();
        GameManager.CreatedPlant += OnCreatePlant;
    }

    private void Update()
    {
        // Roatate velocity vector so that the player moves relative to the camera
        var relativeVelocity = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * velocity;
        relativeVelocity.y += -9.81f * Time.deltaTime; // Apply gravity
        // Move player
        Controller.Move(movementSpeed * Time.deltaTime * relativeVelocity);

        if (velocity.magnitude != 0)
        {
            var toRoation = Quaternion.LookRotation(velocity, Vector3.up);
            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, toRoation, 720.0f * Time.deltaTime);
        }

        Animator.SetBool("isWalking", velocity.magnitude > 0);
        handleScroll();
    }

    private void handleScroll()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            Inventory.ScrollSeed(1);
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            Inventory.ScrollSeed(-1);
        }
    }

    public void OnMove(InputValue value)
    {
        var vector = value.Get<Vector2>();
        velocity = new Vector3(vector.x, 0, vector.y);
    }

    public void OnLook(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
        Ray ray = camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 300f))
        {
            if (hitInfo.transform.gameObject.TryGetComponent(out MonoBehaviour targetObject))
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
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Animator.SetTrigger("attack");
            targetEnemy = enemy;
        }
    }

    public void ClickedTile(Tile tile)
    {
        if (distanceToCursor <= Range && !GameManager.IsNight)
        {
            HandleTileInteraction(tile);
        }
    }

    private void HandleTileInteraction(Tile tile)
    {
        Tool ActiveTool = Inventory.activeTool;

        if (!tile.Plant && ActiveTool != Inventory.Harvest && Inventory.GetPlant().seed._amount > 0)
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
        if (Inventory.activeTool == Inventory.Harvest)
        {
            Inventory.SetTool(Inventory.PlantSeeds);
            ui.ToggleToolType(Inventory.activeTool);
        }
        else if (Inventory.activeTool == Inventory.PlantSeeds)
        {
            Inventory.SetTool(Inventory.HerbSeeds);
            ui.ToggleToolType(Inventory.activeTool);
        }
        else if (Inventory.activeTool == Inventory.HerbSeeds)
        {
            Inventory.SetTool(Inventory.Harvest);
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

    private void OnCreatePlant(Plant plant)
    {
        ui.RefreshAmounts();
    }

    public void PlaySound(AudioClip audioclip, float vol)
    {
        AudioSourceItems.pitch = (Random.Range(0.6f, .9f));
        AudioSourceItems.PlayOneShot(audioclip, vol);
    }

    private void AttackEvent()
    {
        if (targetEnemy)
        {
            AttackParticle particle = Instantiate(AttackParticle, transform.position, Quaternion.identity);
            particle.Setup(targetEnemy, damage);
            AudioSource.PlayOneShot(AttackAudioClip);
        }
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Debug.Log("Ded");
        }
    }
}
