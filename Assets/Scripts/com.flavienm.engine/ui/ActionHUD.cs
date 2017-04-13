using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.flavienm.engine.ui
{
    public class ActionHUD : MonoBehaviour
    {
        public delegate void EventHUD();
        public static EventHUD OnPlay;
        public static EventHUD OnMenu;
        public static EventHUD OnCredits;

		public void OnPlayButton ()
        {
            if (OnPlay != null)
            {
                OnPlay();
            }
        }

        public void OnMenuButton ()
        {
            if (OnMenu != null)
            {
                OnMenu();
            }
        }

		public void OnCreditsButton()
		{
			if(OnCredits != null)
			{
				OnCredits();
			}
		}

		public void OnReplay()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		} 
	}
}