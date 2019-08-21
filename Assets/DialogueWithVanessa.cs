using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithVanessa : MonoBehaviour
{

    private LockView LockViewScript;
    public GameObject PlayerCamera;

    public GameObject groundskeeper;
    public bool groundskeeperInvisible = true;

    public GameObject door;
    private DoorScript doorScript;

    private FMOD.Studio.EventInstance TellPlayerToFindTheBathroom;
    private bool PlayerHasBeenToldToFindBathroomThisInteraction = false;

    // Start is called before the first frame update
    void Start()
    {
        
        LockViewScript = PlayerCamera.GetComponent<LockView>();
        doorScript = door.GetComponent<DoorScript>();

        TellPlayerToFindTheBathroom = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Vanessa/TellPlayerToFindTheBathroom");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (LockViewScript.LockedWithVanessa && !doorScript.playerHasExploredTheCemetery && !PlayerHasBeenToldToFindBathroomThisInteraction)
        {
            TellPlayerToFindTheBathroom.start();
            PlayerHasBeenToldToFindBathroomThisInteraction = true;
        }

        if (!LockViewScript.LockedWithVanessa)
        {
            PlayerHasBeenToldToFindBathroomThisInteraction = false;
        }

        if (LockViewScript.LockedWithVanessa && groundskeeperInvisible && doorScript.playerHasExploredTheCemetery)
        {
            groundskeeper.SetActive(true);
            groundskeeperInvisible = false;
        }

        
    }
}
