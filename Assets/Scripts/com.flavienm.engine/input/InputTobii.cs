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
            if (EyeTrackingHost.TobiiEngineAvailability.Equals(EngineAvailability.Running))
            {
                Debug.Log("ENGINE IS HERE");
            }
            else
            {
                Debug.Log("ENGINE NOT WORKING");
            }

            Debug.Log(EyeTracking.GetGazePoint().Screen);
            
            

            EyeTrackingHost.GetInstance().EyeTrackingDeviceStatus.Equals(DeviceStatus.Tracking);

            if (EyeTracking.GetUserPresence().IsUserPresent)
            {
                Debug.Log("IS USER PRESENT");
            }
            else
            {
                Debug.Log("ENGINE NOT WORKING");
            }
        }
    }
}