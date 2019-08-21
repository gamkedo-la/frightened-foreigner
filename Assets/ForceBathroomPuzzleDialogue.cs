using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBathroomPuzzleDialogue : MonoBehaviour
{

    public float timeLeft = 10.0f;
    private FMOD.Studio.EventInstance VanessaSaysComeHere;
    public bool HeyComeHereHasStarted = false;
    //public Text startText; // used for showing countdown from 3, 2, 1 

    // Start is called before the first frame update
    void Start()
    {
        VanessaSaysComeHere = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Vanessa/VanessaSaysComeHere");

    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        //Debug.Log(timeLeft);
        //startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0 && !HeyComeHereHasStarted)
        {
            VanessaSaysComeHere.start();
            HeyComeHereHasStarted = true;
            Debug.Log("should be playing");
        }
    }
    

    
}
