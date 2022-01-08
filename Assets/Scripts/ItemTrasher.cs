using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrasher : Counter
{
    [SerializeField]
    ItemSO[] blacklist;
    public override void InteractAction(PlayerController player)
    {
        if (!player.currentItem) return;
        ItemScript playerItem = player.currentItem;
        foreach(ItemSO blacklistItem in blacklist)
        {
            if(playerItem.itemName == blacklistItem.itemName)
            {
                return;
            }
        }
        player.currentItem.Destroy();
        player.currentItem = null;
        player.trashSound.Play();
    }
}
