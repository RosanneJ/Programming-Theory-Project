using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] Canvas infoCanvasPrefab;
    [SerializeField] private string infoText;

    protected bool ShouldShowInfo = true;
    
    private bool _closeEnough;
    private Canvas _instantiatedCanvas;

    public void UpdateInformationPanel(Camera playerCamera)
    {
        if (_closeEnough && ShouldShowInfo)
        {
            ShowInfo(playerCamera);
        }
        else
        {
            HideInfo();
        }
    }

    private void ShowInfo(Camera playerCamera)
    {
        infoCanvasPrefab.transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward);
        
        if (_instantiatedCanvas == null)
        {
            infoCanvasPrefab.worldCamera = playerCamera;
            infoCanvasPrefab.GetComponentInChildren<TextMeshProUGUI>().SetText(infoText);
            _instantiatedCanvas = Instantiate(infoCanvasPrefab, transform);
        }
        else
        {
            _instantiatedCanvas.transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward);
        }
    }
    
    public void HideInfo()
    {
        if (_instantiatedCanvas != null)
        {
            Destroy(_instantiatedCanvas.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _closeEnough = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _closeEnough = false;
        }
    }
}