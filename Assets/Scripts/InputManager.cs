using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerManager playerManager;
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
                InputEvents.SinglePlayerDroppedInputEvent.Invoke(playerManager.playerToLookFor);
            }
        }
    }

    private void DetectFirstPlayerDrop()
    {
        foreach (var keyBinding in firstPlayerDropKeyBindings)
        {
            if (Input.GetKeyDown(keyBinding))
            {
                InputEvents.FirstPlayerDroppedInputEvent.Invoke(playerManager.playerToLookFor);
            }
        }
    }

    private void DetectSecondPlayerDrop()
    {
        foreach (var keyBinding in secondPlayerDropKeyBindings)
        {
            if (Input.GetKeyDown(keyBinding))
            {
                InputEvents.SecondPlayerDroppedInputEvent.Invoke(playerManager.playerToLookFor);
            }
        }
    }
}
