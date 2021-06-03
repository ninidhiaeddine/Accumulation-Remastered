using UnityEngine;

public class AnimatorGetter : MonoBehaviour, IEventHandler
{
    // references:
    public PlayerManager playerManager;
    public AnimatorSpeedManager animatorSpeedManager;

    public Animator initalAnimator;

    // helper variables:
    private Animator animator;

    private void Start()
    {
        InitializeAnimatorReference();
        InitializeEventHandlers();
    }

    private void InitializeAnimatorReference()
    {
        this.animator = initalAnimator;
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    // helper methods:

    private void PassAnimatorReference()
    {
        animatorSpeedManager.animator = this.animator;
    }

    // event handlers:

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            // get reference to hierarchy:
            IHoveringParentHierarchy hierarchy = hoveringCubeParent.GetComponent<IHoveringParentHierarchy>();

            // update reference to animator:
            this.animator = hierarchy.Animator;

            // pass reference:
            PassAnimatorReference();
        }
    }
}
