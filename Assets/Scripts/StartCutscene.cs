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

    public GameObject tutorialUIHolder;

    void Start() // disable player control scripts at start
    {
        player.GetComponent<characterController>().enabled = false;
        player.GetComponentInChildren<camMouseLook>().enabled = false;
        StartCoroutine(PlayCutscene());
        StartCoroutine(ShowControlsScreen());
    }

    IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(lookAtTheGateTime);

        float startTime = Time.time; // while loop start time
        while (Time.time - startTime < lookAtTurulTime)
        {
            // rotates the player only on y axis
            Vector3 lookAtTarget = new Vector3(targetObject.position.x,
                                               player.transform.position.y,
                                               targetObject.position.z);
            var targetRotation = Quaternion.LookRotation(lookAtTarget - player.transform.position);

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

    IEnumerator ShowControlsScreen()
    {
        yield return new WaitForSeconds(lookAtTheGateTime + lookAtTurulTime);
        tutorialUIHolder.SetActive(true);
        PauseGame.GamePaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }
}
