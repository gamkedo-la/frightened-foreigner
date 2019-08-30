using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithBathroomAttendant : MonoBehaviour
{
    private LockView LockViewScript;
    public GameObject PlayerCamera;

    public FMOD.Studio.EventInstance BathroomAttendantSaysHeNeedsForint;
    public FMOD.Studio.EventInstance BathroomAttendantSaysThankYou;

    private bool BathroomAttendantHasSaidThankYou = false;

    // Start is called before the first frame update
    void Start()
    {
        LockViewScript = PlayerCamera.GetComponent<LockView>();
        BathroomAttendantSaysHeNeedsForint = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/BathroomAttendant/forintNeededToEnter");
        BathroomAttendantSaysThankYou = FMODUnity.RuntimeManager.CreateInstance("event:/Dialogue/BathroomAttendant/bathroomAttendantSaysThankYou");
    }

    // Update is called once per frame
    void Update()
    {
        if (LockViewScript.LockedWithBathroomAttendant && !DialogManager.BathroomAttendantSaidToGetForint)
        {
            BathroomAttendantSaysHeNeedsForint.start();
            DialogManager.BathroomAttendantSaidToGetForint = true;
            StartCoroutine(ChangeBathroomAttendantSprite());
        }
        if (LockViewScript.LockedWithBathroomAttendant && InventoryItemManager.playerHasForint && !BathroomAttendantHasSaidThankYou)
        {
            BathroomAttendantSaysThankYou.start();
            BathroomAttendantHasSaidThankYou = true;
        }
    }

    private IEnumerator ChangeBathroomAttendantSprite()
    {
        yield return new WaitForSeconds(6.0f);
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/bathroomAttendantHinting");
    }
}
