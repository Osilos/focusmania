﻿using UnityEngine;
using System.Collections;
using System;
using com.flavienm.engine;
using Tobii.EyeTracking;

namespace com.flavienm.engine.ui
{
    public class UIManager : EngineObject
    {
        [SerializeField]
        private GameObject startHud;
        [SerializeField]
        private GameObject gameOverHud;
        [SerializeField]
        private GameObject gameHud;
		[SerializeField]
		private GameObject creditsHud;

		private void Start()
        {
            StartCoroutine(WaitForInit());
        }

        private IEnumerator WaitForInit ()
        {
            EyeTrackingHost.GetInstance().Initialize();
            yield return new WaitForSeconds(1.1f);
            com.flavienm.engine.input.InputFactory.Create();
        }

        protected override void OnMenu()
        {
            
            startHud.SetActive(true);
            gameOverHud.SetActive(false);
            gameHud.SetActive(false);
			creditsHud.SetActive(false);
        }

		protected override void OnCredits()
		{

			startHud.SetActive(false);
			gameOverHud.SetActive(false);
			gameHud.SetActive(false);
			creditsHud.SetActive(true);

		}

		protected override void OnGameOver()
        {
            startHud.SetActive(false);
            gameOverHud.SetActive(true);
            gameHud.SetActive(false);
			creditsHud.SetActive(false);
		}

        protected override void OnNewGame()
        {
            startHud.SetActive(false);
            gameOverHud.SetActive(false);
            gameHud.SetActive(true);
			creditsHud.SetActive(false);
		}
    }
}