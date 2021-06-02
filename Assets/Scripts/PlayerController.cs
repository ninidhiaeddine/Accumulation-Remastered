using UnityEngine;

public class PlayerController : MonoBehaviour, IEventHandler
{
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
    }

    // event handlers:

    private void SinglePlayerDroppedInputEventHandler()
    {
        if (transform.CompareTag("Player"))
            DropHoveringCube();
    }

    private void FirstPlayerDroppedInputEventHandler()
    {
        if (transform.CompareTag("Player1"))
            DropHoveringCube();
    }

    private void SecondPlayerDroppedInputEventHandler()
    {
        if (transform.CompareTag("Player2"))
            DropHoveringCube();
    }

    // helper method:

    private void DropHoveringCube()
    {
        // get reference to the hovering parent hierarchy interface:
        IHoveringParentHierarchy hierarchy = GetComponent<IHoveringParentHierarchy>();

        // 1. remove animator from the hierarchy:
        hierarchy.DestroyAnimator();

        // 2. add rigidbody to the mesh (to simulate dropping physics):
        hierarchy.AddRigidbody();
    }
}
