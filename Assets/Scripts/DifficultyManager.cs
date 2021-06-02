using UnityEngine;

public class DifficultyManager : MonoBehaviour, IEventHandler
{
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
        AnimatorSpeedManager.Singleton.AnimationSpeed *= ratio;
    }

    private void ResetAnimatorSpeed()
    {
        AnimatorSpeedManager.Singleton.AnimationSpeed = 1.0f;
    }

    // event handlers:

    private void PerfectDropCounterUpdatedEventHandler(int count)
    {
        float ratio = 1.0f + count * this.ratioToIncreaseAnimatorSpeed;
        IncreaseAnimatorSpeed(ratio);
    }

    private void SlicedEventHandler(GameObject staticCube, GameObject fallingCube)
    {
        ResetAnimatorSpeed();
    }
}
