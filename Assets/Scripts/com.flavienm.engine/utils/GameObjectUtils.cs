using UnityEngine;

namespace com.flavienm.engine.utils
{
    public class GameObjectUtils
    {
        public static T CreateGameObjectWithScript<T>(string name) where T : Component
        {
            GameObject inputObject = new GameObject { name = name };
            inputObject.AddComponent<T>();
            return inputObject.GetComponent<T>();
        }
    }
}