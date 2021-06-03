using UnityEngine;

public class PerfectDropsCounter : MonoBehaviour, IEventHandler
{
    // references:
    public PlayerManager playerManager;

    public int Counter { get; private set; }

    private void Start()
    {
        InitializeEventHandlers();
    }

    // helper methods:

    private void IncrementCounter()
    {
        this.Counter++;
    }

    private void ResetCounter()
    {
        this.Counter = 0;
    }

    // event handlers:

    private void PerfectDropEventHandler(GameObject droppedCube, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            // perfect drop detected:
            IncrementCounter();

            // invoke event:
            GameEvents.PerfectDropCounterUpdatedEvent.Invoke(this.Counter, playerManager.playerToLookFor);
        }
    }

    private void SlicedEventHandler(GameObject staticCube, GameObject fallingCube, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            // slicing took place:
            ResetCounter();

            // invoke event:
            GameEvents.PerfectDropCounterUpdatedEvent.Invoke(this.Counter, playerManager.playerToLookFor);
        }
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.PerfectDropEvent.AddListener(PerfectDropEventHandler);
        GameEvents.SlicedEvent.AddListener(SlicedEventHandler);
    }
}
