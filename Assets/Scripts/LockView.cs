using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Events;

public class LockView : MonoBehaviour
{
	[SerializeField] private InventoryItemManager inventory = null;
	[SerializeField] private Image itemPreview = null;
	[SerializeField] private Behaviour[] toDisable = null;
	[SerializeField] private Transform character = null;
	[SerializeField] private float maxTargetDistance = 7f;
	[SerializeField] private float lookUpCorrection = 0.3f;
	[SerializeField] private float damping = 1f;
	[SerializeField] private LayerMask mask = 0;
	[SerializeField] private PlayerItem itemInHand = PlayerItem.None;
	[SerializeField] private UnityEvent usePhone = null;

	public bool locked = false;
	public RandomWords randomWord = null;
    public bool randomWordBool = false;

    private GameObject TargetsTextGraphic;
    //private GameObject TargetHit;

    private RaycastHit hit;
    private bool lockedWithNPC = false;
    private bool lockedWithGround = false;
    private bool ambientInteractable = false;

    public bool LockedWithVanessa = false;
    public bool LockedWithCharlie = false;
    public bool LockedWithGroundskeeper = false;
    public bool LockedWithBathroomAttendant = false;
    public bool LockedWithForint = false;
    public bool LockedWithBathroomDoor = false;
    public bool LockedWithTurul = false;
    public static bool LockedWithMilk = false;
    public bool LockedWithCatPuzzle = false;
    public bool LockedWithCandyBowl = false;
    public bool LockedWithCandyPuzzle = false;
    public bool LockedWithSink = false;
    public bool LockedWithBlankGrave = false;

    public GameObject BathroomDoor;
    public bool bathroomCutSceneCameraPan = false;

    public bool sicknessPuzzleCutsceneWithFene = false;

    public GameObject fene;
    public GameObject GroundskeeperTextGraphic;

    public FMOD.Studio.EventInstance UhhhhMaybeYouShouldWait;
    public bool UhhhMaybeYouShouldWaitPlayed = false;

    public GameObject charliesTextGraphic;

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

    public GameObject candyPuzzle;
    public bool candyPuzzleLightningCutscene = false;
    public GameObject elementsPuzzle;
    public bool elementsPuzzleLightningCutscene = false;

    public GameObject candyBowlTextGraphic;

    public GameObject shovel;

    public GameObject sink;

    public GameObject waterBottleSlot;
    private SelectItem waterBottleSlotSelectItemScript;
    private Inventory inventoryScript;
    public Sprite fullWaterBottleSprite;

    public static bool AnItemIsBeingHeld = false;

    public GameObject InventoryCanvas;
    private InventoryItemManager inventoryItemManagerScript;

    public FMOD.Studio.EventInstance IHaveAShovel;

    public GameObject bathroomAttendant;

    void Start()
    {
        lockedWithNPC = false;
        UhhhhMaybeYouShouldWait = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Player/UhhhhMaybeYouShouldWait");
        turulSFXScript = turul.GetComponent<PlayTurulSFX>();

        IHaveAShovel = FMODUnity.RuntimeManager.CreateInstance("event:/ItemInteractions/IHaveAShovel");

        lightScript = lights.GetComponent<ProgressiveLights>();
        gateCloseScript = bathroomCutsceneHolder.GetComponent<TriggerGateClose>();

        waterBottleSlotSelectItemScript = waterBottleSlot.GetComponent<SelectItem>();
        inventoryScript = GetComponent<Inventory>();

        inventoryItemManagerScript = InventoryCanvas.GetComponent<InventoryItemManager>();
    }

