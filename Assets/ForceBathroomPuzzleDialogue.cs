using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBathroomPuzzleDialogue : MonoBehaviour
{

    public float timeLeft = 10.0f;
    private FMOD.Studio.EventInstance VanessaSaysComeHere;
    public bool HeyComeHereHasStarted = false; 

    // Start is called before the first frame update
    void Start()
    {
        VanessaSaysComeHere = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/Vanessa/VanessaSaysComeHere");
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
  
        if (timeLeft < 0 && !HeyComeHereHasStarted)
        {
            VanessaSaysComeHere.start();
            HeyComeHereHasStarted = true;
        }
    }
    

    
}
