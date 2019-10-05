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
        ListOfAllChoicesForGame.Add("cukorkát");//candy
        ListOfAllChoicesForGame.Add("tej");//milk
        ListOfAllChoicesForGame.Add("forint");//money
        ListOfAllChoicesForGame.Add("Tűz");//fire
        ListOfAllChoicesForGame.Add("szél");//wind
        ListOfAllChoicesForGame.Add("föld");//earth
        ListOfAllChoicesForGame.Add("víz");//water



    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
