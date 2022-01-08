using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSO", menuName ="ScriptableObjects/CustomerSO", order = 0)]
public class CustomerSO : ScriptableObject
{
    public string customerName;
    public string orderText;
    public int moneyReward;
    public ItemSO[] order;
}
