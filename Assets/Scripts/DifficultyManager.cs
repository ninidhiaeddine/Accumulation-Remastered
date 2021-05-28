using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public float animationSpeedRatio = 1.0f;
    public Animator hoveringAnimator;

    private void Start()
    {
        SetAnimationSpeed();

        // add event listeners:
        GameEvents.UpdateHoveringCubeReferenceEvent.AddListener(HandleUpdateHoveringCubeReferenceEvent);
    }

    private void OnValidate()
    {
        SetAnimationSpeed();
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
