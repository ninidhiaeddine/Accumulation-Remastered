using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectDropsCounter : MonoBehaviour, IEventListener
{
    public int Counter { get; private set; }

    // singleton:
    public static PerfectDropsCounter Instance { get; private set; }

    private void Awake()
    {
        // enforce singleton:
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        InitializeEventListeners();
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

    private void HandleDistanceApproximatedEvent(GameObject droppedCube)
    {
        // perfect drop detected:
        IncrementCounter();

        // invoke event:
        GameEvents.PerfectDropCounterUpdatedEvent.Invoke(this.Counter);
    }

    private void HandleDroppedAndSlicedEvent(GameObject staticCube, GameObject fallingCube)
    {
        if (staticCube != null && fallingCube != null)
        {
            // slicing took place:
            ResetCounter();

            // invoke event:
            GameEvents.PerfectDropCounterUpdatedEvent.Invoke(this.Counter);
        }
    }

    // interface methods:

    public void InitializeEventListeners()
    {
        GameEvents.DistanceApproximatedEvent.AddListener(HandleDistanceApproximatedEvent);
        GameEvents.DroppedAndSlicedEvent.AddListener(HandleDroppedAndSlicedEvent);
    }
}
