using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject player;
    public new Camera camera;
    public CharacterController Controller;
    public Inventory Inventory;
    public InventoryDrops InventoryDrops;
    public UI ui;
    public Animator Animator;

    public float movementSpeed = 25.0f;
    public float damage = 4.0f;
    public float Range = 10.0f;
    public float AttackCooldown = 0.5f;
    public float MaxHealth = 100f;
    public float currentHealth = 100f;

    private Timer attackCooldownTimer;
    private Timer toolCooldownTimer;

    private Vector3 velocity = new();
    private Vector2 mousePosition;
    public float distanceToCursor;

    public AudioSource AudioSource;
    public AudioClip AttackAudioClip;
    public AttackParticle AttackParticle;

    private void Start()
    {
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
            var toRoation = Quaternion.LookRotation(new Vector3(relativeVelocity.x, 0, relativeVelocity.z), Vector3.up);
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
        if (!PauseMenu.isPaused)
        {
            Ray ray = camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100))
            {
                var clickedObject = hitInfo.transform.gameObject;

                if (clickedObject.TryGetComponent(out Enemy enemy))
                {
                    ClickedEnemy(enemy);
                }

                if (clickedObject.TryGetComponent(out Tile tile))
                {
                    ClickedTile(tile);
                }

                if (clickedObject.TryGetComponent(out Cauldron cauldron))
                {
                    ClickedCauldron(cauldron);
                }
            }
        }
    }

    private void ClickedEnemy(Enemy enemy)
    {
        if (!attackCooldownTimer)
        {
            // Play Animation and Sound
            Animator.SetTrigger("attack");
            AudioSource.pitch = Random.Range(0.8f, 1.2f);
            AudioSource.PlayOneShot(AttackAudioClip, .4f);

            // Spawn Particle
            AttackParticle particle = Instantiate(AttackParticle, transform.position, Quaternion.identity);
            particle.Setup(enemy, damage);

            // Rotate towards Enemy
            var direction = (enemy.transform.position - transform.position).normalized;
            player.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            // Start Attack cooldown
            attackCooldownTimer = this.CreateTimer(AttackCooldown);
            attackCooldownTimer.StartTimer();
        }
    }

    private void ClickedTile(Tile tile)
    {
        if (distanceToCursor <= Range && !GameManager.IsNight)
        {
            HandleTileInteraction(tile);
        }
    }

    private void ClickedCauldron(Cauldron cauldron)
    {
        cauldron.OnClick();
    }

    private void HandleTileInteraction(Tile tile)
    {
        Inventory.Tool ActiveTool = Inventory.ActiveTool;

        if (!tile.Plant && ActiveTool != Inventory.Tool.Harvest && InventoryDrops.GetAmount(Inventory.GetPlant().ItemDrop) > 0)
        {
            Plant tileContent = GameManager.CreatePlant(Inventory.GetPlant(), tile.transform.position, Quaternion.identity, tile);
            tile.Plant = tileContent;
            InventoryDrops.RemoveItem(Inventory.GetPlant().ItemDrop, 1);
            ui.RefreshAmounts();
        }
        else if (tile.Plant && ActiveTool == Inventory.Tool.Harvest)
        {
            GameManager.DestroyPlant(tile.Plant);
        }
    }

    public void OnSelectHarvest(InputValue value)
    {
        if (!toolCooldownTimer)
        {
            if (Inventory.ActiveTool == Inventory.Tool.Harvest)
            {
                Inventory.SetTool(Inventory.Tool.Plant);
                ui.ToggleToolType(Inventory.ActiveTool);
            }
            else if (Inventory.ActiveTool == Inventory.Tool.Plant)
            {
                Inventory.SetTool(Inventory.Tool.Herb);
                ui.ToggleToolType(Inventory.ActiveTool);
            }
            else if (Inventory.ActiveTool == Inventory.Tool.Herb)
            {
                Inventory.SetTool(Inventory.Tool.Harvest);
                ui.ToggleToolType(Inventory.ActiveTool);
            }

            // Start Tool cooldown
            toolCooldownTimer = this.CreateTimer(0.35f);
            toolCooldownTimer.StartTimer();
        }
    }

    public void OnSelectItem1(InputValue value)
    {
        Inventory.SetSeed(0);
        ui.SelectActiveSeed(0, Inventory.ActiveTool);
    }

    public void OnSelectItem2(InputValue value)
    {
        Inventory.SetSeed(1);
        ui.SelectActiveSeed(1, Inventory.ActiveTool);
    }

    public void OnSelectItem3(InputValue value)
    {
        Inventory.SetSeed(2);
        ui.SelectActiveSeed(2, Inventory.ActiveTool);
    }

    public void OnSelectItem4(InputValue value)
    {
        Inventory.SetSeed(3);
        ui.SelectActiveSeed(3, Inventory.ActiveTool);
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
        AudioSource.pitch = Random.Range(0.6f, .9f);
        AudioSource.PlayOneShot(audioclip, vol);
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Debug.Log("Ded");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void OnStopTimerTesting()
    {
        GameManager.TimeManger.timer.StopTimer();
    }
}
