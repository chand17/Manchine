using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VMCondition
{
    private string[] wantedItems;
    public int itemCosts;

    public VMCondition(string[] wantedItems, int itemCosts)
    {
        this.wantedItems = wantedItems;
        this.itemCosts = itemCosts;
    }

    public bool CheckCondition(string[] itemsCollected, int coinsCollected)
    {
        if(coinsCollected < itemCosts) return false;
        
        for (int i = 0; i < wantedItems.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < itemsCollected.Length; j++)
            {
                if(wantedItems[i].Equals(itemsCollected[j]))
                {
                    found = true;
                }
                if(!found) return false;
            }
        }

        return true;
    }
}
