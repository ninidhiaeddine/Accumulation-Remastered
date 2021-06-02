using UnityEngine;
using UnityEngine.Events;

public class SinglePlayerDroppedInputEvent : UnityEvent { }
public class FirstPlayerDroppedInputEvent : UnityEvent { }
public class SecondPlayerDroppedInputEvent : UnityEvent { }

public class InputEvents : MonoBehaviour
{
    public static SinglePlayerDroppedInputEvent SinglePlayerDroppedInputEvent;
    public static FirstPlayerDroppedInputEvent FirstPlayerDroppedInputEvent;
    public static SecondPlayerDroppedInputEvent SecondPlayerDroppedInputEvent;

    private void Awake()
    {
        if (SinglePlayerDroppedInputEvent == null)
            SinglePlayerDroppedInputEvent = new SinglePlayerDroppedInputEvent();

        if (FirstPlayerDroppedInputEvent == null)
            FirstPlayerDroppedInputEvent = new FirstPlayerDroppedInputEvent();

        if (SecondPlayerDroppedInputEvent == null)
            SecondPlayerDroppedInputEvent = new SecondPlayerDroppedInputEvent();
    }
}
