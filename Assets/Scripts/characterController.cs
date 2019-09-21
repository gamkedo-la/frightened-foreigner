using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{

    public float speed = 10.0f;
   public static bool PlayerIsWalking = false;
    public static FMOD.Studio.EventInstance Footsteps;
    private FMOD.Studio.PLAYBACK_STATE FootstepsPlaybackState;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//keeps mouse locked into the game space
        Footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Footsteps");
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxisRaw("Vertical") * speed;
        float strafe = Input.GetAxisRaw("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown("escape"))//allows player to navigate out of the game space
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown("r"))//repeats target audio
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Words/Bathroom_Furdoszoba");
        }

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
