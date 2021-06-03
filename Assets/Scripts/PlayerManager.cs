using UnityEngine;

public enum Player
{
    SinglePlayer,
    Player1,
    Player2
};

public class PlayerManager : MonoBehaviour
{
    public Player playerToLookFor;
    public string TagToLookFor { get => GetTag(playerToLookFor); }

    private string GetTag(Player player)
    {
        switch (player)
        {
            case Player.SinglePlayer:
                return "Player";
            case Player.Player1:
                return "Player1";
            case Player.Player2:
                return "Player2";
            default:
                throw new System.Exception("Unexpected Player Enum Value!");
        }
    }

    public bool TagIsMatching(string tag)
    {
        return (tag.CompareTo(TagToLookFor) == 0);
    }

    public bool EventShouldBeApproved(Player sender)
    {
        return (sender == playerToLookFor);
    }
}
