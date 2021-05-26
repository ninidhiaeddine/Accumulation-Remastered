using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroppedAndCollidedEvent : UnityEvent<GameObject> { }
public class UpdateHoveringCubeReferenceEvent : UnityEvent<GameObject> { }

public class GameEvents : MonoBehaviour
{
    public static DroppedAndCollidedEvent DroppedAndCollidedEvent;
    public static UpdateHoveringCubeReferenceEvent UpdateHoveringCubeReferenceEvent;

    private void Awake()
    {
        if (DroppedAndCollidedEvent == null)
            DroppedAndCollidedEvent = new DroppedAndCollidedEvent();

        if (UpdateHoveringCubeReferenceEvent == null)
            UpdateHoveringCubeReferenceEvent = new UpdateHoveringCubeReferenceEvent();
    }
}
