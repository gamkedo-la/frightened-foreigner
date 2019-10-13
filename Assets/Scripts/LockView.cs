using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Events;
using UnityEngine.Assertions;

public class LockView : MonoBehaviour
{
	[SerializeField] private InventoryItemManager inventory = null;
	[SerializeField] private Image itemPreview = null;
	[SerializeField] private Behaviour[] toDisable = null;
	[SerializeField] private Transform character = null;
	//[SerializeField] private float maxTargetDistance = 7f;
	[SerializeField] private float lookUpCorrection = 0.3f;
	[SerializeField] private float damping = 1f;
	[SerializeField] private LayerMask mask = 0;
	[SerializeField] private PlayerItem itemInHand = PlayerItem.None;
	[SerializeField] private UnityEvent usePhone = null;

	public bool locked = false;
	public RandomWordWorkaround randomWord = null;
    public bool randomWordBool = false;

    public RaycastHit hit;
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
    public bool LockedWithLock = false;
    public bool LockedWithMausoleum = false;
    public bool LockedWithStillClock = false;
    public bool LockedWithCreepyClock = false;
    public bool LockedWithTreeOfLife = false;
    public bool LockedWithEngravedGrave = false;

    //elements puzzle
    public bool LockedWithFlowerPot = false;
    public bool LockedWithBasin = false;
    public bool LockedWithPinwheel = false;
    public bool LockedWithTorch = false;

    public static bool endingCutsceneLockWithTurul = false;
    public static bool endingCutsceneLockWithSword = false;
    public static bool endingCutsceneLockWithDragon = false;
    public static bool endingCutsceneLockWithKey = false;

    public GameObject BathroomDoor;
    public bool bathroomLightningCutSceneCameraPan = false;

    public bool sicknessPuzzleCutsceneWithFene = false;

    public GameObject fene;
    
    public FMOD.Studio.EventInstance UhhhhMaybeYouShouldWait;
    public bool UhhhMaybeYouShouldWaitPlayed = false;

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
    public FMOD.Studio.EventInstance TurulSingleSquawk;
    public FMOD.Studio.EventInstance LockComment;
    public FMOD.Studio.EventInstance StillMausoleumComment;
    public FMOD.Studio.EventInstance StillClockComment;
    public FMOD.Studio.EventInstance BlankGraveComment;
    public FMOD.Studio.EventInstance TreeOfLifeComment;
    public FMOD.Studio.EventInstance BangingMausoleumComment;
    public FMOD.Studio.EventInstance CreepyClockComment;
    public FMOD.Studio.EventInstance FilledInGravesComment;
    public FMOD.Studio.EventInstance IgnoreTheGhostComment;



    public GameObject bathroomCutsceneHolder;
    private TriggerGateClose gateCloseScript;

    public GameObject candyPuzzle;
    public bool candyPuzzleLightningCutscene = false;
    public GameObject elementsPuzzle;
    public bool elementsPuzzleLightningCutscene = false;

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

    public bool checkHit = true;

    public GameObject controlsLockedReminder;
    public GameObject elementsPuzzleLockViewTarget;

    public FMOD.Studio.EventInstance obtainLighterFromCharlieComment;
    public FMOD.Studio.EventInstance obtainFanFromVanessaComment;

    public GameObject Groundskeeper;
    private DialogueWithGroundskeeper dialogueWithGroundskeeperScript;

    public GameObject Vanessa;
    private DialogueWithVanessa dialogueWithVanessaScript;

    private FMOD.Studio.EventInstance LeaveForintOnTheGround;
    private FMOD.Studio.PLAYBACK_STATE LeaveForintOnTheGroundPlaybackState;

    public GameObject textGraphic;

    public GameObject Forint;

    public GameObject milk;
    public GameObject Charlie;
    public GameObject CandyBowl;
    public GameObject FlowerPot;
    public GameObject PinwheelLeafs;
    public GameObject Torch;
    public GameObject WaterBasin;

    public GameObject sword;
    public GameObject dragon;
    public GameObject key;

