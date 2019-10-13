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

    public GameObject playerCamera;
    private LockView lockViewScript;

    public static bool endingCutscene = false;

    // Start is called before the first frame update
    void Start()
    {
        swordAnimator = sword.GetComponent<Animator>();
        lockViewScript = playerCamera.GetComponent<LockView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lockOnToTurul()
    {
        endingCutscene = true;
        lockViewScript.randomWordBool = false;
        lockViewScript.locked = true;
        LockView.endingCutsceneLockWithTurul = true;
        
    }

    public void lockOnToSword()
    {
        LockView.endingCutsceneLockWithTurul = false;
        LockView.endingCutsceneLockWithSword = true;
        LockView.endingCutsceneLockWithDragon = false;
    }

    public void lockOnToDragon()
    {
        LockView.endingCutsceneLockWithSword = false;
        LockView.endingCutsceneLockWithDragon = true;
    }

    public void lockOnToKey()
    {
        LockView.endingCutsceneLockWithSword = false;
        LockView.endingCutsceneLockWithDragon = false;
        LockView.endingCutsceneLockWithKey = true;
    }

    public void activateAndAnimateSword()
    {
        sword.SetActive(true);
        lockOnToSword();
        //swordAnimator.enabled = true;
    }

    public void activateAndAnimateDragon()
    {
        dragon.SetActive(true);
        lockOnToDragon();
        //dragonAnimator.enabled = true;
    }

    

    public void pickUpSwordAndThrowAnimation()
    {
        swordAnimator.Play("pickUpSwordAndThrowAtDragon");
        lockOnToSword();
    }

    public void keyDropAnimation()
    {
        dragon.SetActive(false);
        sword.SetActive(false);
        key.SetActive(true);
        lockViewScript.locked = true;
        lockOnToKey();
    }
}
