using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public bool inventoryActive;

    public GameObject inventory;
    private int NumberOfSlots;
    private GameObject[] ArrayOfSlots;

    public GameObject slotHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        inventoryActive = false;

        NumberOfSlots = 6;
        ArrayOfSlots = new GameObject[NumberOfSlots];

        for (int i = 0; i < NumberOfSlots; i++)
        {
            ArrayOfSlots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(inventory);
            if (!inventory.active)
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
}
