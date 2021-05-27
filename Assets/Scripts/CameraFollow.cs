using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        GameEvents.UpdateHoveringCubeReferenceEvent.AddListener(HandleUpdateHoveringCubeReferenceEvent);
    }

    private void HandleUpdateHoveringCubeReferenceEvent(GameObject hoveringCube)
    {
        Debug.Log("Received new target: " + hoveringCube.transform.position);
        SetTarget(hoveringCube.transform);
    }

    private void SetTarget(Transform target)
    {
        virtualCamera.Follow = target;
    }
}
