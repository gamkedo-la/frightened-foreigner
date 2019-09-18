using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueWithBathroomAttendant : MonoBehaviour
{
    private LockView LockViewScript;
    public GameObject PlayerCamera;

    public FMOD.Studio.EventInstance BathroomAttendantSaysHeNeedsForint;
    public FMOD.Studio.EventInstance BathroomAttendantSaysThankYou;
    public FMOD.Studio.EventInstance GottaGoGottaGoGottaGoGoGo;
    public FMOD.Studio.EventInstance RepeatForint;

    


    private bool BathroomAttendantHasSaidThankYou = false;

    public GameObject bathroomDoor;
    public PlayableDirector playerGoingIntoBathroomTimeline;

    // Start is called before the first frame update
    void Start()
    {
        LockViewScript = PlayerCamera.GetComponent<LockView>();
        BathroomAttendantSaysHeNeedsForint = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/BathroomAttendant/forintNeededToEnter");
        BathroomAttendantSaysThankYou = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/BathroomAttendant/bathroomAttendantSaysThankYou");
        GottaGoGottaGoGottaGoGoGo = FMODUnity.RuntimeManager.CreateInstance("event:/Bathroom Cutscene/gottaGoGottaGoGottaGoGoGo");
        RepeatForint = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/BathroomAttendant/RepeatForint");
    }

    // Update is called once per frame
    void Update()
    {
        if (LockViewScript.LockedWithBathroomAttendant && !DialogManager.BathroomAttendantSaidToGetForint)
        {
            BathroomAttendantSaysHeNeedsForint.start();
            DialogManager.BathroomAttendantSaidToGetForint = true;
            StartCoroutine(ChangeBathroomAttendantSprite());
            LockViewScript.LockedWithBathroomAttendant = false;
        }
        
        
    }

    private IEnumerator ChangeBathroomAttendantSprite()
    {
        yield return new WaitForSeconds(6.0f);
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/bathroomAttendantHinting");
    }

    private IEnumerator WaitForDoorToOpenBeforeGoingIn()
    {
        yield return new WaitForSeconds(1.25f);
        playerGoingIntoBathroomTimeline.Play();
        GottaGoGottaGoGottaGoGoGo.start();
    }

    public void OpenBathroomDoor()
    {
        BathroomAttendantSaysThankYou.start();
        BathroomAttendantHasSaidThankYou = true;
        bathroomDoor.GetComponent<Animator>().enabled = true;
        StartCoroutine(WaitForDoorToOpenBeforeGoingIn());
        PuzzleManagement.PlayerIsDoingBathroomPuzzle = false;
        PuzzleManagement.PlayerIsDoingCatPuzzle = true;
        LockViewScript.HoldItem(PlayerItem.None, null);
    }

    public void BathroomAttendantRepeatsForint()
    {
        if (DialogManager.BathroomAttendantSaidToGetForint)
        {
            RepeatForint.start();
            
        }     
    }
}
