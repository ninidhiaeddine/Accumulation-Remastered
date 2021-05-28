using UnityEngine;

namespace ScoreManagement
{
    public class ScoreCounter : MonoBehaviour, IEventListener
    {
        public int Score { get; private set; }

        // singleton:
        public static ScoreCounter Instance { get; private set; }

        // helper variable:
        private bool isGameOver = true;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            InitializeEventListeners();
        }

        // helper methods:

        private void IncrementScore()
        {
            Score++;
        }

        // event Handler:

        private void HandleDroppedAndSlicedEvent(GameObject staticCube, GameObject fallingCube)
        {
            if (!isGameOver)
                IncrementScore();
        }

        private void HandleGameOverEvent()
        {
            isGameOver = true;
        }

        // interface method:

        public void InitializeEventListeners()
        {
            GameEvents.DroppedAndSlicedEvent.AddListener(HandleDroppedAndSlicedEvent);
            GameEvents.GameOverEvent.AddListener(HandleGameOverEvent);
        }
    }
}


