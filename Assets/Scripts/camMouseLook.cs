using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour
{
    public Transform canvas;

    Vector2 mouselook; //change per frame in mouse movement
    Vector2 smoothV; //smoothing of changes
    public float sensitivity = 5.0f; // offset for how much change in mouse movement needs to happen to trigger changes
    public float smoothing = 2.0f; // smoothing factor

    GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1f / smoothing);
            mouselook += smoothV;

            transform.localRotation = Quaternion.AngleAxis(-mouselook.y, Vector2.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouselook.x, character.transform.up);
        }
        
    }
}
