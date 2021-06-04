using UnityEngine;
using UnityEngine.Events;

public class SinglePlayerDroppedInputEvent : UnityEvent<Player> { }
public class FirstPlayerDroppedInputEvent : UnityEvent<Player> { }
public class SecondPlayerDroppedInputEvent : UnityEvent<Player> { }
public class AIPlayerDroppedInputEvent : UnityEvent<Player> { }

public class InputEvents : MonoBehaviour
{
    public static SinglePlayerDroppedInputEvent SinglePlayerDroppedInputEvent;
    public static FirstPlayerDroppedInputEvent FirstPlayerDroppedInputEvent;
    public static SecondPlayerDroppedInputEvent SecondPlayerDroppedInputEvent;
    public static AIPlayerDroppedInputEvent AIPlayerDroppedInputEvent;

    private void Awake()
    {
        if (SinglePlayerDroppedInputEvent == null)
            SinglePlayerDroppedInputEvent = new SinglePlayerDroppedInputEvent();

        if (FirstPlayerDroppedInputEvent == null)
            FirstPlayerDroppedInputEvent = new FirstPlayerDroppedInputEvent();

        if (SecondPlayerDroppedInputEvent == null)
            SecondPlayerDroppedInputEvent = new SecondPlayerDroppedInputEvent();

        if (AIPlayerDroppedInputEvent == null)
            AIPlayerDroppedInputEvent = new AIPlayerDroppedInputEvent();
    }
}
