﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmersionLightningEmmissionToggle : MonoBehaviour
{

    private ParticleSystem EmersionLightning;
    public ParticleSystem Sparks;

    private LockView LockViewScript;
    public GameObject PlayerCamera;
    private bool lightningEmitted = false;

    public GameObject catPuzzle;

    public GameObject fene;
    private PlayFeneSFX feneSFXScript;

    public GameObject candyPuzzle;

    public GameObject elementsPuzzle;

    public FMOD.Studio.EventInstance baseLightningSound;

    public static bool catLightningEmitted = false;
    public static bool sicknessLightningEmitted = false;
    public static bool candyLightningEmitted = false;
    public static bool elementsLightningEmitted = false;

    public FMOD.Studio.EventInstance FeneLaughs;

    // Start is called before the first frame update
    void Start()
    {
        FeneLaughs = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/FeneLaughs");
        EmersionLightning = gameObject.GetComponent<ParticleSystem>();
        LockViewScript = PlayerCamera.GetComponent<LockView>();
        baseLightningSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Lightning");
        feneSFXScript = fene.GetComponent<PlayFeneSFX>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (LockViewScript.bathroomLightningCutSceneCameraPan && !lightningEmitted)
        {
            StartCoroutine(DelayLightningStrikeForBathroom());
        }

        
    }

    
    //intro bathroom puzzle
    private IEnumerator DelayLightningStrikeForBathroom()
    {
        yield return new WaitForSeconds(1.75f);
        var EmersionLightningEmitter = EmersionLightning.emission;
        EmersionLightningEmitter.enabled = true;
        EmersionLightning.Emit(1);
        Sparks.Emit(1);
        lightningEmitted = true;
    }

    //cat/milk puzzle, currently the 'first' puzzle
    public void emitLightningForCatPuzzle()
    {
       
        
        gameObject.transform.position = new Vector3(2.75f, 20.4f, -5.0f);
        var lightningPosition = FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position);
        baseLightningSound.set3DAttributes(lightningPosition);
        baseLightningSound.start();
        StartCoroutine(DelayLightningStrikeForCatPuzzle());
        StartCoroutine(DelayAppearanceOfCatPuzzle());
    }

    private IEnumerator DelayLightningStrikeForCatPuzzle()
    {
        yield return new WaitForSeconds(1.75f);
        EmersionLightning.Emit(1);
        Sparks.Emit(1);
    }

    private IEnumerator DelayAppearanceOfCatPuzzle()
    {
        yield return new WaitForSeconds(2.75f);
        catPuzzle.SetActive(true);
        catLightningEmitted = true;
        LockViewScript.UnLockView();
    }

    //sickness puzzle, currently the 'second' puzzle
    public void emitLightningForSicknessPuzzle()
    {
        
        gameObject.transform.position = new Vector3(-2.96f, 20.4f, 8.59f);
        var lightningPosition = FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position);
        baseLightningSound.set3DAttributes(lightningPosition);
        baseLightningSound.start();
        StartCoroutine(DelayLightningStrikeForSicknessPuzzle());
        StartCoroutine(DelayAppearanceOfFene());
    }

    private IEnumerator DelayLightningStrikeForSicknessPuzzle()
    {
        yield return new WaitForSeconds(2.0f);
        EmersionLightning.Emit(1);
        Sparks.Emit(1);
    }

    private IEnumerator DelayAppearanceOfFene()
    {
        yield return new WaitForSeconds(2.5f);
        fene.SetActive(true);
        //feneSFXScript.PlayFeneLaughs();
        FeneLaughs.start();
    }

    private IEnumerator DelayTurnOffLockViewForFene()
    {
        yield return new WaitForSeconds(11.0f);
            sicknessLightningEmitted = true;
        LockViewScript.UnLockView();
    }

    //candy puzzle, currently the 'third' puzzle
    public void emitLightningForCandyPuzzle()
    {
        
        gameObject.transform.position = new Vector3(10.42f, 20.4f, 4.84f);
        var lightningPosition = FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position);
        baseLightningSound.set3DAttributes(lightningPosition);
        baseLightningSound.start();
        StartCoroutine(DelayLightningStrikeForCandyPuzzle());
        StartCoroutine(DelayAppearanceOfCandyPuzzle());
    }

    private IEnumerator DelayLightningStrikeForCandyPuzzle()
    {
        yield return new WaitForSeconds(2.0f);
        EmersionLightning.Emit(1);
        Sparks.Emit(1);
    }

    private IEnumerator DelayAppearanceOfCandyPuzzle()
    {
        yield return new WaitForSeconds(2.5f);
        candyPuzzle.SetActive(true);
        LockViewScript.candyPuzzleLightningCutscene = false;
        LockViewScript.LockedWithTurul = false;
        candyLightningEmitted = true;
        LockViewScript.UnLockView();

    }

    public void emitLightningForElementsPuzzle()
    {
        
        gameObject.transform.position = new Vector3(4.77f, 20.4f, 0.79f);
        var lightningPosition = FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform.position);
        baseLightningSound.set3DAttributes(lightningPosition);
        baseLightningSound.start();
        StartCoroutine(DelayLightningStrikeForElementsPuzzle());
        StartCoroutine(DelayAppearanceOfElementsPuzzle());
    }

    private IEnumerator DelayLightningStrikeForElementsPuzzle()
    {
        yield return new WaitForSeconds(2.0f);
        EmersionLightning.Emit(1);
        Sparks.Emit(1);
    }

    private IEnumerator DelayAppearanceOfElementsPuzzle()
    {
        yield return new WaitForSeconds(2.5f);
        elementsPuzzle.SetActive(true);
        LockViewScript.elementsPuzzleLightningCutscene = false;
        LockViewScript.LockedWithTurul = false;
        elementsLightningEmitted = true;
        LockViewScript.UnLockView();

    }


}
