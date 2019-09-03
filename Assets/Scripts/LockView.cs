using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockView : MonoBehaviour
{
	[SerializeField] private Image itemPreview = null;
	[SerializeField] private Behaviour[] toDisable = null;
	[SerializeField] private Transform character = null;
	[SerializeField] private float maxTargetDistance = 5f;
	[SerializeField] private float lookUpCorrection = 0.3f;
	[SerializeField] private float damping = 1f;
	[SerializeField] private LayerMask mask = 0;
	[SerializeField] private PlayerItem itemInHand = PlayerItem.None;

	public bool locked = false;
	public RandomWords randomWord = null;

    private GameObject TargetsTextGraphic;
    //private GameObject TargetHit;

    private RaycastHit hit;
    private bool NPC;

    public bool LockedWithVanessa = false;
    public bool LockedWithGroundskeeper = false;
    public bool LockedWithBathroomAttendant = false;
    public bool LockedWithForint = false;
    public bool LockedWithBathroomDoor = false;
    public bool LockedWithTurul = false;

    public GameObject BathroomDoor;
    public bool bathroomCutSceneCameraPan = false;

    public GameObject GroundskeeperTextGraphic;

    public FMOD.Studio.EventInstance UhhhhMaybeYouShouldWait;
    public bool UhhhMaybeYouShouldWaitPlayed = false;

    public GameObject ForintTextGraphic;

    void Start()
    {
        NPC = false;
        UhhhhMaybeYouShouldWait = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Player/UhhhhMaybeYouShouldWait");
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
            if (randomWord )
            {
                if (!LockedWithForint)
                {
                    Vector3 targetPos = randomWord.gameObject.transform.position + randomWord.gameObject.transform.up * lookUpCorrection;
                    LockOnToTargetObject(targetPos);
                }
                if (LockedWithForint)
                {
                    ForintTextGraphic.SetActive(true);

                }



                //Quaternion rotationT = Quaternion.LookRotation(targetPos - transform.position);
                //Quaternion rotationC = Quaternion.LookRotation(targetPos - character.position);
                //rotationT = Quaternion.Slerp(transform.rotation, rotationT, Time.deltaTime * damping);
                //rotationC = Quaternion.Slerp(character.rotation, rotationC, Time.deltaTime * damping);

                //transform.localRotation = Quaternion.Euler(rotationT.eulerAngles.x, 0, 0);
                //character.rotation = Quaternion.Euler(0, rotationC.eulerAngles.y, 0);
            }
            if (bathroomCutSceneCameraPan)
            {
                Vector3 targetPos = BathroomDoor.transform.position;
                LockOnToTargetObject(targetPos);
                //GroundskeeperTextGraphic.SetActive(false);
                //StartCoroutine(DelayUhhhhDialogue());
                if (!UhhhMaybeYouShouldWaitPlayed)
                {
                    StartCoroutine(DelayUhhhhDialogue());
                }
                //UhhhhMaybeYouShouldWait.start();
            }
		}
    }

	public void HoldItem( PlayerItem item, Sprite image)
	{
		itemInHand = item;
		itemPreview.sprite = image;

		if ( image )
			itemPreview.gameObject.SetActive( true );
		else
			itemPreview.gameObject.SetActive( false );
	}

    public void LockOnToTargetObject(Vector3 targetPos)
    {
        Quaternion rotationT = Quaternion.LookRotation(targetPos - transform.position);
        Quaternion rotationC = Quaternion.LookRotation(targetPos - character.position);
        rotationT = Quaternion.Slerp(transform.rotation, rotationT, Time.deltaTime * damping);
        rotationC = Quaternion.Slerp(character.rotation, rotationC, Time.deltaTime * damping);

        transform.localRotation = Quaternion.Euler(rotationT.eulerAngles.x, 0, 0);
        character.rotation = Quaternion.Euler(0, rotationC.eulerAngles.y, 0);
    }

	private void TryToLockView( )
	{
		// Did we hit something?
		if ( !Physics.Raycast( transform.position, transform.TransformDirection( Vector3.forward ), out RaycastHit hit, Mathf.Infinity, mask ) )
			return;

		// Is it within out max distance?
		if ( Vector3.Distance( character.transform.position, hit.collider.gameObject.transform.position ) > maxTargetDistance )
			return;

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
        if (hit.transform.tag == "NPC")
        {
            NPC = true;
        }

        if (hit.transform.name == "Vanessa")
        {
            LockedWithVanessa = true;
        }
        if (hit.transform.name == "Groundskeeper")
        {
            LockedWithGroundskeeper = true;
        }
        if (hit.transform.name == "Bathroom Attendant")
        {
            LockedWithBathroomAttendant = true;
        }
        if (hit.transform.name == "forint")
        {
            LockedWithForint = true;
        }
        if (hit.transform.name == "BathroomDoor")
        {
            LockedWithBathroomDoor = true;
        }
        if (hit.transform.name == "Turul")
        {
            LockedWithTurul = true;
        }




        // Does it have a RandomWords on it?
        randomWord = hit.collider.gameObject.GetComponent<RandomWords>( );
		if ( !randomWord && !NPC)
			return;



        // We got a winner!
        //Debug.Log( "Focusing on: " + randomWord.gameObject.name );

        //TargetHit = randomWord.gameObject;
        //Debug.Log(TargetHit);
        //TargetsTextGraphic = randomWord.gameObject.transform.Find("TextGraphic").gameObject;
        //TargetsTextGraphic.SetActive(true);

        foreach ( var item in toDisable )
			item.enabled = false;

		locked = true;
	}

	private void UnLockView( )
	{
		foreach ( var item in toDisable )
			item.enabled = true;

		locked = false;

        LockedWithVanessa = false;
        LockedWithGroundskeeper = false;
        LockedWithBathroomAttendant = false;
        LockedWithBathroomDoor = false;
        LockedWithForint = false;
        LockedWithTurul = false;
	}

    public IEnumerator DelayUhhhhDialogue()
    {
        UhhhMaybeYouShouldWaitPlayed = true;
        yield return new WaitForSeconds(3.0f);
        UhhhhMaybeYouShouldWait.start();
        bathroomCutSceneCameraPan = false;
    }


}
