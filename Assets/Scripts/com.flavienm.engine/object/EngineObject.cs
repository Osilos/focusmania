﻿using UnityEngine;
using System.Collections;

namespace com.flavienm.engine
{
	public abstract class EngineObject : MonoBehaviour
	{
		private void Awake ()
		{
			com.flavienm.engine.GameManager.NewGame += OnNewGame;
			com.flavienm.engine.GameManager.GameOver += OnGameOver;
			com.flavienm.engine.GameManager.Win += OnWin;
			com.flavienm.engine.GameManager.Menu += OnMenu;
			com.flavienm.engine.GameManager.Credits += OnCredits;
			com.flavienm.engine.GameManager.MarkPoint += OnMarkPoint;
		}

		protected virtual void OnMenu() { }
		protected virtual void OnCredits() { }
		protected virtual void OnGameOver() { }
		protected virtual void OnWin() { }
		protected virtual void OnNewGame() { }
		protected virtual void OnMarkPoint() { }

		void OnDestroy()
		{
			com.flavienm.engine.GameManager.NewGame -= OnNewGame;
			com.flavienm.engine.GameManager.GameOver -= OnGameOver;
			com.flavienm.engine.GameManager.Win -= OnWin;
			com.flavienm.engine.GameManager.Menu -= OnMenu;
			com.flavienm.engine.GameManager.Credits -= OnCredits;
			com.flavienm.engine.GameManager.MarkPoint -= OnMarkPoint;

		}
	}
}
