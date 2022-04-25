using UnityEngine;

public class Shovel : Tool
{
    public new void PerformAction(RaycastHit hit)
    {
        Debug.Log("Using the Shovel");
        base.PerformAction(hit);
    }
}