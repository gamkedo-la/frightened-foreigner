﻿using UnityEngine;

public class Door : MonoBehaviour
{
    public bool BathroomCutsceneHasPlayed = false;
    //nonsense to make a commit

	private void OnTriggerExit( Collider other )
	{
		if ( !other.CompareTag( "Player" )  )
			return;

        if (BathroomCutsceneHasPlayed)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Collider>().enabled = false;
        }
		
	}
}
