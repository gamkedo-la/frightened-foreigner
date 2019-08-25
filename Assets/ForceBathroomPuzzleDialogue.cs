using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBathroomPuzzleDialogue : MonoBehaviour
{

    public float timeLeft = 180.0f;
    private FMOD.Studio.EventInstance VanessaSaysComeHere;
    public bool HeyComeHereHasStarted = false;


    //private FMOD.Studio.EventInstance tellPlayerToFindTheBathroom;
    FMOD.Studio.PLAYBACK_STATE tellPlayerToFindTheBathroomPlaybackState;
    private DialogueWithVanessa DialogueWithVanessaScript;

    // Start is called before the first frame update
    void Start()
    {
        DialogueWithVanessaScript = gameObject.GetComponent<DialogueWithVanessa>();
        //tellPlayerToFindTheBathroom = DialogueWithVanessaScript.TellPlayerToFindTheBathroom;

        VanessaSaysComeHere = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Vanessa/VanessaSaysComeHere");
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        DialogueWithVanessaScript.TellPlayerToFindTheBathroom.getPlaybackState(out tellPlayerToFindTheBathroomPlaybackState);
        
        if (timeLeft < 0 && !HeyComeHereHasStarted && tellPlayerToFindTheBathroomPlaybackState != FMOD.Studio.PLAYBACK_STATE.PLAYING && !DialogManager.ITriedToFindTheBathroomPlayed)
        {
            VanessaSaysComeHere.start();
            HeyComeHereHasStarted = true;
        }
    }
    

    
}
