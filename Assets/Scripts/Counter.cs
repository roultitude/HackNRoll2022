using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Counter : MonoBehaviour
{
    protected PhotonView PV;
    [SerializeField]
    protected Transform pickupPoint;
    public ItemScript storedItem;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    public void placeDown(ItemScript item)
    {
        if (storedItem) return;
        PV.RPC("placeDownRPC", RpcTarget.All, item.PV.ViewID);
    }

    public virtual void InteractAction(PlayerController player)
    {
        ItemScript playerItem = player.currentItem;
        if (playerItem && !storedItem)
        {
            placeDown(playerItem);
            player.currentItem = null;
            player.pickupSound.Play();
        }
        else if (!playerItem)
        {
            player.currentItem = pickUp();
            playerItem = player.currentItem;
            playerItem.gameObject.transform.SetParent(player.holdPoint);
            playerItem.transform.localPosition = Vector3.zero;
            player.pickupSound.Play();
        } else
        {
            Debug.Log("searching for valid recipe" + playerItem.itemName + storedItem.itemName);
            RecipeSO recipe = RecipeManager.instance.getValidHandRecipe(playerItem.itemName, storedItem.itemName);
            Debug.Log(recipe);
            if (recipe)
            {
                storedItem.Setup(recipe.outputOne);
                if (recipe.outputTwo)
                {
                    player.currentItem.Setup(recipe.outputTwo);
                } else
                {
                    player.currentItem.Destroy();
                    player.currentItem = null;
                }
                player.recipeSound.Play();
            }
        }
    }
    public ItemScript pickUp()
    {
        ItemScript item = storedItem;
        if (!item.PV.IsMine)
        {
            item.PV.TransferOwnership(PhotonNetwork.LocalPlayer);
        }
        PV.RPC("pickUpRPC", RpcTarget.All);
        return item;
    }

    [PunRPC]
    public void placeDownRPC(int viewID)
    {
        storedItem = Photon.Pun.PhotonView.Find(viewID).GetComponent<ItemScript>();
        storedItem.PV.TransferOwnership(PhotonNetwork.MasterClient);
        storedItem.transform.SetParent(pickupPoint);
        storedItem.transform.localPosition = Vector3.zero;
    }
    [PunRPC]
    public void pickUpRPC()
    {
        storedItem.transform.SetParent(null);
        storedItem = null;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<PlayerController>().currentCounter = this;
        }
    }
}
