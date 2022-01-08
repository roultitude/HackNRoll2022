using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class OrderManager : MonoBehaviour
{
    [SerializeField]
    CustomerSO[] customers;
    int nextCustomerId = 0;
    CustomerSO customerLeft;
    CustomerSO customerRight;
    [SerializeField]
    Counter leftCounter, rightCounter;
    [SerializeField]
    TextMeshProUGUI leftText, rightText;
    [SerializeField]
    ParticleSystem leftParticles, rightParticles;
    [SerializeField]
    AudioSource successAudio;
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            completeOrder(0);
            completeOrder(1);
        }
        
    }
    public void updateText()
    {
        if (customerLeft) leftText.text = customerLeft.orderText;
        if (customerRight) rightText.text = customerRight.orderText;

    }

    public void check()
    {
        if (leftCounter.storedItem)
        {
            foreach (ItemSO item in customerLeft.order)
            {
                if (item.itemName == leftCounter.storedItem.itemName)
                {
                    ItemScript storedItem = leftCounter.storedItem;
                    leftCounter.pickUp();
                    storedItem.Destroy();
                    completeOrder(0);
                    leftParticles.Play();
                    successAudio.Play();
                }
            }
        }
        if (rightCounter.storedItem)
        {
            foreach (ItemSO item in customerRight.order)
            {
                if (item.itemName == rightCounter.storedItem.itemName)
                {
                    ItemScript storedItem = rightCounter.storedItem;
                    rightCounter.pickUp();
                    storedItem.Destroy();
                    completeOrder(1);
                    rightParticles.Play();
                    successAudio.Play();
                }
            }
        }
        
    }

    public void completeOrder(int id)
    {
        PV.RPC("completeOrderRPC", RpcTarget.AllBufferedViaServer, id);
    }
    [PunRPC]
    public void completeOrderRPC(int id)
    {
        if(id == 0)
        {
            if (customerLeft)
            {
                GameManager.instance.changeMoney(customerLeft.moneyReward);
            }
            
            customerLeft = customers[nextCustomerId];
            nextCustomerId++;
        }
        else
        {
            if (customerRight)
            {
                GameManager.instance.changeMoney(customerRight.moneyReward);
            }
            customerRight = customers[nextCustomerId];
            nextCustomerId++;
        }
        updateText();
    }

}
