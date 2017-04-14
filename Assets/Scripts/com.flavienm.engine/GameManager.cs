using UnityEngine;
using System.Collections;
using com.flavienm.engine.ui;

namespace com.flavienm.engine
{
	public class GameManager : MonoBehaviour
	{
		public delegate void GameEvent();
		public delegate void GameValueEvent(int value);

		public static bool isSoundEnabel = true;
		private static float difficulty = 0f;
		private static float minDifficulty = 1f;
		private static float maxdifficulty = 2f;

		public static GameEvent NewGame;
		public static GameEvent GameOver;
		public static GameEvent Win;
		public static GameEvent Menu;
		public static GameEvent Credits;
		public static GameEvent BlowBombs;
		public static GameEvent MarkPoint;

		public AnimationCurve difficultyCurve;

		[SerializeField]
		private AudioSource loseSound;
		[SerializeField]
		private AudioSource markSound;
		
		private float increaseDifficultyStep = 0.03f;
		private float currentDifficulty = 0f;

		public static float GetCurrentDifficulty ()
		{
			return Mathf.Lerp(minDifficulty, maxdifficulty, difficulty);
		}

		void Start()
		{
			Application.targetFrameRate = 30;
			
			com.flavienm.engine.Player.Win += OnPlayerWin;
			com.flavienm.engine.Player.Lose += OnPlayerLose;
			com.flavienm.engine.Player.MarkPoint += OnPlayerMark;
			ActionHUD.OnPlay += StartGame;
			ActionHUD.OnMenu += GoMenu;
			ActionHUD.OnCredits += GoCredits;
			DispatchMenuEvent();
		}

		public void LaunchBlowBombs()
		{
			BlowBombs();
		}

		public void OnPlayerMark ()
		{
			if (MarkPoint != null)
			{
				MarkPoint();
			}
		}

		public void GoMenu()
		{
			DispatchMenuEvent();
		}

		public void GoCredits()
		{
			DispatchCreditsEvent();
		}

		public void StartGame()
		{
			
			DispatchNewGameEvent();
		}

		private void OnPlayerWin()
		{
			//Anim player -> anim victoire
			//if (isSoundEnabel)
			//loseSound.Play();

			DispatchWinEvent();
		}

		private void OnPlayerLose()
		{
			LaunchBlowBombs();
			// FAIRE TOUT EXPLOSER
			//            if (isSoundEnabel)
			//                loseSound.Play();

			DispatchGameOverEvent();
		}

		private void DispatchNewGameEvent()
		{
			if (NewGame != null)
			{
				NewGame();
			}
		}
		private void DispatchGameOverEvent()
		{
			if (GameOver != null)
			{
				GameOver();
			}
		}

		private void DispatchWinEvent()
		{
			if(Win != null)
			{
				Win();
			}
		}

		private void DispatchMenuEvent()
		{
			if (Menu != null)
			{
				Menu();
			}
		}

		private void DispatchCreditsEvent()
		{
			if(Credits != null)
			{
				Credits();
			}
		}

		private void IncreaseDifficulty ()
		{
			currentDifficulty += increaseDifficultyStep;
			difficulty = difficultyCurve.Evaluate(currentDifficulty);
		}
	}
}