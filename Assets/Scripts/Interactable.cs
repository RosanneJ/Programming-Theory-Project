using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool closeEnough;
    public Canvas infoCanvas;


    public void ShowInfo()
    {
        
    }
    
    public void HideInfo()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            closeEnough = true;
            infoCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            closeEnough = false;
            infoCanvas.gameObject.SetActive(false);
        }
    }

}