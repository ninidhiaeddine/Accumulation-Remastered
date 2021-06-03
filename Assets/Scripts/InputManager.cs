using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public Player playerToLookFor;
    public List<KeyCode> singlePlayerDropKeyBindings;
    public List<KeyCode> firstPlayerDropKeyBindings;
    public List<KeyCode> secondPlayerDropKeyBindings;

    private void Update()
    {
        DetectDropInput();
    }

    private void DetectDropInput()
    {
        switch (playerToLookFor)
        {
            case Player.SinglePlayer:
                DetectSinglePlayerDrop();
                break;
            case Player.Player1:
                DetectFirstPlayerDrop();
                break;
            case Player.Player2:
                DetectSecondPlayerDrop();
                break;
            default:
                break;
        }
    }

    private void DetectSinglePlayerDrop()
    {
        foreach (var keyBinding in singlePlayerDropKeyBindings)
        {
            if (Input.GetKeyDown(keyBinding))
            {
                InputEvents.SinglePlayerDroppedInputEvent.Invoke(Player.SinglePlayer);
            }
        }
    }

    private void DetectFirstPlayerDrop()
    {
        foreach (var keyBinding in firstPlayerDropKeyBindings)
        {
            if (Input.GetKeyDown(keyBinding))
            {
                InputEvents.FirstPlayerDroppedInputEvent.Invoke(Player.Player1);
            }
        }
    }

    private void DetectSecondPlayerDrop()
    {
        foreach (var keyBinding in secondPlayerDropKeyBindings)
        {
            if (Input.GetKeyDown(keyBinding))
            {
                InputEvents.SecondPlayerDroppedInputEvent.Invoke(Player.Player2);
            }
        }
    }
}
