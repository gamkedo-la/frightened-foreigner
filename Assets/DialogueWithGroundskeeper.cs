using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithGroundskeeper : MonoBehaviour
{

    private LockView LockViewScript;
    //public GameObject PlayerCamera;
    private GameObject PlayerCamera;

    public FMOD.Studio.EventInstance WheresTheBathroom;
    public bool PlayerHasAskedWhereTheBathroomIs = false;
    public bool PlayerHasLearnedWordForBathroom = false;

    public FMOD.Studio.EventInstance PlayerSaysBathroomAndGroundskeeperSaysThere;
    public bool GroundskeeperSaidThere = false;
    public FMOD.Studio.EventInstance Lightning;
    public bool LightningPlayed = false;

    public GameObject TextGraphic;

    public GameObject Vanessa;
    private DialogueWithVanessa DialogueWithVanessaScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCamera = GameObject.Find("Main Camera");
        LockViewScript = PlayerCamera.GetComponent<LockView>();

        WheresTheBathroom = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Player/WheresTheBathroom");
        PlayerSaysBathroomAndGroundskeeperSaysThere = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Player/ITriedToFindTheBathroom");
        


        Lightning = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Lightning");

        DialogueWithVanessaScript = Vanessa.GetComponent<DialogueWithVanessa>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerHasLearnedWordForBathroom);
        if (LockViewScript.LockedWithGroundskeeper && !PlayerHasAskedWhereTheBathroomIs)
        {
            WheresTheBathroom.start();
            PlayerHasAskedWhereTheBathroomIs = true;
        }
        if (LockViewScript.LockedWithGroundskeeper && DialogueWithVanessaScript.learnedFurduszoba)
        {
            TextGraphic.SetActive(true);
        }

        

        
    }
}

