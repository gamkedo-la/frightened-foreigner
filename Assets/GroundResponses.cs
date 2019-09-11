using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundResponses : MonoBehaviour
{

    public FMOD.Studio.EventInstance groundDoesntCare;

    // Start is called before the first frame update
    void Start()
    {
        groundDoesntCare = FMODUnity.RuntimeManager.CreateInstance("event:/ItemInteractions/GroundDoesntCare");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CorrectInteraction()
    {
    }

    public void IncorrectResponse()
    {
        groundDoesntCare.start();
    }
}
