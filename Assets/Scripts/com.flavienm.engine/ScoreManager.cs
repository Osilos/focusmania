using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.flavienm.engine
{
    public class ScoreManager : EngineObject
    {
        public delegate void ScoreManagerEvent(int value);
        public static ScoreManagerEvent UpdateScore;

        private int score;

        private void Start()
        {
            ScoreObject.Score += AddScore;
            DispatchUpdateScore();
        }

        private void AddScore (int value)
        {
            score += value;
            DispatchUpdateScore();
        }
        
        private void DispatchUpdateScore()
        {
            if (UpdateScore != null)
            {
                UpdateScore(score);
            }
        }

        protected override void OnNewGame()
        {
            base.OnNewGame();
            score = 0;
            DispatchUpdateScore();
        }
    }
}