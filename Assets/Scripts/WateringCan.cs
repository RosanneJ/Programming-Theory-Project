using System;
using System.Collections;
using UnityEngine;

public class WateringCan : Tool
{
    public new void PerformAction(RaycastHit hit)
    {
        Debug.Log("Perform action of watering can");
        base.PerformAction(hit);
    }

}