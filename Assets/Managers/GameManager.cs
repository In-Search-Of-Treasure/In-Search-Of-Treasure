using UnityEngine;

namespace Assets.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static bool isGamePaused;
        public static bool isGameOver;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public void PauseGame()
        {
            isGamePaused = true;
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            isGamePaused = false;
            Time.timeScale = 1;
        }

        public void GameOverActive()
        {
            isGameOver = true;
        }

        public void GameOverDeactive()
        {
            isGameOver = false;
        }
    }
}
