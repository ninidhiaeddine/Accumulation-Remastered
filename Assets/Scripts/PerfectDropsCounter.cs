using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectDropsCounter : MonoBehaviour, IEventHandler
{
    public int Counter { get; private set; }

    // singleton:
    public static PerfectDropsCounter Singleton { get; private set; }

    private void Awake()
    {
        // enforce singleton:
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(this.gameObject);
    }

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

    private void PerfectDropEventHandler(GameObject droppedCube)
    {
        // perfect drop detected:
        IncrementCounter();

        // invoke event:
        GameEvents.PerfectDropCounterUpdatedEvent.Invoke(this.Counter);
    }

    private void SlicedEventHandler(GameObject staticCube, GameObject fallingCube)
    {
        // slicing took place:
        ResetCounter();

        // invoke event:
        GameEvents.PerfectDropCounterUpdatedEvent.Invoke(this.Counter);
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.PerfectDropEvent.AddListener(PerfectDropEventHandler);
        GameEvents.SlicedEvent.AddListener(SlicedEventHandler);
    }
}