    void Start()
    {
        dialogueWithGroundskeeperScript = Groundskeeper.GetComponent<DialogueWithGroundskeeper>();
        dialogueWithVanessaScript = Vanessa.GetComponent<DialogueWithVanessa>();

        lockedWithNPC = false;
        UhhhhMaybeYouShouldWait = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Player/UhhhhMaybeYouShouldWait");
        turulSFXScript = turul.GetComponent<PlayTurulSFX>();
        TurulSingleSquawk = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/TurulSquawk");
        LockComment = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/LockComment");
        StillMausoleumComment = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/StillMausoleumComment");
        StillClockComment = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/StillClockComment");
        BlankGraveComment = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/GraveWithoutWordsComment");
        TreeOfLifeComment = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/TreeOfLife");
        BangingMausoleumComment = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/BangingMausoleumComment");
        CreepyClockComment = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/CreepyClockComment");
        FilledInGravesComment = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/FilledInGravesComment");
        IgnoreTheGhostComment = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/IgnoreTheGhostComment");

        obtainLighterFromCharlieComment = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Player/ObtainLighterComment");
            obtainFanFromVanessaComment = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Player/ObtainFanComment");


    IHaveAShovel = FMODUnity.RuntimeManager.CreateInstance("event:/ItemInteractions/IHaveAShovel");

        lightScript = lights.GetComponent<ProgressiveLights>();
        gateCloseScript = bathroomCutsceneHolder.GetComponent<TriggerGateClose>();

        waterBottleSlotSelectItemScript = waterBottleSlot.GetComponent<SelectItem>();
        inventoryScript = GetComponent<Inventory>();

        inventoryItemManagerScript = InventoryCanvas.GetComponent<InventoryItemManager>();

        LeaveForintOnTheGround = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/LeaveForintOnTheGround");
    }

