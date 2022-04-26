using System;
using TMPro;
using UnityEngine;

public class InformationUnit : MonoBehaviour
{
    [SerializeField] private Canvas infoCanvasPrefab;
    [SerializeField] private string infoText;
    
    protected bool CloseEnough { get; private set; }
    protected bool ShouldShowInfo { private get; set; }
    private Canvas _instantiatedCanvas;

    protected void Awake()
    {
        ShouldShowInfo = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CloseEnough = true;
    }

    private void OnTriggerExit(Collider other)
    {
        CloseEnough = false;
    }

    public void UpdateInformationPanel(Camera lookedAtBy)
    {
        if (CloseEnough && ShouldShowInfo)
        {
            ShowInfo(lookedAtBy);
        }
        else
        {
            HideInfo();
        }
    }

    protected void ShowInfo(Camera playerCamera)
    {
        Debug.Log("ShowInfo");
        infoCanvasPrefab.transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward);
        infoCanvasPrefab.transform.position = Vector3.up * 3;
        
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

    protected void HideInfo()
    {
        if (_instantiatedCanvas != null)
        {
            Destroy(_instantiatedCanvas.gameObject);
        }

    }
}