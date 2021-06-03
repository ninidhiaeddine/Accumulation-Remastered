using UnityEngine;

public class DifficultyManager : MonoBehaviour, IEventHandler
{
    // references:
    public PlayerManager playerManager;
    public AnimatorSpeedManager animatorSpeedManager;

    public float ratioToIncreaseAnimatorSpeed = 0.1f;

    private void Start()
    {
        InitializeEventHandlers();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.PerfectDropCounterUpdatedEvent.AddListener(PerfectDropCounterUpdatedEventHandler);
        GameEvents.SlicedEvent.AddListener(SlicedEventHandler);
    }

    // helper methods:

    private void IncreaseAnimatorSpeed(float ratio)
    {
        animatorSpeedManager.AnimationSpeed *= ratio;
    }

    private void ResetAnimatorSpeed()
    {
        animatorSpeedManager.AnimationSpeed = 1.0f;
    }

    // event handlers:

    private void PerfectDropCounterUpdatedEventHandler(int count, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            float ratio = 1.0f + count * this.ratioToIncreaseAnimatorSpeed;
            IncreaseAnimatorSpeed(ratio);
        }

    }

    private void SlicedEventHandler(GameObject staticCube, GameObject fallingCube, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
            ResetAnimatorSpeed();
    }
}
