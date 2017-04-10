using UnityEngine;
using System.Collections;

namespace com.flavienm.engine.input
{
    public class InputDesktop : Input
    {
        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
            {
                DispatchLeftEvent();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
            {
                DispatchRighEvent();
            }
        }
    }
}