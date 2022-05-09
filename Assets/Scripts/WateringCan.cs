using UnityEngine;

// INHERITANCE
public class WateringCan : Tool
{
    public new void PerformAction()
    {
        Debug.Log("Perform action of watering can");
        base.PerformAction();
    }

}