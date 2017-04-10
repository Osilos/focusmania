using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.flavienm.engine.ui
{
    [RequireComponent(typeof(Text))]
    public class DisplayScore : EngineObject
    {
        private Text text;

        private void Start()
        {
            text = GetComponent<Text>();
            UpdateText(0);
            ScoreManager.UpdateScore += UpdateText;
        }

        private void UpdateText (int value)
        {
            text.text = value.ToString();
        }

        protected override void OnNewGame()
        {
            base.OnNewGame();
            UpdateText(0);
        }
    }
}