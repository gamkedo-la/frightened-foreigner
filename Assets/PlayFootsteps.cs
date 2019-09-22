using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFootsteps : MonoBehaviour
{

    public static bool PlayerIsWalking = false;
    public static FMOD.Studio.EventInstance Footsteps;
    private FMOD.Studio.PLAYBACK_STATE FootstepsPlaybackState;

    // Start is called before the first frame update
    void Start()
    {
        Footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Footsteps");
        var feet3DPosition = FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position);
        Footsteps.set3DAttributes(feet3DPosition);
    }

    // Update is called once per frame
    void Update()
    {
        var feet3DPosition = FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position);
        Footsteps.set3DAttributes(feet3DPosition);
        if (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d"))
        {
            Debug.Log("Player is Walking");
            PlayerIsWalking = true;
        }
        if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d"))
        {
            PlayerIsWalking = false;
        }

        Footsteps.getPlaybackState(out FootstepsPlaybackState);
        Debug.Log(FootstepsPlaybackState);
        if (PlayerIsWalking && FootstepsPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING && FootstepsPlaybackState != FMOD.Studio.PLAYBACK_STATE.STARTING)
        {
            Debug.Log("Footsteps should be audible");
            Footsteps.start();
        }
        else if (!PlayerIsWalking)
        {
            Footsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
