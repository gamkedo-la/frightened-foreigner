using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushPushSoundTrigger : MonoBehaviour
{

    private FMOD.Studio.EventInstance BushPushSound;
    //private bool playerWasWalkingLastFrame = false;
    private bool PlayerIsWalking = false;
    private FMOD.Studio.PLAYBACK_STATE BushPushSoundPlaybackState;

    private void Awake()
    {
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        box.isTrigger = true;

        BushPushSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/BushPush");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d"))
        {
            //Debug.Log("Player is Walking");
            PlayerIsWalking = true;
        }
        if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d"))
        {
            PlayerIsWalking = false;
        }

        BushPushSound.getPlaybackState(out BushPushSoundPlaybackState);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        BushPushSound.start();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!PlayerIsWalking)
        {
            BushPushSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        else if (PlayerIsWalking && BushPushSoundPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING && BushPushSoundPlaybackState != FMOD.Studio.PLAYBACK_STATE.STARTING)
        {
            BushPushSound.start();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        BushPushSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
