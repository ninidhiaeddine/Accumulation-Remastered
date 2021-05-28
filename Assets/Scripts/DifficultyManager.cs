using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour, IEventListener
{
    // settings:
    public float animationSpeedRatio = 1.0f;
    public Animator hoveringAnimator;

    // singleton:
    public static DifficultyManager instance;

    private void Awake()
    {
        // enforce singleton:
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        SetAnimationSpeed();
        InitializeEventListeners();
    }

    private void OnValidate()
    {
        SetAnimationSpeed();
    }

    // interface methods:

    public void InitializeEventListeners()
    {
        GameEvents.UpdateHoveringCubeReferenceEvent.AddListener(HandleUpdateHoveringCubeReferenceEvent);
    }

    // helper methods:

    private void SetAnimationSpeed()
    {
        hoveringAnimator.speed = animationSpeedRatio;
    }

    private void UpdateAnimatorReference(Animator newAnimator)
    {
        // update reference:
        hoveringAnimator = newAnimator;

        // set speed again:
        SetAnimationSpeed();
    }

    // event handlers:

    private void HandleUpdateHoveringCubeReferenceEvent(GameObject hoveringCubeParent)
    {
        // retrieve child:
        GameObject child = hoveringCubeParent.transform.GetChild(0).gameObject;

        // get animator reference
        Animator animator = child.GetComponent<Animator>();

        // update reference:
        UpdateAnimatorReference(animator);
    }
}
