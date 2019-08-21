using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool BathroomCutsceneHasPlayed = false;
    
    public GameObject vanessa;
    private DialogueWithVanessa DialogueWithVanessaScript;

    public bool playerHasExploredTheCemetery = false;

    private void Start()
    {
        DialogueWithVanessaScript = vanessa.GetComponent<DialogueWithVanessa>();
    }


    private void OnTriggerExit( Collider other )
	{
		if ( !other.CompareTag( "Player" )  )
			return;

        if (BathroomCutsceneHasPlayed)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Collider>().enabled = false;
        }

        playerHasExploredTheCemetery = true;
        Debug.Log("Player has explored the cemetery");


    }
}
