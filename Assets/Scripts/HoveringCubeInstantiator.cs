using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringCubeInstantiator : MonoBehaviour
{
    public Vector3 offsetFromDroppedCube;
    public RuntimeAnimatorController hoveringCubeAnimator;

    [HideInInspector]
    public int AnimationIndex { get; private set; }

    // singleton:
    public static HoveringCubeInstantiator instance;

    private void Awake()
    {
        // enforce singleton:
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        // add event listener:
        GameEvents.DroppedAndSlicedEvent.AddListener(HandleDroppedAndSlicedEvent);
    }

    // event handlers:

    private void HandleDroppedAndSlicedEvent(GameObject staticCube, GameObject fallingCube)
    {
        GameObject newHoveringCubeParent = InstantiateHoveringCube(staticCube);
        AttachComponentsToHoveringCube(newHoveringCubeParent);
        UpdateHoveringCubeReference(newHoveringCubeParent);
    }

    // helper methods:

    private GameObject InstantiateHoveringCube(GameObject staticCube)
    {
        // retrieve position of dropped cube:
        Vector3 droppedCubePos = staticCube.transform.position;

        // calculate new position and scale based on settings:
        Vector3 newPos = droppedCubePos + offsetFromDroppedCube;
        Vector3 newScale = staticCube.transform.localScale;

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
        AnimationIndex = Random.Range(0, 4);
        animator.SetInteger("index", AnimationIndex);
    }

    private void UpdateHoveringCubeReference(GameObject hoveringCubeParent)
    {
        GameEvents.UpdateHoveringCubeReferenceEvent.Invoke(hoveringCubeParent);
    }
}
