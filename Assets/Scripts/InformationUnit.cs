using System;
using TMPro;
using UnityEngine;

public class InformationUnit : MonoBehaviour
{
    [SerializeField] private Canvas infoCanvasPrefab;
    [SerializeField] private string infoText;
    
    // ENCAPSULATION
    private bool PlayerEnteredTrigger { get; set; }
    protected bool ShouldShowInfo { private get; set; }
    private Canvas _instantiatedCanvas;

    protected void Awake()
    {
        ShouldShowInfo = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerEnteredTrigger = true;   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerEnteredTrigger = false;   
        }
    }

    public void UpdateInformationPanel(Camera lookedAtBy)
    {
        if (PlayerEnteredTrigger && ShouldShowInfo)
        {
            ShowInfo(lookedAtBy);
        }
        else
        {
            HideInfo();
        }
    }

    private void ShowInfo(Camera playerCamera)
    {
        var infoCanvasRotation = Quaternion.LookRotation(playerCamera.transform.forward);
        if (_instantiatedCanvas == null)
        {
            infoCanvasPrefab.renderMode = RenderMode.WorldSpace;
            infoCanvasPrefab.worldCamera = playerCamera;
            
            var infoCanvasText = infoCanvasPrefab.GetComponentInChildren<TextMeshProUGUI>();
            infoCanvasText.SetText(infoText);
            
            var infoCanvasPosition = transform.position + Vector3.up * 1;
            _instantiatedCanvas = Instantiate(infoCanvasPrefab, infoCanvasPosition, infoCanvasRotation, transform);
        }
        else
        {
            _instantiatedCanvas.transform.rotation = infoCanvasRotation;
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