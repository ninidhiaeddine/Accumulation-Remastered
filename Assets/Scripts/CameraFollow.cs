using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour, IEventListener
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
        InitializeEventListeners();
    }

    // interface methods:

    public void InitializeEventListeners()
    {
        GameEvents.UpdateHoveringCubeReferenceEvent.AddListener(HandleUpdateHoveringCubeReferenceEvent);
    }

    // helper methods:

    private void SetTarget(Transform target)
    {
        virtualCamera.Follow = target;
    }

    // event handlers:

    private void HandleUpdateHoveringCubeReferenceEvent(GameObject hoveringCube)
    {
        SetTarget(hoveringCube.transform);
    }
}
