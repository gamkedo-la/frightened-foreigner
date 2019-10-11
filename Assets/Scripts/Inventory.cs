using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public bool inventoryActive;

    public GameObject guidebook;
    public GameObject inventory;
    private int NumberOfSlots;
    private GameObject[] ArrayOfSlots;

    public GameObject slotHolder;

    public GameObject waterBottleSlot;
    private SelectItem waterBottleSelectItemScript;

    private Image waterBottleImageComponent;

    public Sprite fullWaterBottlePicture;

    public GameObject playerCamera;
    private LockView lockViewScript;




    // Start is called before the first frame update
    void Start()
    {
        inventoryActive = false;

        NumberOfSlots = 12;
        ArrayOfSlots = new GameObject[NumberOfSlots];

        for (int i = 0; i < NumberOfSlots; i++)
        {
            ArrayOfSlots[i] = slotHolder.transform.GetChild(i).gameObject;
        }


        lockViewScript = playerCamera.GetComponent<LockView>();

        waterBottleSelectItemScript = waterBottleSlot.GetComponent<SelectItem>();


    }

    public void addObtainedItemPictureToNextAvailableSlot(Sprite inventoryImageSprite)
    {
        for (int i = 0; i < NumberOfSlots; i++)
        {
            if (ArrayOfSlots[i].GetComponent<Image>().sprite == null)
            {
                ArrayOfSlots[i].GetComponent<Image>().sprite = inventoryImageSprite;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayUISounds.UISelectionGhostVoiceSound.start();
            if (!inventory.activeInHierarchy)
            {
                inventory.SetActive(true);
                inventoryActive = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;


            } else
            {
                inventory.SetActive(false);
                inventoryActive = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

    }

	public void ShowGuideBook( )
	{
		inventory.SetActive( false );
		guidebook.SetActive( true );
	}

	public void HideGuideBook( )
	{
		inventory.SetActive( true );
		guidebook.SetActive( false );
	}

	public void Close()
	{
        PlayUISounds.UISelectionGhostVoiceSound.start();

        inventory.SetActive( false );
		guidebook.SetActive( false );
		inventoryActive = false;
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
