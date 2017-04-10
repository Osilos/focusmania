using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.flavienm.engine.input
{
    public class InputTouch : Input
    {
        private readonly Dictionary<int, Vector2> touchsBegans = new Dictionary<int, Vector2>();
        private readonly Dictionary<int, Vector2> touchsMoves = new Dictionary<int, Vector2>();

        private Touch touch;
        private float minLenghtForSwipe;

        private void Start()
        {
            minLenghtForSwipe = 50f;
        }

        protected override void OnNewGame()
        {
            touchsBegans.Clear();
            touchsMoves.Clear();
        }

        private void Update()
        {

            for (int i = 0; i < UnityEngine.Input.touchCount; i++)
            {   
                touch = UnityEngine.Input.GetTouch(i);
                StorePosition(touch);
                //DispatchSwipe(touch.fingerId);
                DispatchSide(touch);
            }
        }

        private void StorePosition(Touch touch)
        {
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (!touchsBegans.ContainsKey(touch.fingerId))
                    {
                        touchsBegans.Add(touch.fingerId, touch.position);
                    }
                    else
                    {
                        touchsBegans.Remove(touch.fingerId);
                        touchsBegans.Add(touch.fingerId, touch.position);
                    }
                    break;
                case TouchPhase.Ended:
                    if (!touchsMoves.ContainsKey(touch.fingerId))
                    {
                        touchsMoves.Add(touch.fingerId, touch.position);
                    }
                    else
                    {
                        touchsMoves.Remove(touch.fingerId);
                        touchsMoves.Add(touch.fingerId, touch.position);
                    }
                    break;
            }
        }

        private void DispatchSide(Touch touch)
        {
            if (touch.phase.Equals(TouchPhase.Ended))
            {
                if(touch.position.x > Screen.width / 2)
                {
                    DispatchRighEvent();
                }
                else
                {
                    DispatchLeftEvent();
                }
            }
        }

        private void DispatchSwipe(int fingerId)
        {
            if (!touchsBegans.ContainsKey(fingerId) || !touchsMoves.ContainsKey(fingerId))
                return;

            Vector2 swipeDirection = touchsBegans[fingerId] - touchsMoves[fingerId];

            if (minLenghtForSwipe > swipeDirection.magnitude)
                return;

            if (0 > swipeDirection.x)
            {
                DispatchRighEvent();
            }
            else
            {
                DispatchLeftEvent();
            }

            touchsBegans.Remove(fingerId);
            touchsMoves.Remove(fingerId);
        }
    }
}