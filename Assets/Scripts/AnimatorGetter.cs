using UnityEngine;

public class AnimatorGetter : MonoBehaviour, IEventHandler
{
    // references:
    public PlayerManager playerManager;
    public AnimatorSpeedManager animatorSpeedManager;

    // helper variables:
    private Animator animator;

    private void Start()
    {
        InitializeEventHandlers();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.SpawnedPlayerEvent.AddListener(SpawnedPlayerEventHandler);
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    // helper methods:

    private void PassAnimatorReference()
    {
        animatorSpeedManager.animator = this.animator;
    }

    // event handlers:

    private void SpawnedPlayerEventHandler(GameObject spawnedPlayer, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            // get hierarchy:
            IHoveringParentHierarchy hierarchy = spawnedPlayer.GetComponent<IHoveringParentHierarchy>();

            // update reference to animator:
            this.animator = hierarchy.Animator;

            // pass reference:
            PassAnimatorReference();
        }
    }

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
