using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour
{
    public Transform PauseCanvas;
    public Transform AudioSettingsCanvas;
    public bool useAngleFix = false;

    private PauseGame PauseGameScript;
    private bool gamePaused;

    Vector2 mouselook; //change per frame in mouse movement
    Vector2 smoothV; //smoothing of changes
    public float sensitivity = 5.0f; // offset for how much change in mouse movement needs to happen to trigger changes
    public float smoothing = 2.0f; // smoothing factor

    [Space]
    public float minVerticalAngle = -60f;
    public float maxVerticalAngle = 60f;

    GameObject character;

    Quaternion ogCharacterRotation;
    Quaternion ogRotation;

    private Inventory InventoryScript;

    private void Awake()
    {
        PauseGameScript = GameObject.Find("GameController").GetComponent<PauseGame>();
        InventoryScript = GameObject.Find("Character").GetComponent<Inventory>();
        gamePaused = PauseGameScript.GamePaused;
    }

    // Start is called before the first frame update
    void Start()
    {
        ogRotation = transform.localRotation;
        ogCharacterRotation = character.transform.localRotation;
    }

	private void OnEnable( )
	{
		character = transform.parent.gameObject;

		ogRotation = transform.localRotation;
		ogCharacterRotation = character.transform.localRotation;
		mouselook = Vector2.zero;
		mouselook.y = useAngleFix ? 360 - transform.localEulerAngles.x : -transform.localEulerAngles.x;
	}

	// Update is called once per frame
	void Update()
    {
        gamePaused = PauseGameScript.GamePaused;
        //Debug.Log(gamePaused);
        if (!gamePaused && !InventoryScript.inventoryActive)
        {
            var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1f / smoothing);
            mouselook += smoothV;
            mouselook.y = Mathf.Clamp(mouselook.y, minVerticalAngle, maxVerticalAngle);

            Quaternion xRotation = Quaternion.AngleAxis(mouselook.x, Vector3.up);
            character.transform.localRotation = ogCharacterRotation * xRotation;

            Vector3 angles = ogRotation.eulerAngles;
            angles.x = -mouselook.y;
            transform.localEulerAngles = angles;
        }

    }
}
