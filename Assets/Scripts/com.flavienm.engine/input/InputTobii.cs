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

            if (EyeTrackingHost.GetInstance().EyeTrackingDeviceStatus == DeviceStatus.Tracking)
            {
                Debug.Log("IS TRACKING");
            }

            if (EyeTracking.GetUserPresence().IsUserPresent)
            {
                Debug.Log("IS USER PRESENT");
            }
        }
    }
}