using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWords : MonoBehaviour
{

    public List<string> MasterListOfAnswerChoices = new List<string>();
    
    public List<string> ListOfChoicesForThisTextGraphic = new List<string>();

    public int RandomInteger;

    public string CurrentStringToAddToAnswerChoicesList;

    private void Awake()
    {

    }

    public void ShuffleList()
    {
        Debug.Log("This is being enabled: " + gameObject.name);

        MasterListOfAnswerChoices.Add("fürdőszoba");//bathroom
        MasterListOfAnswerChoices.Add("gyógyszert");//medicine
        MasterListOfAnswerChoices.Add("cukorkát");//candy
        MasterListOfAnswerChoices.Add("tej");//milk
        MasterListOfAnswerChoices.Add("forint");//money
        MasterListOfAnswerChoices.Add("Tűz");//fire
        MasterListOfAnswerChoices.Add("szél");//wind
        MasterListOfAnswerChoices.Add("föld");//earth
        MasterListOfAnswerChoices.Add("víz");//water

        for (int i = 0; i < 9; i++)
        {

           // Debug.Log("Master List Count: " + MasterListOfAnswerChoices.Count);
            RandomInteger = Random.Range(0, MasterListOfAnswerChoices.Count);//grab a random integer from current length of list
           // Debug.Log("Random Integer: " + RandomInteger);
            CurrentStringToAddToAnswerChoicesList = MasterListOfAnswerChoices[RandomInteger];//pull the string from the random index of the list
           // Debug.Log("CurrentStringToAddToAnswerChoicesList: " + CurrentStringToAddToAnswerChoicesList);
            ListOfChoicesForThisTextGraphic.Add(CurrentStringToAddToAnswerChoicesList);//add the string to the list of answer choices
            MasterListOfAnswerChoices.RemoveAt(RandomInteger);//remove the added choice from the transferable list to prevent redundancy
        }

        MouseClicks.ImageNameList = ListOfChoicesForThisTextGraphic;
        /*for (int i = 0; i < ListOfChoicesForThisTextGraphic.Count; i++)
        {
            Debug.Log(ListOfChoicesForThisTextGraphic[i]);
        }*/
    }
}
