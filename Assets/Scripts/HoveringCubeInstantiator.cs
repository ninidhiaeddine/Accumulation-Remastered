using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringCubeInstantiator : MonoBehaviour
{
    public Vector3 offsetFromDroppedCube;
    public RuntimeAnimatorController hoveringCubeAnimator;

    void Start()
    {
        // add event listener:
        GameEvents.DroppedAndCollidedEvent.AddListener(HandleDroppedAndCollidedEvent);
    }

    // event handlers:

    private void HandleDroppedAndCollidedEvent(GameObject droppedCube)
    {
        GameObject newHoveringCubeParent = InstantiateHoveringCube(droppedCube);
        AttachComponentsToHoveringCube(newHoveringCubeParent);
        UpdateHoveringCubeReference(newHoveringCubeParent);
    }

    // helper methods:

    private GameObject InstantiateHoveringCube(GameObject droppedCube)
    {
        // retrieve position of dropped cube:
        Vector3 droppedCubePos = droppedCube.transform.position;

        // calculate new position and scale based on settings:
        Vector3 newPos = droppedCubePos + offsetFromDroppedCube;
        Vector3 newScale = droppedCube.transform.localScale;

        // create and return cube:
        return HoveringCubeMaker.CreateHoveringCube("Hovering", newPos, newScale);
    }

    private void AttachComponentsToHoveringCube(GameObject hoveringCubeParent)
    {
        // get child:
        GameObject hoveringCubeChild = hoveringCubeParent.transform.GetChild(0).gameObject;

        // attach behavior script:
        hoveringCubeChild.AddComponent<DroppedCubeBehavior>();

        // attach animator:
        Animator animator = hoveringCubeChild.AddComponent<Animator>();
        animator.runtimeAnimatorController = hoveringCubeAnimator;

        // randomize animations:
        RandomizeAnimation(animator);
    }

    private void RandomizeAnimation(Animator animator)
    {
        int animIndex = Random.Range(0, 2);
        animator.SetInteger("index", animIndex);
    }

    private void UpdateHoveringCubeReference(GameObject hoveringCubeParent)
    {
        GameEvents.UpdateHoveringCubeReferenceEvent.Invoke(hoveringCubeParent);
    }
}
