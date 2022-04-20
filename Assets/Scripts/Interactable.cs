using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool closeEnough;
    [SerializeField] Canvas infoCanvasPrefab;
    [SerializeField] private string infoText;

    private Canvas _instantiatedCanvas;

    public void ShowInfo(Camera camera)
    {
        infoCanvasPrefab.transform.rotation = Quaternion.LookRotation(camera.transform.forward);
        
        if (_instantiatedCanvas == null)
        {
            infoCanvasPrefab.worldCamera = camera;
            infoCanvasPrefab.GetComponentInChildren<TextMeshProUGUI>().SetText(infoText);
            _instantiatedCanvas = Instantiate(infoCanvasPrefab, transform);
        }
        else
        {
            _instantiatedCanvas.transform.rotation = Quaternion.LookRotation(camera.transform.forward);
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