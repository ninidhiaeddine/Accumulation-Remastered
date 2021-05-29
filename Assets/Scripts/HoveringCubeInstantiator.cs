using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringCubeInstantiator : MonoBehaviour, IEventListener
{
    public Vector3 offsetFromDroppedCube;
    public RuntimeAnimatorController hoveringCubeAnimator;

    [HideInInspector]
    public int AnimationIndex { get; private set; }

    // helper variable:
    private bool isGameOver = false;

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
        InitializeEventListeners();
    }

    // interface methods:

    public void InitializeEventListeners()
    {
        GameEvents.DroppedAndSlicedEvent.AddListener(HandleDroppedAndSlicedEvent);
        GameEvents.PerfectDropEvent.AddListener(HandlePerfectDropEvent);
        GameEvents.GameOverEvent.AddListener(HandleGameOverEvent);
    }

    // event handlers:

    private void HandleDroppedAndSlicedEvent(GameObject staticCube, GameObject fallingCube)
    {
        // only instantiate new hovering cubes when game is not over:
        if (!isGameOver)
        {
            GameObject newHoveringCubeParent = InstantiateHoveringCube(staticCube);
            AttachComponentsToHoveringCube(newHoveringCubeParent);
            UpdateHoveringCubeReference(newHoveringCubeParent);
        }
    }

    private void HandlePerfectDropEvent(GameObject staticCube)
    {
        // only instantiate new hovering cubes when game is not over:
        if (!isGameOver)
        {
            GameObject newHoveringCubeParent = InstantiateHoveringCube(staticCube);
            AttachComponentsToHoveringCube(newHoveringCubeParent);
            UpdateHoveringCubeReference(newHoveringCubeParent);
        }
    }

    private void HandleGameOverEvent()
    {
        this.isGameOver = true;
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
