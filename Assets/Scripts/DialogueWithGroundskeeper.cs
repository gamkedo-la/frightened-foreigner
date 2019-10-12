using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithGroundskeeper : MonoBehaviour
{

    private LockView LockViewScript;
    
    private GameObject PlayerCamera;

    public FMOD.Studio.EventInstance WheresTheBathroom;
    
    public GameObject Furduszoba;
    public GameObject TextGraphic;
    public GameObject RandomWordsScriptHolder;

    public GameObject Vanessa;
    private DialogueWithVanessa DialogueWithVanessaScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCamera = GameObject.Find("Main Camera");
        LockViewScript = PlayerCamera.GetComponent<LockView>();

        WheresTheBathroom = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Player/WheresTheBathroom");

        DialogueWithVanessaScript = Vanessa.GetComponent<DialogueWithVanessa>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerAsksWhereTheBathroomIs()
    {
        WheresTheBathroom.start();
        DialogManager.PlayerHasAskedWhereTheBathroomIs = true;
    }

    public void PlayerAttemptsToSayBathroomToGroundskeeper()
    {
        RandomWordsScriptHolder.GetComponent<RandomWords>().ShuffleList();
        Furduszoba.SetActive(true);
        TextGraphic.SetActive(true);
    }
}

