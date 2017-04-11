using UnityEngine;
using System.Collections;

namespace com.flavienm.engine.input
{
    public class InputDesktop : Input
    {
        private Vector3 mousePostion = Vector3.zero;

        void Update()
        {
            Debug.Log(UnityEngine.Input.mousePosition);
            if (mousePostion != UnityEngine.Input.mousePosition)
            {
                mousePostion = UnityEngine.Input.mousePosition;
                DispatchPositionEvent(positionInput, mousePostion);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                DispatchSpaceEvent();
            }
        }
    }
}