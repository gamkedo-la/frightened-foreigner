using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForintInteraction : MonoBehaviour
{

    public GameObject PlayerCamera;
    private LockView LockViewScript;

    private FMOD.Studio.EventInstance LeaveForintOnTheGround;
    private FMOD.Studio.PLAYBACK_STATE LeaveForintOnTheGroundPlaybackState;

    private bool LeaveForintPlayedThisInteraction = false;
    public GameObject textGraphic;


    // Start is called before the first frame update
    void Start()
    {
        LockViewScript = PlayerCamera.GetComponent<LockView>();
        LeaveForintOnTheGround = FMODUnity.RuntimeManager.CreateInstance("event:/Monologue/LeaveForintOnTheGround");
    }

    // Update is called once per frame
    void Update()
    {
        LeaveForintOnTheGround.getPlaybackState(out LeaveForintOnTheGroundPlaybackState);
        if (LockViewScript.LockedWithForint && !DialogManager.BathroomAttendantSaidToGetForint && LeaveForintOnTheGroundPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING && !LeaveForintPlayedThisInteraction)
        {
            LeaveForintOnTheGround.start();
            LeaveForintPlayedThisInteraction = true;
        }
        if (LockViewScript.LockedWithForint && DialogManager.BathroomAttendantSaidToGetForint)
        {
            textGraphic.SetActive(true);
        }
        if (!LockViewScript.LockedWithForint)
        {
            LeaveForintPlayedThisInteraction = false;
        }
    }
}