    void Update()
    {
        PlayLoopingSquawk.TurulLoopsSquawk.getPlaybackState(out TurulSquawkingPlaybackState);
        //Debug.Log("locked " + locked);
       // Debug.Log("random word " + randomWord);

        if ( Input.GetKeyDown( KeyCode.Space ) )
		{
            Debug.Log("locked check on space key down " + locked);
			if ( locked )
				UnLockView( );
			else
				TryToLockView( );
		}

		if ( locked )
		{
            Debug.Log("random word " + randomWord);
            Debug.Log("randomWordBool" + randomWordBool);
            if (randomWordBool )
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
                    //Vector3 targetPos = 
                    //ForintTextGraphic.SetActive(true);

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

                //candy/cukorkat
                if (LockedWithCandyBowl && !InventoryItemManager.playerHasCandy)
                {
                    candyBowlTextGraphic.SetActive(true);
                }
                if (!LockedWithCandyBowl)
                {
                    candyBowlTextGraphic.SetActive(false);
                }

                //Quaternion rotationT = Quaternion.LookRotation(targetPos - transform.position);
                //Quaternion rotationC = Quaternion.LookRotation(targetPos - character.position);
                //rotationT = Quaternion.Slerp(transform.rotation, rotationT, Time.deltaTime * damping);
                //rotationC = Quaternion.Slerp(character.rotation, rotationC, Time.deltaTime * damping);

                //transform.localRotation = Quaternion.Euler(rotationT.eulerAngles.x, 0, 0);
                //character.rotation = Quaternion.Euler(0, rotationC.eulerAngles.y, 0);

            }//end of random word condition
            else 
            {
                Debug.Log("else condition of lockView update");
                Vector3 targetPos = hit.transform.position;
                LockOnToTargetObject(targetPos);
                Debug.Log("inside non random word lock");
            }

            

            /*if (LockedWithTurul && !candyPuzzleLightningCutscene && !sicknessPuzzleCutsceneWithFene && !elementsPuzzleLightningCutscene)
            {
                Vector3 targetPos = turul.transform.position;
                LockOnToTargetObject(targetPos);
            }

            if (sicknessPuzzleCutsceneWithFene)
            {
                Vector3 targetPos = fene.transform.position;
                LockOnToTargetObject(targetPos);
            }

            if (candyPuzzleLightningCutscene)
            {
                Vector3 targetPos = candyPuzzle.transform.position;
                LockOnToTargetObject(targetPos);
            }
            
            if (elementsPuzzleLightningCutscene)
            {
                LockedWithTurul = false;
                Vector3 targetPos = elementsPuzzle.transform.position;
                LockOnToTargetObject(targetPos);
            }
            if (LockedWithBathroomAttendant)
            {
                Vector3 targetPos = bathroomAttendant.transform.position;
                LockOnToTargetObject(targetPos);
            }*/

            if (bathroomCutSceneCameraPan)
            {
                LockedWithGroundskeeper = false;
                Vector3 targetPos = BathroomDoor.transform.position;
                LockOnToTargetObject(targetPos);

                if (!UhhhMaybeYouShouldWaitPlayed)
                {
                    StartCoroutine(DelayUhhhhDialogue());
                }

            }//end of bathroomCutScene

        }//end of locked
    }

	public void HoldItem( PlayerItem item, Sprite image)
	{
		itemInHand = item;
		itemPreview.sprite = image;
        

		if ( image)
        {
            itemPreview.gameObject.SetActive(true);
            AnItemIsBeingHeld = true;
        }

        else
        {
            itemPreview.gameObject.SetActive(false);
            AnItemIsBeingHeld = false;
        }
			
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
        Debug.Log("reached TryToLockView");
        
		// Did we hit something?
		if ( !Physics.Raycast( transform.position, transform.TransformDirection( Vector3.forward ), out RaycastHit hit, Mathf.Infinity, mask ))
        {
            Debug.Log("Lock View Hit Nothing/Went to infinity");
            return;
        }
			
        
        //Debug.Log(character.transform.position);
        //Debug.Log(hit.collider.gameObject.transform.position);

        // Is it within out max distance?
        if (Vector3.Distance(character.transform.position, hit.collider.gameObject.transform.position) > 10.0f)
        {
            Debug.Log("Lock View Hit " + hit.transform.name + ", but it is beyond the max distance");
            return;
        }
			
        
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
        Debug.Log("Lock View raycast hit " + hit.transform.name);
        if (hit.transform.tag == "NPC")
        {
            lockedWithNPC = true;
        }
        if (hit.transform.tag == "Ground")
        {
            lockedWithGround = true;
        }
        

        if (hit.transform.name == "Vanessa")
        {
            LockedWithVanessa = true;
            if (PuzzleManagement.PlayerIsDoingElementsPuzzle && !InventoryItemManager.playerHasFan)
            {
                InventoryItemManager.playerHasFan = true;
                inventoryItemManagerScript.SetItem(PlayerItem.Fan, true);
                Debug.Log("You should have the fan in your inventory");
            }
        }

        if (hit.transform.name == "Charlie")
        {
            LockedWithCharlie = true;
            if (PuzzleManagement.PlayerIsDoingSicknessPuzzle)
            {

                charliesTextGraphic.SetActive(true);
                 if (!DialogManager.PlayerHasAskedForMedicine)
                {
                    DialogueWithCharlie.AskingForMedicine.start();
                    DialogManager.PlayerHasAskedForMedicine = true;
                } 
                 else
                {
                    DialogueWithCharlie.StillDontKnowWordForMedicine.start();
                }
            } else if (PuzzleManagement.PlayerIsDoingElementsPuzzle && !InventoryItemManager.playerHasLighter)
                {
                InventoryItemManager.playerHasLighter = true;
                inventoryItemManagerScript.SetItem(PlayerItem.Lighter, true);
                Debug.Log("You should have the lighter in your inventory");
            }
            else
            {
                DialogueWithCharlie.IDontFeelWell.start();
            }
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
            //Debug.Log("Locked with Turul");
            LockedWithTurul = true;
            if (PuzzleManagement.PlayerIsDoingCatPuzzle)
            {
                turulSFXScript.TurulSaysMilkTejSound.start();
            }
            if (PuzzleManagement.PlayerIsDoingSicknessPuzzle)
            {
                turulSFXScript.TurulSaysMedicineSound.start();
            }
            if (PuzzleManagement.PlayerIsDoingCandyPuzzle)
            {
                turulSFXScript.TurulSaysCandySound.start();
            }
            if (PuzzleManagement.PlayerIsDoingElementsPuzzle)
            {
                turulSFXScript.TurulSaysFireWaterEarthWindSound.start();
            }
                
        }
        if (hit.transform.name == "Milk")
        {
            //Debug.Log("Locked With Milk");
            LockedWithMilk = true;
        }
        if (hit.transform.name == "CatPuzzle")
        {

            LockedWithCatPuzzle = true;
        }
        if (hit.transform.name == "Candy Bowl")
        {
            LockedWithCandyBowl = true;

        }
        if (hit.transform.name == "CandyPuzzle")
        {
            
            LockedWithCandyPuzzle = true;
            
        }
        if (hit.transform.name == "Shovel")
        {
            InventoryItemManager.playerHasShovel = true;
            shovel.SetActive(false);
            IHaveAShovel.start();
        }
        if (hit.transform.name == "VanessasBlankGrave")
        {
            LockedWithBlankGrave = true;
            ambientInteractable = true;
        }
        /*if (hit.transform.name == "Sink" && itemInHand == PlayerItem.WaterBottleEmpty)
        {
            InventoryItemManager.playerHasFullWaterBottle = true;
            HoldItem(PlayerItem.WaterBottleEmpty, fullWaterBottleSprite);
        }*/

		// Did we hit something we can interact with?
		ItemInteraction interactionScript = hit.collider.gameObject.GetComponent<ItemInteraction>( );
		if ( interactionScript )
		{
			PlayerItem returnedItem = interactionScript.TryInteracting( itemInHand );

			if ( returnedItem != itemInHand )
			{
				inventory.RemoveItem( itemInHand );
				HoldItem( PlayerItem.None, null );
				inventory.GiveItem( returnedItem );
			}
		}
		else if (itemInHand == PlayerItem.Phone)
		{
			usePhone.Invoke( );
		}


        // Does it have a RandomWords on it?
        if (hit.collider.gameObject.GetComponent<RandomWords>() != null)
        {
            randomWord = hit.collider.gameObject.GetComponent<RandomWords>();
            randomWordBool = true;
        } else
        {
            randomWord = null;
            randomWordBool = false;
        }
        

        //return if we hit something not interactable
		if ( !randomWordBool && !lockedWithNPC && !lockedWithGround && !ambientInteractable)
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
        if (locked)
        {
            Debug.Log("successful lock");
            Debug.Log("object name: " + hit.transform.name);
        }
        else
        {
            Debug.Log("unsuccessful lock");
        }
        
        
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
        LockedWithCandyBowl = false;
        LockedWithCandyPuzzle = false;
        LockedWithCharlie = false;
        LockedWithSink = false;
        LockedWithBlankGrave = false;

        lockedWithNPC = false;
        randomWord = null;
        lockedWithGround = false;
        randomWordBool = false;
        ambientInteractable = false;
        //Debug.Log("View Unlocked");
	}

    public IEnumerator DelayUhhhhDialogue()
    {
        UhhhMaybeYouShouldWaitPlayed = true;
        yield return new WaitForSeconds(3.0f);
        UhhhhMaybeYouShouldWait.start();
        bathroomCutSceneCameraPan = false;
    }

    public void makeGraphicsGrainier()
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
