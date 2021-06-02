using UnityEngine;

public class CameraFollow : MonoBehaviour, IEventHandler
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public Transform Target 
    {
        get
        {
            return Target;
        }
        private set 
        { 
            if (virtualCamera != null)
                virtualCamera.Follow = value; 
        } 
    }

    // singleton:
    public static CameraFollow Singleton { get; private set; }

    private void Awake()
    {
        // enforce singleton:
        if (Singleton == null)
            Singleton = this;
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
        GameEvents.SpawnedPlayerEvent.AddListener(SpawnedPlayerEventHandler);
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    // event handlers:

    private void SpawnedPlayerEventHandler(GameObject hoveringCubeParent)
    {
        this.Target = hoveringCubeParent.transform;
    }

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent)
    {
        this.Target = hoveringCubeParent.transform;
    }
}
