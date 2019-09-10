using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithCharlie : MonoBehaviour
{

    public static FMOD.Studio.EventInstance IDontFeelWell;
    public static FMOD.Studio.EventInstance AskingForMedicine;
    public static FMOD.Studio.EventInstance CorrectWordForMedicineResponse;
    public static FMOD.Studio.EventInstance IncorrectWordForMedicineResponse;
    public static FMOD.Studio.EventInstance StillDontKnowWordForMedicine;


    // Start is called before the first frame update
    void Start()
    {
        IDontFeelWell = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Charlie/IDontFeelWell");
        AskingForMedicine = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Charlie/AskingForMedicine");
        CorrectWordForMedicineResponse = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Charlie/CorrectWordForMedicine");
        IncorrectWordForMedicineResponse = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Charlie/IncorrectWordForMedicine");
        StillDontKnowWordForMedicine = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Charlie/StillDontKnowWordForMedicine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
