using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroppedAndCollidedEvent : UnityEvent<GameObject, GameObject, Player> { }
public class UpdatedHoveringParentReferenceEvent : UnityEvent<GameObject, Player> { }
public class SlicedEvent : UnityEvent<GameObject, GameObject, Player> { }
public class GeneratedPaletteEvent : UnityEvent<List<Color>, Player> { }
public class GameOverEvent : UnityEvent<Player> { }
public class PerfectDropEvent : UnityEvent<GameObject, Player> { }
public class PerfectDropCounterUpdatedEvent : UnityEvent<int, Player> { }

public class GameEvents : MonoBehaviour
{
    public static DroppedAndCollidedEvent DroppedAndCollidedEvent;
    public static SlicedEvent SlicedEvent;
    public static UpdatedHoveringParentReferenceEvent UpdatedHoveringParentReferenceEvent;
    public static GeneratedPaletteEvent GeneratedPaletteEvent;
    public static GameOverEvent GameOverEvent;
    public static PerfectDropEvent PerfectDropEvent;
    public static PerfectDropCounterUpdatedEvent PerfectDropCounterUpdatedEvent;

    private void Awake()
    {

        if (DroppedAndCollidedEvent == null)
            DroppedAndCollidedEvent = new DroppedAndCollidedEvent();

        if (SlicedEvent == null)
            SlicedEvent = new SlicedEvent();

        if (UpdatedHoveringParentReferenceEvent == null)
            UpdatedHoveringParentReferenceEvent = new UpdatedHoveringParentReferenceEvent();

        if (GeneratedPaletteEvent == null)
            GeneratedPaletteEvent = new GeneratedPaletteEvent();

        if (GameOverEvent == null)
            GameOverEvent = new GameOverEvent();

        if (PerfectDropEvent == null)
            PerfectDropEvent = new PerfectDropEvent();

        if (PerfectDropCounterUpdatedEvent == null)
            PerfectDropCounterUpdatedEvent = new PerfectDropCounterUpdatedEvent();
    }
}
