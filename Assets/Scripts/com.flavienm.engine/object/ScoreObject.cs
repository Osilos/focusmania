using UnityEngine;
using System.Collections;

namespace com.flavienm.engine
{
    public class ScoreObject : EngineObject
    {
        public delegate void ScoreObjectEvent(int value);
        public static ScoreObjectEvent Score;

        protected void AddScore (int value)
        {
            if (Score != null)
                Score(value);
        }
    }   
}
