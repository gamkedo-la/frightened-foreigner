using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTurulSFX : MonoBehaviour
{

    private FMOD.Studio.EventInstance TurulSquawkSound;

    public GameObject bathroomCutsceneTimeline;
    private TriggerGateClose triggerGateCloseScript;

    public GameObject mainCamera;
    private LockView LockViewScript;

    private GameObject LevelChanger;
    private SceneManagement sceneManagementScript;

    // Start is called before the first frame update
    void Start()
    {
        TurulSquawkSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/TurulSquawk");
        triggerGateCloseScript = bathroomCutsceneTimeline.GetComponent<TriggerGateClose>();
        LockViewScript = mainCamera.GetComponent<LockView>();
        //TurulSquawkSound.start();

        LevelChanger = GameObject.Find("LevelChanger");
        sceneManagementScript = LevelChanger.GetComponent<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LockViewScript.LockedWithTurul)
        {
            Debug.Log("Turul should shut up");
            //sceneManagementScript.PostBathroomMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            triggerGateCloseScript.loopingTurulSquawkSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            triggerGateCloseScript.testSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    public void PlayTurulSquawkSound()
    {
        
        TurulSquawkSound.start();
    }
}
