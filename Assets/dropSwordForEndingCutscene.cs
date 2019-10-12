using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropSwordForEndingCutscene : MonoBehaviour
{

    public GameObject sword;
    Animator swordAnimator;

    public GameObject dragon;
    Animator dragonAnimator;

    public GameObject key;
    Animator keyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        swordAnimator = sword.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateAndAnimateSword()
    {
        sword.SetActive(true);
        //swordAnimator.enabled = true;
    }

    public void activateAndAnimateDragon()
    {
        dragon.SetActive(true);
        //dragonAnimator.enabled = true;
    }

    public void pickUpSwordAndThrowAnimation()
    {
        swordAnimator.Play("pickUpSwordAndThrowAtDragon");
        
    }

    public void keyDropAnimation()
    {
        dragon.SetActive(false);
        sword.SetActive(false);
        key.SetActive(true);
    }
}
