using UnityEngine;
using System.Collections;

namespace com.flavienm.engine.ui
{
    public class SoundButton : MonoBehaviour
    {
        public void EnableSound ()
        {
            GameManager.isSoundEnabel = true;            
        }

        public void DisableSound()
        {
            GameManager.isSoundEnabel = false;
        }
    }
}