using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemScript : MonoBehaviour
{
    public PhotonView PV;
    public string itemName;
    public SpriteRenderer spriteRenderer;
    public ItemSO itemSO;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        PV = GetComponent<PhotonView>();
        if (itemSO)
        {
            spriteRenderer.sprite = itemSO.itemSprite;
            itemName = itemSO.itemName;
        }
    }

    public void Setup(ItemSO itemSO)
    {
        PV.RPC("SetupRPC", RpcTarget.All, itemSO.itemName);
    }

    
    [PunRPC]
    public void SetupRPC(string itemSOName)
    {
        ItemSO item = RecipeManager.instance.getItemSOfromName(itemSOName);
        if (item)
        {
            this.itemSO = item;
            spriteRenderer.sprite = itemSO.itemSprite;
            itemName = itemSO.itemName;
        }
    }

    public void Destroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

}
