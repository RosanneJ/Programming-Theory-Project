using UnityEngine;

public class Shovel : Tool
{
    public new void PerformAction()
    {
        Debug.Log("Using the Shovel");
        base.PerformAction();
    }
}