using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] Canvas infoCanvasPrefab;
    [SerializeField] private string infoText;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    protected bool ShouldShowInfo = true;
    protected Rigidbody _rb;
    
    private bool _closeEnough;
    private Canvas _instantiatedCanvas;
    private bool _isGrounded;
    private Vector3 _velocity;

    protected void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    protected void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += GameManager.gravity * Time.deltaTime;
        _rb.AddForce(Vector3.up * _velocity.y, ForceMode.Acceleration);
    }

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