using UnityEngine;

public class HoveringCubeTracker : MonoBehaviour, IEventHandler
{
    // references:
    public PlayerManager playerManager;

    [SerializeField]
    private GameObject initialHoveringCubeParent;
    private GameObject initialCubeBeneathHoveringCube;

    [HideInInspector]
    public GameObject HoveringCubeParent { get; private set; }
    public GameObject CubeBeneathHoveringCube { get; private set; }

    private void Start()
    {
        InitializeCubesReferences();
        InitializeEventHandlers();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
        GameEvents.SlicedEvent.AddListener(SlicedEventHandler);
        GameEvents.PerfectDropEvent.AddListener(PerfectDropEventHandler);
    }

    // helper methods:

    private void InitializeCubesReferences()
    {
        this.HoveringCubeParent = initialHoveringCubeParent;
        this.CubeBeneathHoveringCube = initialCubeBeneathHoveringCube;
    }

    // event handlers:

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
            this.HoveringCubeParent = hoveringCubeParent;
    }

    private void SlicedEventHandler(GameObject staticCube, GameObject fallingCube, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
            this.CubeBeneathHoveringCube = staticCube;
    }

    private void PerfectDropEventHandler(GameObject staticCube, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
            this.CubeBeneathHoveringCube = staticCube;
    }
}
