using UnityEngine;

public class CameraFollow : MonoBehaviour, IEventHandler
{
    // references:
    public PlayerManager playerManager;
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

    private void Start()
    {
        InitializeEventHandlers();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    // event handlers:

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
            this.Target = hoveringCubeParent.transform;
    }
}
