using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform targetObject;
    [SerializeField] float lookAtSpeed = 2f;
    [SerializeField] float lookAtTheGateTime = 1f;
    [SerializeField] float lookAtTurulTime = 4f;

    void Start() // disable player control scripts at start
    {
        player.GetComponent<characterController>().enabled = false;
        player.GetComponentInChildren<camMouseLook>().enabled = false;
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(lookAtTheGateTime);

        float startTime = Time.time; // while loop start time
        while (Time.time - startTime < lookAtTurulTime)
        {
            var targetRotation = Quaternion.LookRotation(targetObject.position - player.transform.position);

            // Smoother look at action
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation,
                                                         targetRotation,
                                                         lookAtSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate(); // update the look at action in while loop
        }
        // enable player controller scripts after while loop
        player.GetComponent<characterController>().enabled = true;
        player.GetComponentInChildren<camMouseLook>().enabled = true;
    }
}
