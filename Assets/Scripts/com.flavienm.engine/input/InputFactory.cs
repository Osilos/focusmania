using UnityEngine;
using System;
using com.flavienm.engine.utils;
using Tobii.EyeTracking;

namespace com.flavienm.engine.input
{
    public class InputFactory
    {
        private static DeviceType deviceType;

        public static void Create()
        {
            deviceType = SystemInfo.deviceType;
            CreateInputObject(hasEyeTracking());
        }

        private static void CreateInputObject(bool tobii)
        {
            Debug.Log(EyeTrackingHost.GetInstance().EyeTrackingDeviceStatus);
            if (tobii)
                GameObjectUtils.CreateGameObjectWithScript<InputTobii> ("InputTobii");
            else
                GameObjectUtils.CreateGameObjectWithScript<InputDesktop>("InputDesktop");
        }

        private static bool hasEyeTracking ()
        {
            
            return
                EyeTrackingHost.TobiiEngineAvailability.Equals(EngineAvailability.Running);
        }

        private static bool applicationIsMobile ()
        {
            return Application.isMobilePlatform;
        }

        private static bool applicationIsDeskop()
        {
            return deviceType.Equals(DeviceType.Desktop);
        }

        private static bool applicationIsEditor ()
        {
            return Application.isEditor;
        }
    }
}