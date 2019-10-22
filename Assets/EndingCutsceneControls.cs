using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCutsceneControls : MonoBehaviour
{

    public GameObject player;
    Animator playerAnimator;
    public GameObject Turul;
    Animator TurulAnimator;
    public GameObject sword;
    Animator swordAnimator;
    public GameObject Dragon;
    Animator DragonAnimator;
    public GameObject key;
    Animator keyAnimator;


    // Start is called before the first frame update
    void Start()
    {
        DragonAnimator = Dragon.GetComponent<Animator>();
        TurulAnimator = Turul.GetComponent<Animator>();
        swordAnimator = sword.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            playEndingCutscene();
        }*/
    }

    public void playEndingCutscene()
    {
        //DragonAnimator.Play("DragonEndingCutscene");
        //DragonAnimator.enabled = true;
        TurulAnimator.Play("TurulEndingCutscene");
        //sword.SetActive(true);
        //swordAnimator.enabled = true;
    }

    public void activateSword()
    {
        sword.SetActive(true);
    }
}
