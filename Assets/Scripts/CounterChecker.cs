using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterChecker : Counter
{
    [SerializeField]
    OrderManager orderManager;
    public override void InteractAction(PlayerController player)
    {
        base.InteractAction(player);
        if (storedItem)
        {
            orderManager.check();
        }
    }
}
