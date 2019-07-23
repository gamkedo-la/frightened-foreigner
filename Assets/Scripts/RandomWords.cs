using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWords : MonoBehaviour
{
    public GameObject player;

    public List<string> AnswerChoices = new List<string>();//create a list of possible choices
    public string CorrectChoice;
    public string CurrentChoice;

    public MasterListOfWordChoices MasterListOfChoicesScript;//reference the master list of choices
    public List<string> MasterListOfChoices;

    public List<string> CurrentTransferableChoicesList;//for setting up a future flexible list of possible choices
    public int CurrentLengthOfTransferableList;
    public int RandomIntegerForTransferableListOfChoices;

    public string CurrentStringToAddToAnswerChoicesList;

    private void Awake()
    {
        player = GameObject.Find("Character");
        MasterListOfChoicesScript = player.GetComponent<MasterListOfWordChoices>();
        MasterListOfChoices = MasterListOfChoicesScript.ListOfAllChoicesForGame;
        

        CorrectChoice = gameObject.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        CurrentTransferableChoicesList = new List<string>(MasterListOfChoices);
        AnswerChoices.Add(CorrectChoice);//ensure that the correct choice is in the list

        //initialize a random word choice for the parent object        
        RandomIntegerForTransferableListOfChoices = Random.Range(0, CurrentTransferableChoicesList.Count);
        CurrentChoice = CurrentTransferableChoicesList[RandomIntegerForTransferableListOfChoices];
        


        //Remove correct choice from possible transferable choices to avoid redundancy
        for (int i = 0; i < CurrentTransferableChoicesList.Count; i++)
        {
            if (CurrentTransferableChoicesList[i] == CorrectChoice)
            {
                CurrentTransferableChoicesList.RemoveAt(i);
                break;
            }
        }

        //Add incorrect choices to choices list and remove them from transferable list to repeat multiple instances of
        //the same choices
        int wrongAnswerCount = 4;
        for (int i = 0; i < wrongAnswerCount; i++)
        {

            RandomIntegerForTransferableListOfChoices = Random.Range(0, CurrentTransferableChoicesList.Count);//grab a random integer from current length of list
            
            CurrentStringToAddToAnswerChoicesList = CurrentTransferableChoicesList[RandomIntegerForTransferableListOfChoices];//pull the string from the random index of the list
            
            AnswerChoices.Add(CurrentStringToAddToAnswerChoicesList);//add the string to the list of answer choices
            CurrentTransferableChoicesList.RemoveAt(RandomIntegerForTransferableListOfChoices);//remove the added choice from the transferable list to prevent redundancy
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
