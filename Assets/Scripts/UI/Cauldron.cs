using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct ItemAmounts
{
    public Item Item1;
    public int Amount1;

    public Item Item2;
    public int Amount2;

    public Item Item3;
    public int Amount3;

    public Item[] Reward;
    public int RewardAmount;

    public bool IsWinCondition;
}

public class Cauldron : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameManager GameManager;

    public Outline OutlineValid;
    public Outline OutlineInvalid;

    public GameObject ingredientObject1;
    public GameObject ingredientObject2;
    public GameObject ingredientObject3;
    private GameObject instance1;
    private GameObject instance2;
    private GameObject instance3;
    public TextMeshProUGUI ingredientText1;
    public TextMeshProUGUI ingredientText2;
    public TextMeshProUGUI ingredientText3;
    public TextMeshProUGUI ingredientAmount1;
    public TextMeshProUGUI ingredientAmount2;
    public TextMeshProUGUI ingredientAmount3;
    public int objectRotate;

    public AudioSource CauldronAudio;
    public AudioClip Bubbles;

    [SerializeField] private Vector3 _rotation;

    public ItemAmounts[] ItemAmounts;

    private int currentState = 0;

    public void UpdateIngredients()
    {
        ingredientText1.text = string.Format("{0}:", ItemAmounts[currentState].Item1.ItemData.DisplayName);
        ingredientText2.text = string.Format("{0}:", ItemAmounts[currentState].Item2.ItemData.DisplayName);
        ingredientText3.text = string.Format("{0}:", ItemAmounts[currentState].Item3.ItemData.DisplayName);
        ingredientAmount1.text = string.Format("{0}/{1}", GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item1.ItemData), ItemAmounts[currentState].Amount1);
        ingredientAmount2.text = string.Format("{0}/{1}", GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item2.ItemData), ItemAmounts[currentState].Amount2);
        ingredientAmount3.text = string.Format("{0}/{1}", GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item3.ItemData), ItemAmounts[currentState].Amount3);
    }

    public void Update()
    {
        objectRotate++;
        instance1.transform.rotation = Quaternion.Euler(10, 30 + objectRotate, 30);
        instance2.transform.rotation = Quaternion.Euler(30, -30 + objectRotate, 30);
        instance3.transform.rotation = Quaternion.Euler(-30, 90 + objectRotate, 30);
    }

    public void Start()
    {
        GetPotionModels();
    }

    private bool GetValidState()
    {
        return
            GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item1.ItemData) >= ItemAmounts[currentState].Amount1 &&
            GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item2.ItemData) >= ItemAmounts[currentState].Amount2 &&
            GameManager.Player.InventoryDrops.GetAmount(ItemAmounts[currentState].Item3.ItemData) >= ItemAmounts[currentState].Amount3;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetValidState())
        {
            OutlineValid.Enable();
        }
        else
        {
            OutlineInvalid.Enable();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GetValidState())
        {
            OutlineValid.Disable();
        }
        else
        {
            OutlineInvalid.Disable();
        }
    }

    public void OnClick()
    {
        if (GetValidState())
        {
            if (ItemAmounts[currentState].IsWinCondition)
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                for (int i = 0; i < ItemAmounts[currentState].Reward.Length; i++)
                {
                    for (int j = 0; j < ItemAmounts[currentState].RewardAmount; j++)
                    {
                        GameManager.CreateItem(ItemAmounts[currentState].Reward[i], transform.position);
                    }
                }
                currentState += 1;
                CauldronAudio.PlayOneShot(Bubbles, .7f);
                UpdateIngredients();
                GetPotionModels();

            }
        }
    }

    public void GetPotionModels()
    {
        Destroy(instance1);
        Destroy(instance2);
        Destroy(instance3);
        instance1 = Instantiate(ItemAmounts[currentState].Item1.UIGameobject, ingredientObject1.transform.position, Quaternion.identity);
        instance2 = Instantiate(ItemAmounts[currentState].Item2.UIGameobject, ingredientObject2.transform.position, Quaternion.identity);
        instance3 = Instantiate(ItemAmounts[currentState].Item3.UIGameobject, ingredientObject3.transform.position, Quaternion.identity);
    }
}
