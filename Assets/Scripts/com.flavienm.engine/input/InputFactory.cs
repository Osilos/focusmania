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

            CreateInputObject(
                applicationIsMobile() || applicationIsEditor(), 
                applicationIsDeskop() || applicationIsEditor(),
                "Engine"
            );
        }

        private static void CreateInputObject(bool touch, bool desktop, string inputName = "")
        {
            if (touch)
                GameObjectUtils.CreateGameObjectWithScript<InputTouch> ("InputTouch" + inputName);
            if (desktop)
                GameObjectUtils.CreateGameObjectWithScript<InputDesktop>("InputDesktop" + inputName);

            if (!touch && !desktop)
                throw new Exception("Unknow device type");
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