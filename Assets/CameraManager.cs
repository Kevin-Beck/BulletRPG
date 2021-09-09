using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera previewCamera;
    [SerializeField] CinemachineVirtualCamera introCamera;
    [SerializeField] CinemachineVirtualCamera gameCamera;


    private void Awake()
    {
        gameCamera.enabled = false;
        introCamera.enabled = false;
        previewCamera.enabled = false;
        ResetCameras();
        previewCamera.enabled = true;
        introCamera.enabled = true;
        gameCamera.enabled = true;
    }
    private void Start()
    {
        previewCamera.Priority = 1;
    }

    public void SwitchToPreviewCamera()
    {
        ResetCameras();
        previewCamera.Priority = 1;
    }
    public void SwitchToIntroCamera()
    {
        ResetCameras();
        introCamera.Priority = 1;
        introCamera.Follow.gameObject.GetComponent<CinemachineDollyCart>().enabled = true;
    }
    public void SwitchToGameCamera()
    {
        ResetCameras();
        gameCamera.Priority = 1;
    }
    private void ResetCameras()
    {
        previewCamera.Priority = 0;
        introCamera.Priority = 0;
        gameCamera.Priority = 0;
    }
}
