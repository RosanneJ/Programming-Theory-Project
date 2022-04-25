using UnityEngine;

public class Shovel : Tool
{
    public override void PerformAction(RaycastHit hit)
    {
        Debug.Log("Using the Shovel");
    }
}