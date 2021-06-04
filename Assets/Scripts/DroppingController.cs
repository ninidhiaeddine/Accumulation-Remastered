using UnityEngine;

public class DroppingController : MonoBehaviour, IEventHandler
{
    // references:
    public PlayerManager playerManager;
    public HoveringCubeTracker hoveringCubeTracker;

    private void Start()
    {
        InitializeEventHandlers();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        InputEvents.SinglePlayerDroppedInputEvent.AddListener(SinglePlayerDroppedInputEventHandler);
        InputEvents.FirstPlayerDroppedInputEvent.AddListener(FirstPlayerDroppedInputEventHandler);
        InputEvents.SecondPlayerDroppedInputEvent.AddListener(SecondPlayerDroppedInputEventHandler);
        InputEvents.AIPlayerDroppedInputEvent.AddListener(AIPlayerDroppedInputEventHandler);
    }

    // event handlers:

    private void SinglePlayerDroppedInputEventHandler(Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            DropHoveringCube();
        }
    }

    private void FirstPlayerDroppedInputEventHandler(Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            DropHoveringCube();
        }
    }

    private void SecondPlayerDroppedInputEventHandler(Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            DropHoveringCube();
        }
    }

    private void AIPlayerDroppedInputEventHandler(Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            DropHoveringCube();
        }
    }

    // helper method:

    private void DropHoveringCube()
    {
        // get reference to the current hovering cube:
        GameObject hoveringCubeParent = hoveringCubeTracker.HoveringCubeParent;

        // get reference to the hovering parent hierarchy interface:
        IHoveringParentHierarchy hierarchy = hoveringCubeParent.GetComponent<IHoveringParentHierarchy>();

        // 1. remove animator from the hierarchy:
        hierarchy.DestroyAnimator();

        // 2. add rigidbody to the mesh (to simulate dropping physics):
        hierarchy.AddRigidbody();
    }
}
