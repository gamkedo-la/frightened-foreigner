using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterListOfWordChoices : MonoBehaviour
{
    public List<string> ListOfAllChoicesForGame = new List<string>();
    // Start is called before the first frame update

    private void Awake()
    {
        ListOfAllChoicesForGame.Add("fürdőszoba");//bathroom
        ListOfAllChoicesForGame.Add("gyógyszert");//medicine
        ListOfAllChoicesForGame.Add("idő");//time
        ListOfAllChoicesForGame.Add("tej");//milk
        ListOfAllChoicesForGame.Add("forint");//money
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
