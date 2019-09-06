using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

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
    public static bool LockedWithMilk = false;
    public bool LockedWithCatPuzzle = false;

    public GameObject BathroomDoor;
    public bool bathroomCutSceneCameraPan = false;

    public GameObject GroundskeeperTextGraphic;

    public FMOD.Studio.EventInstance UhhhhMaybeYouShouldWait;
    public bool UhhhMaybeYouShouldWaitPlayed = false;

    public GameObject ForintTextGraphic;
    public GameObject MilkTextGraphic;

    public GameObject turul;
    private PlayTurulSFX turulSFXScript;

    public GameObject catPuzzle;

    public GameObject lights;
    private ProgressiveLights lightScript;

    public GameObject postProcessingValue;
    private PostProcessVolume PPVScript;
    private Grain GrainLayer;
    private Vignette VignetteLayer;
    private float PPVMultiplier = 1.5f;
    private float maxGrainIntensity = 0.5f;
    private float maxVignetteIntensity = 0.45f;

    private FMOD.Studio.PLAYBACK_STATE TurulSquawkingPlaybackState;

    public GameObject bathroomCutsceneHolder;
    private TriggerGateClose gateCloseScript;

    void Start()
    {
        NPC = false;
        UhhhhMaybeYouShouldWait = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Player/UhhhhMaybeYouShouldWait");
        turulSFXScript = turul.GetComponent<PlayTurulSFX>();

        lightScript = lights.GetComponent<ProgressiveLights>();
        gateCloseScript = bathroomCutsceneHolder.GetComponent<TriggerGateClose>();
    }

    void Update()
    {
        TriggerGateClose.loopingTurulSquawkSound.getPlaybackState(out TurulSquawkingPlaybackState);
        //Debug.Log(TurulSquawkingPlaybackState);

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
                //toggle visibility of text graphics based on if player is focused on the object
                //money/forint
                if (LockedWithForint)
                {
                    ForintTextGraphic.SetActive(true);

                }
                if (!LockedWithForint)
                {
                    ForintTextGraphic.SetActive(false);
                }
                //milk/tej
                if (LockedWithMilk)
                {
                    MilkTextGraphic.SetActive(true);
                }
                if (!LockedWithMilk)
                {
                    MilkTextGraphic.SetActive(false);
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
            }//end of bathroomCutScene
            
            if (LockedWithTurul)
            {
                Vector3 targetPos = turul.transform.position;
                LockOnToTargetObject(targetPos);
            }
		}//end of locked
        
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
            if (PuzzleManagement.PlayerIsDoingBathroomPuzzle)
            {
                turulSFXScript.TurulSaysMilkTejSound.start();
            }
        }
        if (hit.transform.name == "Milk")
        {
            Debug.Log("Locked With Milk");
            LockedWithMilk = true;
        }
        if (hit.transform.name == "CatPuzzle")
        {
            LockedWithCatPuzzle = true;
            if (InventoryItemManager.playerHasMilk)
            {
                catPuzzle.SetActive(false);
                lightScript.MakeAmbientCreepier();
                makeGraphicsGrainier();
                gateCloseScript.PlayLoopingTurulSquawk();
            }
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

	public void UnLockView( )
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
        LockedWithMilk = false;
        LockedWithCatPuzzle = false;
        Debug.Log("View Unlocked");
	}

    public IEnumerator DelayUhhhhDialogue()
    {
        UhhhMaybeYouShouldWaitPlayed = true;
        yield return new WaitForSeconds(3.0f);
        UhhhhMaybeYouShouldWait.start();
        bathroomCutSceneCameraPan = false;
    }

    private void makeGraphicsGrainier()
    {
        PPVScript = postProcessingValue.GetComponent<PostProcessVolume>();
        PPVScript.profile.TryGetSettings<Grain>(out GrainLayer);
        PPVScript.profile.TryGetSettings<Vignette>(out VignetteLayer);
        //Debug.Log(ambientOcclusionLayer);
        GrainLayer.intensity.Override(GrainLayer.intensity * PPVMultiplier);
        VignetteLayer.intensity.Override(VignetteLayer.intensity * PPVMultiplier);
        if (GrainLayer.intensity > maxGrainIntensity)
        {
            GrainLayer.intensity.Override(maxGrainIntensity);
        }
        if (VignetteLayer.intensity > maxVignetteIntensity)
        {
            VignetteLayer.intensity.Override(maxVignetteIntensity);
        }
    }
}
