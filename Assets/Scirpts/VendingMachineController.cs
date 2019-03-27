using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class VendingMachineController : MonoBehaviour
{
    //Audio
    public AudioClip errorClip;
    public AudioClip succeedClip;
    private AudioSource audioSource;

    //Controls
    public DisplayKeypadController DisplayKeypad;
    public DropboxControl Dropbox;
    public DispenserControl dispenser;

    //Properties
    public int Score;
    [SerializeField] private int MaxItemCount;
    [SerializeField] private InventoryItem[] AvailableItems;
    [SerializeField] private GameObject coin;

    private TextMeshPro text;
    private List<InventoryItem> droppedItems;
    private VMCondition currentCondition; 

    void Awake()
    {
        Dropbox.OnItemDrop += ItemDropped;
        text = GetComponent<TextMeshPro>();
        audioSource = GetComponent<AudioSource>();
        droppedItems = new List<InventoryItem>();
    }

    private VMCondition createCondition(InventoryItem[] availableItems)
    {
        //Get Total Items
        int totalItems = Random.Range(0, MaxItemCount);

        //Get wantedItems array and ItemCost
        string[] wantedItems = new string[totalItems];
        int itemCosts = 0;

        for (int i = 0; i < totalItems; i++)
        {
            int rand = Random.Range(0,availableItems.Length);
            wantedItems[i] = availableItems[rand].ItemName;
            itemCosts += availableItems[rand].ItemCost;
        }

        return new VMCondition(wantedItems, itemCosts);
    }

    public bool CheckCondition()
    {
        //Get string[] of droppedItems names
        string[] itemNames = new string[droppedItems.Count];
        for (int i = 0; i < itemNames.Length; i++)
        {
            itemNames[i] = droppedItems[i].ItemName;
        }

        //Get Coins dispersed
        //TEMP
        int coinsReceived = currentCondition.itemCosts;
        //TEMP

        return currentCondition.CheckCondition(itemNames, coinsReceived);
    }

    public void ItemDropped(GrabbableItem item)
    {
        InventoryItem newItem = item.GetComponent<InventoryItem>();
        if(newItem != null) droppedItems.Add(newItem);
    }
}
