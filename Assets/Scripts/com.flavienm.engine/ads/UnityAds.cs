using System.Collections;
using UnityEngine;
/*
using UnityEngine.Advertisements;

namespace com.flavienm.engine.ads
{
    public class UnityAds : MonoBehaviour
    {
        private static readonly int neededCallToShow = 5;

        private static int displayCount;
        private static int callCount;

        public void Start()
        {
            callCount = 2;
            if (PlayerPrefs.HasKey("displayAdCount"))
            {
                displayCount = PlayerPrefs.GetInt("displayAdCount");
                callCount = displayCount;
            }
        }

        public void CallAd()
        {
            if (callCount++ % neededCallToShow == 0)
            {
                StartCoroutine(Show());
            }
        }

        private IEnumerator Show()
        {
            yield return new WaitForSeconds(0.5f);
            ShowAd();

        }

        private void ShowAd()
        {
            if (Advertisement.IsReady())
            {
                displayCount++;
                Advertisement.Show();
                SaveData();
            }
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt("displayAdCount", displayCount);
            PlayerPrefs.Save();
        }
    }
}*/