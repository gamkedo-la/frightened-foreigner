using System.Collections;
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
    

    // Start is called before the first frame update
    void Start()
    {
        EmersionLightning = gameObject.GetComponent<ParticleSystem>();
        LockViewScript = PlayerCamera.GetComponent<LockView>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (LockViewScript.bathroomCutSceneCameraPan && !lightningEmitted)
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
    }

    //sickness puzzle, currently the 'second' puzzle
    public void emitLightningForSicknessPuzzle()
    {
        gameObject.transform.position = new Vector3(-2.96f, 20.4f, 8.59f);
        StartCoroutine(DelayLightningStrikeForSicknessPuzzle());
        StartCoroutine(DelayAppearanceOfFene());
    }

    private IEnumerator DelayLightningStrikeForSicknessPuzzle()
    {
        yield return new WaitForSeconds(1.0f);
        EmersionLightning.Emit(1);
        Sparks.Emit(1);
    }

    private IEnumerator DelayAppearanceOfFene()
    {
        yield return new WaitForSeconds(2.0f);
        fene.SetActive(true);
    }

}
