using UnityEngine;
using System.Collections;

namespace com.flavienm.engine.input
{
    public class InputDesktop : Input
    {
        void Update()
        {
            DispatchPositionEvent(positionInput, UnityEngine.Input.mousePosition);
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                DispatchSpaceEvent();
            }
        }
    }
}