    void Update()
    {
        PlayLoopingSquawk.TurulLoopsSquawk.getPlaybackState(out TurulSquawkingPlaybackState);
        


		if ( locked )
		{
            
            if (randomWordBool && !dropSwordForEndingCutscene.endingCutscene)
            {
                if (!LockedWithForint)
                {
                    Vector3 targetPos = randomWord.gameObject.transform.position + randomWord.gameObject.transform.up * lookUpCorrection;
                    LockOnToTargetObject(targetPos);
                }
                //toggle visibility of text graphics based on if player is focused on the object

            }//end of random word condition

         else if (!bathroomLightningCutSceneCameraPan && checkHit && !dropSwordForEndingCutscene.endingCutscene)//if locked with something that is not a random word puzzle
            {
                
                Vector3 targetPos = hit.transform.position;

                if (hit.transform.name == "Turul")
                {
                    //cat puzzle before lightning
                    if (PuzzleManagement.PlayerIsDoingCatPuzzle && !EmersionLightningEmmissionToggle.catLightningEmitted)
                    {
                        LockOnToTargetObject(catPuzzle.transform.position);
                    }
                    //cat puzzle after lightning
                    else if (PuzzleManagement.PlayerIsDoingCatPuzzle && EmersionLightningEmmissionToggle.catLightningEmitted)
                    {
                        LockOnToTargetObject(targetPos);
                    }

                    //sickness puzzle before lightning
                    else if(PuzzleManagement.PlayerIsDoingSicknessPuzzle && !EmersionLightningEmmissionToggle.sicknessLightningEmitted)
                    {
                        LockOnToTargetObject(fene.transform.position);
                    }
                    //sickness puzzle after lightning
                    else if(PuzzleManagement.PlayerIsDoingSicknessPuzzle && EmersionLightningEmmissionToggle.sicknessLightningEmitted)
                    {
                        LockOnToTargetObject(targetPos);
                    }

                    //candy puzzle before lightning
                    else if(PuzzleManagement.PlayerIsDoingCandyPuzzle && !EmersionLightningEmmissionToggle.candyLightningEmitted)
                    {
                        LockOnToTargetObject(candyPuzzle.transform.position);
                    }
                    //candy puzzle after lightning
                    else if(PuzzleManagement.PlayerIsDoingCandyPuzzle && EmersionLightningEmmissionToggle.candyLightningEmitted)
                    {
                        LockOnToTargetObject(targetPos);
                    }

                    //elements puzzle before lightning
                    else if(PuzzleManagement.PlayerIsDoingElementsPuzzle && !EmersionLightningEmmissionToggle.elementsLightningEmitted)
                    {
                        LockOnToTargetObject(elementsPuzzleLockViewTarget.transform.position);
                    }
                    //elements puzzle after lightning
                    else if(PuzzleManagement.PlayerIsDoingElementsPuzzle && EmersionLightningEmmissionToggle.elementsLightningEmitted)
                    {
                        LockOnToTargetObject(targetPos);
                    }
                }
                else
                {
                    LockOnToTargetObject(targetPos);
                }
                
                
         }

           

            

        else if (bathroomLightningCutSceneCameraPan)
          {
             
             LockedWithGroundskeeper = false;
             Vector3 targetPos = BathroomDoor.transform.position;
            LockOnToTargetObject(targetPos);

            if (!UhhhMaybeYouShouldWaitPlayed)
             {
                StartCoroutine(DelayUhhhhDialogue());
                    StartCoroutine(UnlockViewAfterBathroomLightningCutscene());
             }

            }//end of bathroomCutScene

            else if (dropSwordForEndingCutscene.endingCutscene)
            {
                if (endingCutsceneLockWithTurul)
                {
                    LockOnToTargetObject(turul.transform.position);
                }
                else if (endingCutsceneLockWithSword)
                {
                    LockOnToTargetObject(sword.transform.position);
                }
                else if (endingCutsceneLockWithDragon)
                {
                    LockOnToTargetObject(dragon.transform.position);
                }
                else if (endingCutsceneLockWithKey)
                {
                    LockOnToTargetObject(key.transform.position);
                }
            }

        }//end of locked

        
		if ( Input.GetKeyDown( KeyCode.Space ) )
		{

            
			if ( locked )
				UnLockView( );
			else
				TryToLockView( );
		}
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
        

		// Did we hit something?
		if ( !Physics.Raycast( transform.position, transform.TransformDirection( Vector3.forward ), out hit, Mathf.Infinity, mask ))
        {
            
            Debug.Log("Lock View Hit Nothing/Went to infinity");
            return;
        }


        

        // Is it within out max distance?
        if (Vector3.Distance(character.transform.position, hit.collider.gameObject.transform.position) > 10.0f)
        {
            Debug.Log("Lock View Hit " + hit.transform.name + ", but it is beyond the max distance");
            return;
        }


        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
        
        if (hit.transform.tag == "NPC")
        {
            lockedWithNPC = true;
        }
        else if (hit.transform.tag == "Ground")
        {
            lockedWithGround = true;
        }


        if (hit.transform.name == "Vanessa")
        {
            LockedWithVanessa = true;
            if (PuzzleManagement.PlayerIsDoingElementsPuzzle && !InventoryItemManager.playerHasFan)
            {
                InventoryItemManager.playerHasFan = true;
                obtainFanFromVanessaComment.start();
                inventoryItemManagerScript.SetItem(PlayerItem.Fan, true);
                Debug.Log("You should have the fan in your inventory");
            }
        }

        else if (hit.transform.name == "Charlie" && !AnItemIsBeingHeld)
        {
            LockedWithCharlie = true;
            if (PuzzleManagement.PlayerIsDoingSicknessPuzzle)
            {
                MouseClicks.currentCorrectAnswer = "gyógyszert";
                textGraphic.transform.position = hit.transform.position;
                Vector3 correction = new Vector3(0, 0.35f, -2.0f);
                textGraphic.transform.position += correction;
                textGraphic.SetActive(true);
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
                obtainLighterFromCharlieComment.start();
                Debug.Log("You should have the lighter in your inventory");
            }
            else
            {
                DialogueWithCharlie.IDontFeelWell.start();
            }
        }

        else if (hit.transform.name == "Groundskeeper")
        {
            LockedWithGroundskeeper = true;
            MouseClicks.currentCorrectAnswer = "fürdőszoba";
            if (!DialogManager.PlayerHasAskedWhereTheBathroomIs)
            {
                dialogueWithGroundskeeperScript.PlayerAsksWhereTheBathroomIs();
            }
            else if (dialogueWithVanessaScript.learnedFurduszoba)
            {
                dialogueWithGroundskeeperScript.PlayerAttemptsToSayBathroomToGroundskeeper();
            }
        }


        else if (hit.transform.name == "Bathroom Attendant")
        {
            LockedWithBathroomAttendant = true;
        }
        else if (hit.transform.name == "forint")
        {
            MouseClicks.currentCorrectAnswer = "forint";
            LockedWithForint = true;

            if (!DialogManager.BathroomAttendantSaidToGetForint)
            {
                LeaveForintOnTheGround.start();
                
            }
            else if (DialogManager.BathroomAttendantSaidToGetForint)
            {
                textGraphic.transform.position = Forint.transform.position;
                Vector3 correction = new Vector3(0, 0.35f, 0);
                textGraphic.transform.position += correction;
                textGraphic.SetActive(true);
            }
        }
        else if (hit.transform.name == "BathroomDoor")
        {
            LockedWithBathroomDoor = true;
        }
        else if (hit.transform.name == "Turul")
        {
            Debug.Log("Player is doing bathroom puzzle? " + PuzzleManagement.PlayerIsDoingBathroomPuzzle);
            Debug.Log("Player is doing cat puzzle? " + PuzzleManagement.PlayerIsDoingCatPuzzle);

            LockedWithTurul = true;
            if (PuzzleManagement.PlayerIsDoingBathroomPuzzle)
            {
                TurulSingleSquawk.start();
            } 
            else if (PuzzleManagement.PlayerIsDoingCatPuzzle)
            {
                turulSFXScript.TurulSaysMilkTejSound.start();
                turulSFXScript.HandleTurulSFXAndEvents();
            }
            else if (PuzzleManagement.PlayerIsDoingSicknessPuzzle)
            {
                turulSFXScript.TurulSaysMedicineSound.start();
                turulSFXScript.HandleTurulSFXAndEvents();

            }
            else if (PuzzleManagement.PlayerIsDoingCandyPuzzle)
            {
                turulSFXScript.TurulSaysCandySound.start();
                turulSFXScript.HandleTurulSFXAndEvents();

            }
            else if (PuzzleManagement.PlayerIsDoingElementsPuzzle)
            {
                turulSFXScript.TurulSaysFireWaterEarthWindSound.start();
                turulSFXScript.HandleTurulSFXAndEvents();

            }

        }
        else if (hit.transform.name == "Milk")
        {
            textGraphic.transform.position = milk.transform.position;
            Vector3 correction = new Vector3(0, 0.55f, 0);
            textGraphic.transform.position += correction;
            textGraphic.SetActive(true);
            MouseClicks.currentCorrectAnswer = "tej";
            LockedWithMilk = true;
        }
        else if (hit.transform.name == "CatPuzzle")
        {

            LockedWithCatPuzzle = true;
        }
        else if (hit.transform.name == "Candy Bowl")
        {
            MouseClicks.currentCorrectAnswer = "cukorkát";
            textGraphic.transform.position = CandyBowl.transform.position;
            Vector3 correction = new Vector3(0, 0.35f, 0);
            textGraphic.transform.position += correction;
            textGraphic.SetActive(true);

            LockedWithCandyBowl = true;

        }
        else if (hit.transform.name == "CandyPuzzle")
        {

            LockedWithCandyPuzzle = true;

        }
        else if (hit.transform.name == "Shovel")
        {
            InventoryItemManager.playerHasShovel = true;
            shovel.SetActive(false);
            IHaveAShovel.start();
        }
        else if (hit.transform.name == "VanessasBlankGrave" || hit.transform.name == "PlayersBlankGrave" || hit.transform.name == "CharliesBlankGrave")
        {
            LockedWithBlankGrave = true;
            ambientInteractable = true;
            BlankGraveComment.start();
        }

        else if (hit.transform.name == "GraveFront" || hit.transform.name == "Charlies Grave" || hit.transform.name == "Vanessas Grave")
        {
            LockedWithEngravedGrave = true;
            ambientInteractable = true;
            FilledInGravesComment.start();
        }

        else if (hit.transform.name == "CreepyClock")
        {
            CreepyClockComment.start();
        }


            if (hit.transform.name == "Lock")
        {
            LockedWithLock = true;
            ambientInteractable = true;
            LockComment.start();
        }
        else if (hit.transform.name == "Left Wall" || hit.transform.name == "Right Wall" || hit.transform.name == "Front Wall" || hit.transform.name == "Back Wall")
        {
            LockedWithMausoleum = true;
            ambientInteractable = true;
            if (PuzzleManagement.PlayerIsDoingBathroomPuzzle)
            {
                StillMausoleumComment.start();
            }
            else
            {
                BangingMausoleumComment.start();
            }

        }

        else if (hit.transform.name == "GhostParticleSystem")
        {
            IgnoreTheGhostComment.start();
        }

        else if (hit.transform.name == "stillClock")
        {
            LockedWithStillClock = true;
            ambientInteractable = true;
            StillClockComment.start();
        }
        else if (hit.transform.name == "CreepyClock")
        {
            LockedWithCreepyClock = true;
            ambientInteractable = true;
        }
        else if (hit.transform.name == "Tree of Life")
        {
            LockedWithTreeOfLife = true;
            ambientInteractable = true;
            TreeOfLifeComment.start();
        }

        else if (hit.transform.name == "FlowerPot")
        {
            MouseClicks.currentCorrectAnswer = "föld";
            textGraphic.transform.position = FlowerPot.transform.position;
            Vector3 correction = new Vector3(0, 0.65f, 0);
            textGraphic.transform.position += correction;
            textGraphic.SetActive(true);

            LockedWithFlowerPot = true;
        }
        else if (hit.transform.name == "Torch")
        {
            MouseClicks.currentCorrectAnswer = "Tűz";
            textGraphic.transform.position = Torch.transform.position;
            Vector3 correction = new Vector3(0, 0.65f, 0);
            textGraphic.transform.position += correction;
            textGraphic.SetActive(true);

            LockedWithTorch = true;
        }
        else if (hit.transform.name == "PinwheelLeafs")
        {
            MouseClicks.currentCorrectAnswer = "szél";
            textGraphic.transform.position = PinwheelLeafs.transform.position;
            Vector3 correction = new Vector3(0, 0.65f, 0);
            textGraphic.transform.position += correction;
            textGraphic.SetActive(true);

            LockedWithPinwheel = true;
        }
        else if (hit.transform.name == "Basin")
        {
            MouseClicks.currentCorrectAnswer = "víz";
            textGraphic.transform.position = WaterBasin.transform.position;
            Vector3 correction = new Vector3(0, 0.65f, 0);
            textGraphic.transform.position += correction;
            textGraphic.SetActive(true);

            LockedWithBasin = true;
        }

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
        if (hit.collider.gameObject.GetComponent<RandomWordWorkaround>() != null)
        {
            
            
            randomWord = hit.collider.gameObject.GetComponent<RandomWordWorkaround>();
            randomWordBool = true;
        } else
        {
            randomWord = null;
            randomWordBool = false;
        }


        //return if we hit something not interactable
		if ( !randomWordBool && !lockedWithNPC && !lockedWithGround && !ambientInteractable)
			return;

        

        foreach ( var item in toDisable )
			item.enabled = false;

		locked = true;
        controlsLockedReminder.SetActive(true);
        if (locked)
        {
           // Debug.Log("successful lock with: " + hit.transform.name);

        }
        else
        {
           // Debug.Log("unsuccessful lock");
        }


	}

