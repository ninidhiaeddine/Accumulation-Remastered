using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingController : MonoBehaviour
{
    // helpers variables:
    private int droppedCounter = 0;

    private void Start()
    {
        InitializeEventListeners();
    }

    private void Update()
    {
        HandleInput();
    }

    // helper methods:

    private void InitializeEventListeners()
    {
        GameEvents.DroppedAndCollidedEvent.AddListener(HandleDroppedAndCollidedEvent);
    }

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

    private void RenameCube(GameObject cube, string name)
    {
        cube.name = name;
    }

    // event handlers:

    private void HandleDroppedAndCollidedEvent(GameObject droppedCube)
    {
        // increment counter:
        this.droppedCounter++;

        // rename cube:
        string newName = $"Dropped{this.droppedCounter + 1}";
        RenameCube(droppedCube, newName);
    }
}
