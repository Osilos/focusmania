﻿using UnityEngine;
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
            Debug.Log("Creation Input Factory");
            deviceType = SystemInfo.deviceType;
            CreateInputObject(hasEyeTracking());
        }

        private static void CreateInputObject(bool tobii)
        {
            //Debug.Log(tobii);
            if(tobii)
                GameObjectUtils.CreateGameObjectWithScript<InputTobii> ("InputTobii");
           else
                GameObjectUtils.CreateGameObjectWithScript<InputDesktop>("InputDesktop");
        }

        private static bool hasEyeTracking ()
        {
            EyeTracking.Initialize();
            return
                EyeTrackingHost.TobiiEngineAvailability.Equals(EngineAvailability.Running);
                //&& EyeTrackingHost.GetInstance().EyeTrackingDeviceStatus.Equals(DeviceStatus.Tracking);
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