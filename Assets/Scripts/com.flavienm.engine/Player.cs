using UnityEngine;
using System.Collections;
using System;

namespace com.flavienm.engine
{
    public class Player : EngineObject
    {
        public delegate void PlayerEvent();
        public static PlayerEvent MarkPoint;
        public static PlayerEvent Killed;

        [SerializeField]
        public Transform startTransform;

        private int layerKill {
            get { return LayerMask.NameToLayer("KillObject"); }
        }
        private int layerMark {
            get { return LayerMask.NameToLayer("MarkObject"); }
        }

        protected override void OnNewGame()
        {
            transform.position = startTransform.position;
            transform.rotation = startTransform.rotation;
        }

        private void OnTriggerEnter(Collider collider)
        {
            OnHitCollider(collider.gameObject.layer);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            OnHitCollider(collider.gameObject.layer);
        }

        private void OnHitCollider (int colliderLayer)
        {
            if (colliderLayer == layerKill)
            {
                OnKill();
            }
            else if (colliderLayer == layerMark)
            {
                OnMark();
            }
        }

        protected virtual void OnKill ()
        {
            DispatchKillEvent();
        }

        protected virtual void OnMark ()
        {
            DispatchMarkEvent();
        }

        private void DispatchKillEvent ()
        {
            if (Killed != null)
            {
                Killed();
            }
        }
        private void DispatchMarkEvent ()
        {
            if (MarkPoint != null)
            {
                MarkPoint();
            }
        }
    }
}