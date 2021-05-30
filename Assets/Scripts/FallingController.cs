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
        // get reference to the hovering cube parent:
        GameObject hoveringCubeParent = HoveringCubeTracker.Instance.HoveringCubeParent;

        // get reference to the hovering parent hierarchy:
        HoveringParentHierarchy hierarchy = hoveringCubeParent.GetComponent<HoveringParentHierarchy>();

        // 1. remove animator from the hierarchy:
        Destroy(hierarchy.Animator);

        // 2. add rigidbody to the mesh (to simulate dropping physics):
        if (hierarchy.Rigidbody == null)
        {
            hierarchy.MeshContainer.AddComponent<Rigidbody>();
        }
    }
}
