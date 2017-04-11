using UnityEngine;
using System;
using com.flavienm.engine.utils;

namespace com.flavienm.engine.input
{
    public class InputFactory
    {
        private static DeviceType deviceType;

        public static void Create()
        {
            deviceType = SystemInfo.deviceType;
            CreateInputObject();
        }

        private static void CreateInputObject()
        {
            GameObjectUtils.CreateGameObjectWithScript<InputTobii> ("InputTobii");
            GameObjectUtils.CreateGameObjectWithScript<InputDesktop>("InputDesktop");
        }

        //private static bool 

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