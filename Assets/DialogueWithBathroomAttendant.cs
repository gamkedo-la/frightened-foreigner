using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithBathroomAttendant : MonoBehaviour
{
    private LockView LockViewScript;
    public GameObject PlayerCamera;

    public FMOD.Studio.EventInstance BathroomAttendantSaysHeNeedsForint;

    // Start is called before the first frame update
    void Start()
    {
        LockViewScript = PlayerCamera.GetComponent<LockView>();
        BathroomAttendantSaysHeNeedsForint = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/BathroomAttendant/forintNeededToEnter");
    }

    // Update is called once per frame
    void Update()
    {
        if (LockViewScript.LockedWithBathroomAttendant && !DialogManager.BathroomAttendantSaidToGetForint)
        {
            BathroomAttendantSaysHeNeedsForint.start();
            DialogManager.BathroomAttendantSaidToGetForint = true;
        }
    }
}
