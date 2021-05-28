using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    // singleton:
    public static CameraFollow instance;

    private void Awake()
    {
        // enforce singleton:
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        GameEvents.UpdateHoveringCubeReferenceEvent.AddListener(HandleUpdateHoveringCubeReferenceEvent);
    }

    private void HandleUpdateHoveringCubeReferenceEvent(GameObject hoveringCube)
    {
        SetTarget(hoveringCube.transform);
    }

    private void SetTarget(Transform target)
    {
        virtualCamera.Follow = target;
    }
}