	public void UnLockView( )
	{
		foreach ( var item in toDisable )
			item.enabled = true;

        controlsLockedReminder.SetActive(false);

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
        LockedWithLock = false;
        LockedWithMausoleum = false;
        LockedWithStillClock = false;
        LockedWithCreepyClock = false;
        LockedWithTreeOfLife = false;
        LockedWithEngravedGrave = false;
        LockedWithBasin = false;
        LockedWithTorch = false;
        LockedWithPinwheel = false;
        LockedWithFlowerPot = false;


        lockedWithNPC = false;
        randomWord = null;
        lockedWithGround = false;
        randomWordBool = false;
        ambientInteractable = false;

        textGraphic.SetActive(false);
        //charliesTextGraphic.SetActive(false);
       // ForintTextGraphic.SetActive(false);
	}

    public IEnumerator DelayUhhhhDialogue()
    {
        UhhhMaybeYouShouldWaitPlayed = true;
        yield return new WaitForSeconds(3.0f);
        UhhhhMaybeYouShouldWait.start();
        bathroomLightningCutSceneCameraPan = false;
        LockedWithGroundskeeper = false;
        
    }

    public IEnumerator UnlockViewAfterBathroomLightningCutscene()
    {
        yield return new WaitForSeconds(17.0f);
        UnLockView();
    }

    public void makeGraphicsGrainier()
    {
        PPVScript = postProcessingValue.GetComponent<PostProcessVolume>();
        PPVScript.profile.TryGetSettings<Grain>(out GrainLayer);
        PPVScript.profile.TryGetSettings<Vignette>(out VignetteLayer);
        
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
