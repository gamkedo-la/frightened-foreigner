using UnityEngine;

public class LockView : MonoBehaviour
{
	[SerializeField] private Behaviour[] toDisable = null;
	[SerializeField] private Transform character = null;
	[SerializeField] private float maxTargetDistance = 5f;
	[SerializeField]private float lookUpCorrection = 0.3f;
	[SerializeField] private float damping = 1f;
	[SerializeField] private LayerMask mask = 0;

	private bool locked = false;
	private RandomWords randomWord = null;

    private GameObject TargetsTextGraphic;
    //private GameObject TargetHit;

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
			Vector3 targetPos = randomWord.gameObject.transform.position + randomWord.gameObject.transform.up * lookUpCorrection;

			Quaternion rotationT = Quaternion.LookRotation( targetPos - transform.position );
			Quaternion rotationC = Quaternion.LookRotation( targetPos - character.position );
			rotationT = Quaternion.Slerp( transform.rotation, rotationT, Time.deltaTime * damping );
			rotationC = Quaternion.Slerp( character.rotation, rotationC, Time.deltaTime * damping );

			transform.localRotation = Quaternion.Euler( rotationT.eulerAngles.x, 0, 0 );
			character.rotation = Quaternion.Euler( 0, rotationC.eulerAngles.y, 0 );
		}
    }

	private void TryToLockView( )
	{
		// Did we hit something?
		if ( !Physics.Raycast( transform.position, transform.TransformDirection( Vector3.forward ), out RaycastHit hit, Mathf.Infinity, mask ) )
			return;

		// Is it within out max distance?
		if ( Vector3.Distance( character.transform.position, hit.collider.gameObject.transform.position ) > maxTargetDistance )
			return;

		// Does it have a RandomWords on it?
		randomWord = hit.collider.gameObject.GetComponent<RandomWords>( );
		if ( !randomWord )
			return;

		// We got a winner!
		Debug.Log( "Focusing on: " + randomWord.gameObject.name );

        //TargetHit = randomWord.gameObject;
        //Debug.Log(TargetHit);
        TargetsTextGraphic = randomWord.gameObject.transform.Find("TextGraphic").gameObject;
        TargetsTextGraphic.SetActive(true);

        foreach ( var item in toDisable )
			item.enabled = false;

		locked = true;
	}

	private void UnLockView( )
	{
		foreach ( var item in toDisable )
			item.enabled = true;

		locked = false;
	}
}
