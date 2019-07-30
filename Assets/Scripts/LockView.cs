using UnityEngine;

public class LockView : MonoBehaviour
{
	[SerializeField] private Behaviour[] toDisable = null;
	[SerializeField] private Transform character = null;
	[SerializeField] private float damping = 1f;

	private bool locked = false;
	private RandomWords rw = null;

	void Start()
    {

    }

    void Update()
    {
		if ( Input.GetKeyDown( KeyCode.Space ) )
		{
			if ( locked )
				UnLockView( );
			else
				TryToLockView( );
		}

		if ( locked )
		{
			Quaternion rotationT = Quaternion.LookRotation( rw.gameObject.transform.position - transform.position );
			Quaternion rotationC = Quaternion.LookRotation( rw.gameObject.transform.position - character.position );
			rotationT = Quaternion.Slerp( transform.rotation, rotationT, Time.deltaTime * damping );
			rotationC = Quaternion.Slerp( character.rotation, rotationC, Time.deltaTime * damping );

			transform.localRotation = Quaternion.Euler( rotationT.eulerAngles.x, 0, 0 );
			character.rotation = Quaternion.Euler( 0, rotationC.eulerAngles.y, 0 );
		}
    }

	private void TryToLockView( )
	{
		if ( !Physics.Raycast( transform.position, transform.TransformDirection( Vector3.forward ), out RaycastHit hit, Mathf.Infinity ) )
			return;

		rw = hit.collider.gameObject.GetComponent<RandomWords>( );
		if ( !rw )
			return;

		Debug.Log( "Focusing on: " + rw.gameObject.name );

		foreach ( var item in toDisable )
			item.enabled = false;

		locked = true;
	}

	private void UnLockView( )
	{
		Debug.Log( "Stopped focusing" );

		foreach ( var item in toDisable )
			item.enabled = true;

		locked = false;
	}
}
