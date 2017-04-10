using UnityEngine;

namespace com.flavienm.engine.utils
{
	public class TriggerKiller : MonoBehaviour
	{

		private void OnTriggerEnter(Collider other)
		{
			Destroy(other.gameObject);
		}
	}
}


