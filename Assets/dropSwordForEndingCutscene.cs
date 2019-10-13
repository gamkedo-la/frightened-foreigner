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
        LockView.endingCutsceneLockWithTurul = true;
        
    }

    public void lockOnToDragon()
    {
        LockView.endingCutsceneLockWithTurul = false;
        LockView.endingCutsceneLockWithDragon = true;
    }

    public void lockOnToKey()
    {
        LockView.endingCutsceneLockWithDragon = false;
        LockView.endingCutsceneLockWithKey = true;
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
        lockViewScript.locked = true;
        lockViewScript.LockOnToTargetObject(key.transform.position);
    }
}
