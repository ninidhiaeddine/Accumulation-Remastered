using UnityEngine;

namespace ScoreManagement
{
    public class BestScoreManager : MonoBehaviour
    {
        // properties:
        public int BestScore 
        {
            get 
            { 
                return PlayerPrefs.GetInt("BestScore"); 
            }
            set
            { 
                // only update best score when input is greater than existing best score:
                if (value > BestScore)
                {
                    // update best score:
                    PlayerPrefs.SetInt("BestScore", value);
                }
            }
        }

        // singleton:
        public static BestScoreManager Singleton { get; private set; }

        private void Awake()
        {
            // enforce singleton:
            if (Singleton == null)
                Singleton = this;
            else
                Destroy(this.gameObject);

            // check if first time running game:
            BestScore = PlayerPrefs.GetInt("BestScore", -1);
            if (BestScore == -1)
            {
                // first time running game:
                this.SaveInitialBestScore();
            }
        }

        private void SaveInitialBestScore()
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }
    }
}
