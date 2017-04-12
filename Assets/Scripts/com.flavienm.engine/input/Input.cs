using UnityEngine;
using System.Collections;

namespace com.flavienm.engine.input
{
    public class Input : EngineObject
    {
        public delegate void DirectionEvent(Vector3 position, Vector3 direction);
        public delegate void PositionEvent(Vector3 position);
        public delegate void InputEvent();

        public static PositionEvent positionInput;
        public static InputEvent left;
        public static InputEvent right;
        public static InputEvent space;

        protected void DispatchPositionEvent (PositionEvent positionEvent, Vector3 position)
        {
            if (positionEvent != null)
                positionEvent(Camera.main.ScreenToWorldPoint(position));
        }

        protected void DispatchDirectionEvent(DirectionEvent directionEvent, Vector3 position, Vector3 direction)
        {
            if (directionEvent != null)
                directionEvent(position, direction);
        }

        protected void SpaceInput ()
        {
            if (UnityEngine.Input.GetKeyUp(KeyCode.Space))
            {
                DispatchSpaceEvent();
            }
        }

        protected void DispatchSpaceEvent ()
        {
            if (space != null)
                space();
        }

        protected void DispatchLeftEvent()
        {
            if (left != null)
                left();
        }
        protected void DispatchRighEvent()
        {
            if (right != null)
                right();
        }
    }
}