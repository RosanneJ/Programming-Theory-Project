using System;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool closeEnough;
    [SerializeField] Canvas infoCanvas;
    


    public void ShowInfo(Camera camera)
    {
        infoCanvas.transform.rotation = Quaternion.LookRotation(camera.transform.forward);
        if (!infoCanvas.isActiveAndEnabled)
        {
            infoCanvas.transform.SetParent(transform);
            
            infoCanvas.gameObject.SetActive(true);
        }
    }
    
    public void HideInfo()
    {
        if (infoCanvas.isActiveAndEnabled)
        {
            infoCanvas.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            closeEnough = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            closeEnough = false;
        }
    }
}