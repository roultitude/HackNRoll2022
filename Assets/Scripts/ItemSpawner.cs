using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class ItemSpawner : Counter
{
    [SerializeField]
    ItemSO spawnedItemSO;

    public override void InteractAction(PlayerController player)
    {
        if (!player.currentItem)
        {
            Transform trans = player.holdPoint;
            GameObject newItem = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "ItemPrefab"), trans.position, trans.rotation);
            newItem.GetComponent<ItemScript>().Setup(spawnedItemSO);
            newItem.transform.SetParent(player.holdPoint);
            player.currentItem = newItem.GetComponent<ItemScript>();
            player.pickupSound.Play();
        }
    }
}
