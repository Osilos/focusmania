using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tobii.EyeTracking;

namespace com.flavienm.engine.input
{
    public class InputTobii : Input
    {
        private void Update()
        {
            DispatchPositionEvent(positionInput, EyeTracking.GetGazePoint().Screen);
            SpaceInput();
        }
    }
}