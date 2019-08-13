using UnityEngine;

public class Door : MonoBehaviour
{
	private void OnTriggerExit( Collider other )
	{
		if ( !other.CompareTag( "Player" ) )
			return;

		GetComponent<Animator>( ).enabled = true;
		GetComponent<Collider>( ).enabled = false;
	}
}
