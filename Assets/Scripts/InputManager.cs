using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public List<KeyCode> singlePlayerDropKeyBindings;
    public List<KeyCode> firstPlayerDropKeyBindings;
    public List<KeyCode> secondPlayerDropKeyBindings;

    private void Update()
    {
        DetectSinglePlayerDrop();
        DetectFirstPlayerDrop();
        DetectSecondPlayerDrop();
    }

    private void DetectSinglePlayerDrop()
    {
        foreach (var keyBinding in singlePlayerDropKeyBindings)
        {
            if (Input.GetKeyDown(keyBinding))
            {
                InputEvents.SinglePlayerDroppedInputEvent.Invoke();
            }
        }
    }

    private void DetectFirstPlayerDrop()
    {
        foreach (var keyBinding in firstPlayerDropKeyBindings)
        {
            if (Input.GetKeyDown(keyBinding))
            {
                InputEvents.FirstPlayerDroppedInputEvent.Invoke();
            }
        }
    }

    private void DetectSecondPlayerDrop()
    {
        foreach (var keyBinding in secondPlayerDropKeyBindings)
        {
            if (Input.GetKeyDown(keyBinding))
            {
                InputEvents.SecondPlayerDroppedInputEvent.Invoke();
            }
        }
    }
}
