using UnityEngine;
using UnityEngine.Events;

public class DroppedAndCollidedEvent : UnityEvent<GameObject, GameObject> { }
public class UpdateHoveringCubeReferenceEvent : UnityEvent<GameObject> { }
public class DroppedAndSlicedEvent : UnityEvent<GameObject, GameObject> { }

public class GameEvents : MonoBehaviour
{
    public static DroppedAndCollidedEvent DroppedAndCollidedEvent;
    public static DroppedAndSlicedEvent DroppedAndSlicedEvent;
    public static UpdateHoveringCubeReferenceEvent UpdateHoveringCubeReferenceEvent;

    private void Awake()
    {
        if (DroppedAndCollidedEvent == null)
            DroppedAndCollidedEvent = new DroppedAndCollidedEvent();

        if (DroppedAndSlicedEvent == null)
            DroppedAndSlicedEvent = new DroppedAndSlicedEvent();

        if (UpdateHoveringCubeReferenceEvent == null)
            UpdateHoveringCubeReferenceEvent = new UpdateHoveringCubeReferenceEvent();
    }
}
