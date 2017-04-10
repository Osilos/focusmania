using UnityEngine;
using UnityEngine.UI;

namespace com.flavienm.engine.ui
{
    public class UIOnEnableFadeIn : MonoBehaviour
    {
        private void OnEnable()
        {
            foreach (Image image in GetComponentsInChildren<Image>())
            {
                image.GetComponent<CanvasRenderer>().SetAlpha(0f);
                image.CrossFadeAlpha(1f, 10f, true);
            }

            foreach (Text text in GetComponentsInChildren<Text>())
            {
                text.GetComponent<CanvasRenderer>().SetAlpha(0f);
                text.CrossFadeAlpha(1f, 1f, true);
            }

        }
    }
}