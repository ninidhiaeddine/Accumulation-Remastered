using UnityEngine;

public class HoveringCubeTracker : MonoBehaviour, IEventHandler
{
    // references:
    public PlayerManager playerManager;

    [SerializeField]
    private GameObject initialHoveringCubeParent;

    [HideInInspector]
    public GameObject HoveringCubeParent { get; private set; }

    private void Start()
    {
        InitializeParentReference();
        InitializeEventHandlers();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    // helper methods:

    private void InitializeParentReference()
    {
        this.HoveringCubeParent = initialHoveringCubeParent;
    }

    private void UpdateParentReference(GameObject hoveringCubeParent)
    {
        this.HoveringCubeParent = hoveringCubeParent;
    }

    // event handlers:

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
            UpdateParentReference(hoveringCubeParent);
    }
}
