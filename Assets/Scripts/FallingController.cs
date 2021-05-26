using UnityEngine;

public class FallingController : MonoBehaviour
{
    private void Update()
    {
        HandleInput();
    }

    // helper methods:

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DropHoveringCube();
    }

    private void DropHoveringCube()
    {
        // get reference to the child:
        GameObject hoveringCubeChild = HoveringCubeTracker.instance.Child;

        // 1. remove animator:
        Animator animator = hoveringCubeChild.GetComponent<Animator>();
        Destroy(animator);

        // 2. add rigidbody to simulate dropping physics:
        hoveringCubeChild.AddComponent<Rigidbody>();
    }
}
