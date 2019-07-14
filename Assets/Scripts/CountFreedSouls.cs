using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountFreedSouls : MonoBehaviour
{
    public int NumberOfFreedSouls = 0;//integer for keeping track of level progress
    //private FMOD.Studio.ParameterInstance FreedSouls;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseNumberOfFreedSouls()
    {
        NumberOfFreedSouls++;
    }
}
