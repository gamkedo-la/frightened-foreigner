using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TriggerGateClose : MonoBehaviour
{

    public GameObject door;
    public GameObject activeStatusAudioManager;
    private DoorScript doorScript;

    private SceneManagement sceneManagementScript;
    private GameObject levelChanger;

    private GameObject Lights;
    private ProgressiveLights LightsScript;

    public GameObject PostProccessingValue;
    private PostProcessVolume PPVScript;
    private Grain GrainLayer;
    private Vignette VignetteLayer;
    private float PPVMultiplier = 1.5f;
    private float maxGrainIntensity = 0.5f;
    private float maxVignetteIntensity = 0.45f;

    public FMOD.Studio.EventInstance vanessaSaysOMGAfterGateCloses;
    public FMOD.Studio.EventInstance shakeGateSound;

    public GameObject ghost;
    public GameObject stillClock;
    public GameObject clock;
    public GameObject BlankGraves;
    public GameObject EngravedGraves;


    public GameObject turul;
    private PlayTurulSFX turulSFXScript;
    public static FMOD.Studio.EventInstance loopingTurulSquawkSound;
    FMOD.Studio.PLAYBACK_STATE loopingTurulPlaybackState;


    public GameObject footstepsHolderObject;
    private BathroomCutsceneFootstepsControl bathroomCutceneFootstepsScript;

    public GameObject stormSystem;
    private Animator stormSystemAnimator;

    public GameObject stormSystemSoundHolder;
    private StormSoundControls stormSoundControlsScript;

    public GameObject playerCamera;
    private LockView lockViewScript;

    public static FMOD.Studio.EventInstance gateCreakAndLockSound;

    public GameObject generalRainSoundHolder;
    private GeneralRainSoundsScript generalRainSoundScript;

    public GameObject mausoleumRainHolder;
    public GameObject BushRainSoundHolder;

    public Material testSkyBox;

    private void Awake()
    {
        stormSystemAnimator = stormSystem.GetComponent<Animator>();
        stormSoundControlsScript = stormSystemSoundHolder.GetComponent<StormSoundControls>();
        lockViewScript = playerCamera.GetComponent<LockView>();
        gateCreakAndLockSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/GateCreakAndLock");
        generalRainSoundScript = generalRainSoundHolder.GetComponent<GeneralRainSoundsScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        doorScript = door.GetComponent<DoorScript>();
        levelChanger = GameObject.Find("LevelChanger");
		if (!levelChanger)
		{
			Instantiate( activeStatusAudioManager );
			levelChanger = GameObject.Find("LevelChanger");
		}

        sceneManagementScript = levelChanger.GetComponent<SceneManagement>();

        Lights = GameObject.Find("Lights");
        LightsScript = Lights.GetComponent<ProgressiveLights>();

        vanessaSaysOMGAfterGateCloses = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Vanessa/OMGTheGateClosed");
        shakeGateSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/ShakingGate");

        turulSFXScript = turul.GetComponent<PlayTurulSFX>();
        

        bathroomCutceneFootstepsScript = footstepsHolderObject.GetComponent<BathroomCutsceneFootstepsControl>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void triggerBathroomCutsceneBool()
    {
        DoorScript.BathroomCutsceneHasPlayed = true;
        ProgressiveLights.fogShouldBeGettingFoggier = true;
        gateCreakAndLockSound.start();
        generalRainSoundScript.generalRainSounds.setParameterValue("rainTypes", 2);
        mausoleumRainHolder.SetActive(true);
        BushRainSoundHolder.SetActive(true);
        stormSoundControlsScript.increaseThunderAndRainIntensityAfterBathroomCutscene();
        sceneManagementScript.ShouldFadeInPostBathroomMusic = true;
        ProgressiveLights.lightsShouldBeDimming = true;
        LightsScript.rotateSunBathroomCutscene = true;
        //RenderSettings.skybox.SetColor("_Tint", new Color(0.0f, 51.0f/255.0f, 106.0f/255.0f, 1.0f));
        //testSkyBox.SetColor("_Tint", new Color(0.0f, 51.0f / 255.0f, 106.0f / 255.0f, 1.0f));
        LightsScript.lowerExposureBathroomCutscene = true;
        PuzzleManagement.shouldIncreaseGrainSizeBathroomCutscene = true;
        PuzzleManagement.shouldIncreaseVignetteIntensity = true;
        LightsScript.targetLightIntensity -= LightsScript.targetDimAmount;

        LightsScript.MakeAmbientCreepier();//make the game slightly darker to help add progressive creepy ambience
        stormSystemAnimator.Play("Storm Convergence 2");
        PPVScript = PostProccessingValue.GetComponent<PostProcessVolume>();
        PPVScript.profile.TryGetSettings<Grain>(out GrainLayer);
        PPVScript.profile.TryGetSettings<Vignette>(out VignetteLayer);
        
       /* GrainLayer.intensity.Override(GrainLayer.intensity * PPVMultiplier);
        VignetteLayer.intensity.Override(VignetteLayer.intensity * PPVMultiplier);
        if (GrainLayer.intensity > maxGrainIntensity)
        {
            GrainLayer.intensity.Override(maxGrainIntensity);
        }
        if (VignetteLayer.intensity > maxVignetteIntensity)
        {
            VignetteLayer.intensity.Override(maxVignetteIntensity);
        }*/
    }

    public void VanessaSaysOMGAfterTheGateCloses()
    {
        vanessaSaysOMGAfterGateCloses.start();
    }

    public void PlayShakeGateSound()
    {
        shakeGateSound.start();
    }

    public void PlayLoopingTurulSquawk()
    {
        PlayLoopingSquawk.TurulLoopsSquawk = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/TurulSquawkingLoop");
        var turuls3DPosition = FMODUnity.RuntimeUtils.To3DAttributes(turul.transform.position);
        PlayLoopingSquawk.TurulLoopsSquawk.set3DAttributes(turuls3DPosition);
        PlayLoopingSquawk.TurulLoopsSquawk.start();
        //PlayLoopingSquawk.TurulLoopsSquawk.start();
        
    }

    public void turnOnCatPuzzle()
    {
        PuzzleManagement.PlayerIsDoingBathroomPuzzle = false;
        lockViewScript.UnLockView();
        lockViewScript.checkHit = true;
        
    }

    public void particleGhostAppears()
    {
        ghost.SetActive(true);
    }

    public void switchStillClockToMovingClock()
    {
        stillClock.SetActive(false);
        clock.SetActive(true);
    }

    public void switchBlankGravesToEngravedGraves()
    {
        BlankGraves.SetActive(false);
        EngravedGraves.SetActive(true);
    }

    public void bathroomFloorFootStepsOn()
    {
        bathroomCutceneFootstepsScript.turnOnBathroomFootsteps();
    }

    public void bathroomFloorFootStepsOff()
    {
        bathroomCutceneFootstepsScript.turnOffBathroomFootsteps();
    }

    public void groundFootStepsOn()
    {
        bathroomCutceneFootstepsScript.turnOnGroundFootsteps();
    }

    public void groundFootStepsOff()
    {
        bathroomCutceneFootstepsScript.turnOffGroundFootsteps();
    }

    public void playOneShotGroundFootstep()
    {

        FMODUnity.RuntimeManager.PlayOneShot("event:/Bathroom Cutscene/OneShotGroundStep");
    }
}
