using UnityEngine;
using System.Collections;
using System;

namespace com.flavienm.engine
{
	public class Player : EngineObject
	{
		public delegate void PlayerEvent();
		public static PlayerEvent MarkPoint;
		public static PlayerEvent Win;
		public static PlayerEvent Lose;

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
				OnLose();
			}
			else if (colliderLayer == layerMark)
			{
				OnMark();
			}
		}

		protected virtual void OnLose ()
		{
			DispatchLoseEvent();
		}

		protected virtual void OnWin()
		{
			DispatchWinEvent();
		}


		protected virtual void OnMark ()
		{
			DispatchMarkEvent();
		}

		protected void DispatchWinEvent ()
		{
			if (Win != null)
			{
				Win();
			}
		}
		private void DispatchLoseEvent()
		{
			if(Lose != null)
			{
				Lose();
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