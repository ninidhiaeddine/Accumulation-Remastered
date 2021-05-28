using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroppedAndCollidedEvent : UnityEvent<GameObject, GameObject> { }
public class UpdateHoveringCubeReferenceEvent : UnityEvent<GameObject> { }
public class DroppedAndSlicedEvent : UnityEvent<GameObject, GameObject> { }
public class GeneratedPaletteEvent : UnityEvent<List<Color>> { }
public class GameOverEvent : UnityEvent { }
public class DistanceApproximatedEvent : UnityEvent<GameObject> { }
public class PerfectDropCounterUpdatedEvent : UnityEvent<int> { }

public class GameEvents : MonoBehaviour
{
    public static DroppedAndCollidedEvent DroppedAndCollidedEvent;
    public static DroppedAndSlicedEvent DroppedAndSlicedEvent;
    public static UpdateHoveringCubeReferenceEvent UpdateHoveringCubeReferenceEvent;
    public static GeneratedPaletteEvent GeneratedPaletteEvent;
    public static GameOverEvent GameOverEvent;
    public static DistanceApproximatedEvent DistanceApproximatedEvent;
    public static PerfectDropCounterUpdatedEvent PerfectDropCounterUpdatedEvent;

    private void Awake()
    {
        if (DroppedAndCollidedEvent == null)
            DroppedAndCollidedEvent = new DroppedAndCollidedEvent();

        if (DroppedAndSlicedEvent == null)
            DroppedAndSlicedEvent = new DroppedAndSlicedEvent();

        if (UpdateHoveringCubeReferenceEvent == null)
            UpdateHoveringCubeReferenceEvent = new UpdateHoveringCubeReferenceEvent();

        if (GeneratedPaletteEvent == null)
            GeneratedPaletteEvent = new GeneratedPaletteEvent();

        if (GameOverEvent == null)
            GameOverEvent = new GameOverEvent();

        if (DistanceApproximatedEvent == null)
            DistanceApproximatedEvent = new DistanceApproximatedEvent();

        if (PerfectDropCounterUpdatedEvent == null)
            PerfectDropCounterUpdatedEvent = new PerfectDropCounterUpdatedEvent();
    }
}
