using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInstantiator : MonoBehaviour
{
    public Vector3 offsetFromDroppedCube;
    public RuntimeAnimatorController cubeHoveringAnimator;

    void Start()
    {
        // add event listener:
        GameEvents.DroppedAndCollidedEvent.AddListener(HandleDroppedAndCollidedEvent);
    }

    private void HandleDroppedAndCollidedEvent(GameObject droppedCube)
    {
        GameObject newHoveringCube = InstantiateHoveringCube(droppedCube);
        AttachComponentsToHoveringCube(newHoveringCube);
        UpdateHoveringCubeReference(newHoveringCube);
    }

    private GameObject InstantiateHoveringCube(GameObject droppedCube)
    {
        // retrieve position of dropped cube:
        Vector3 droppedCubePos = droppedCube.transform.position;

        // calculate new position and scale based on settings:
        Vector3 newPos = droppedCubePos + offsetFromDroppedCube;
        Vector3 newScale = droppedCube.transform.localScale;

        // create and return cube:
        return CubeMaker.CreateCube("Hovering", newPos, newScale);
    }

    private void AttachComponentsToHoveringCube(GameObject hoveringCube)
    {
        // attach behavior script:
        hoveringCube.AddComponent<DroppedCubeBehavior>();

        // attach animator:
        Animator animator = hoveringCube.AddComponent<Animator>();
        animator.runtimeAnimatorController = cubeHoveringAnimator;

        // randomize animations:
        RandomizeAnimation(animator);
    }

    private void RandomizeAnimation(Animator animator)
    {
        int animIndex = Random.Range(0, 2);
        animator.SetInteger("index", animIndex);
    }

    private void UpdateHoveringCubeReference(GameObject hoveringCube)
    {
        GameEvents.UpdateHoveringCubeReferenceEvent.Invoke(hoveringCube);
    }
}
