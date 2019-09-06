using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithBathroomDoor : MonoBehaviour
{
    public GameObject PlayerCamera;
    private LockView LockViewScript;

    private FMOD.Studio.EventInstance BathroomDoorMonologue;
    private FMOD.Studio.PLAYBACK_STATE BathroomDoorMonologuePlaybackState;

    private bool BathroomDoorMonologuePlayedThisInteraction = false;


    // Start is called before the first frame update
    void Start()
    {
        LockViewScript = PlayerCamera.GetComponent<LockView>();
        BathroomDoorMonologue = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/BathroomDoorHint");
    }

    // Update is called once per frame
    void Update()
    {
        BathroomDoorMonologue.getPlaybackState(out BathroomDoorMonologuePlaybackState);

        if (LockViewScript.LockedWithBathroomDoor && !BathroomDoorMonologuePlayedThisInteraction && BathroomDoorMonologuePlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            BathroomDoorMonologue.start();
            BathroomDoorMonologuePlayedThisInteraction = true;
        }
        if (!LockViewScript.LockedWithBathroomDoor)
        {
            BathroomDoorMonologuePlayedThisInteraction = false;
        }
    }
}
