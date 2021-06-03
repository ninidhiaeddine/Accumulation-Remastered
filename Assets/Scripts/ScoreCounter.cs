using UnityEngine;

namespace ScoreManagement
{
    public class ScoreCounter : MonoBehaviour, IEventHandler
    {
        // references:
        public PlayerManager playerManager;

        public int Score { get; private set; }

        // helper variable:
        private bool isGameOver = true;

        private void Start()
        {
            InitializeEventHandlers();
        }

        // helper methods:

        private void IncrementScore()
        {
            Score++;
        }

        // event Handler:

        private void SlicedEventHandler(GameObject staticCube, GameObject fallingCube, Player sender)
        {
            if (playerManager.EventShouldBeApproved(sender))
            {
                if (!isGameOver)
                    IncrementScore();
            }
        }

        private void GameOverEventHandler(Player sender)
        {
            if (playerManager.EventShouldBeApproved(sender))
                isGameOver = true;
        }

        // interface method:

        public void InitializeEventHandlers()
        {
            GameEvents.SlicedEvent.AddListener(SlicedEventHandler);
            GameEvents.GameOverEvent.AddListener(GameOverEventHandler);
        }
    }
}


