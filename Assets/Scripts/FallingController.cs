using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingController : MonoBehaviour
{
    // helpers variables:
    public GameObject hoveringCube;
    private Animator hoverinCubeAnimator;

    private int droppedCounter = 0;

    private void Start()
    {
        InitializeVariables();
        InitializeEventListeners();
    }

    private void Update()
    {
        HandleInput();
    }

    // helper methods:

    private void InitializeVariables()
    {
        this.hoverinCubeAnimator = this.hoveringCube.GetComponent<Animator>();
    }

    private void InitializeEventListeners()
    {
        GameEvents.DroppedAndCollidedEvent.AddListener(HandleDroppedAndCollidedEvent);
        GameEvents.UpdateHoveringCubeReferenceEvent.AddListener(HandleUpdateHoveringCubeReferenceEvent);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            DropHoveringCube();
    }

    private void DropHoveringCube()
    {
        // remove animator:
        Destroy(hoverinCubeAnimator);

        // add rigidbody to simulate dropping physics:
        hoveringCube.AddComponent<Rigidbody>();
    }

    private void UpdateHoveringCube(GameObject newHoveringCube)
    {
        this.hoveringCube = newHoveringCube;
        this.hoverinCubeAnimator = this.hoveringCube.GetComponent<Animator>();
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

    private void HandleUpdateHoveringCubeReferenceEvent(GameObject newHoveringCube)
    {
        this.UpdateHoveringCube(newHoveringCube);
    }
}
