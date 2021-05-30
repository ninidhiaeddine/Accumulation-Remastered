using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour, IEventHandler
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public float minOrthographicSize = 7.0f;
    public float maxOrthographicSize = 20.0f;
    public float sizeTransitionDuration = 1.0f;

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
        InitializeEventHandlers();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    // helper methods:

    private void SetTarget(Transform target)
    {
        virtualCamera.Follow = target;
    }

    // event handlers:

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent)
    {
        SetTarget(hoveringCubeParent.transform);
    }
}
