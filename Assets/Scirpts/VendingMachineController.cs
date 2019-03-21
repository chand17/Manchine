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

    public int Score;

    private string wantedItem;
    private TextMeshPro text;
    [SerializeField] private GameObject coin;

    void Awake()
    {
        Dropbox.OnItemDrop += ItemDropped;
        text = GetComponent<TextMeshPro>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        selectNextItem();
    }

    private string[] items = new string[] { "A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4"};
    private void selectNextItem()
    {
        int rand = Random.Range(0, items.Length);
        wantedItem = items[rand];
        DisplayKeypad.SetDisplay(wantedItem, true);
        dispenseCoins(rand + 1);
    }
    private void dispenseCoins(int amount)
    {
        dispenser.DispenseObject(coin, amount);
    }

    public void ItemDropped(GrabbableItem item)
    {
       if(item !=null) checkDroppedItem(item.GetComponent<InventoryItem>());
    }

    private void checkDroppedItem(InventoryItem item)
    {
        if (item.ItemName.Equals(wantedItem))
        {
            Score++;
            audioSource.clip = succeedClip;
        }
        else
        {
            Score--;
            audioSource.clip = errorClip;
        }

        audioSource.Play();
        text.text = "Score: " + Score.ToString();

        selectNextItem();
    }
}
