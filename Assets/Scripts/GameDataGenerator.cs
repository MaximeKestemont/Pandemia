using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generate game data (events, choices, etc.)
public class GameDataGenerator: MonoBehaviour
{

    public List<GameObject> events {get;set;}
    public ResourceManager resourceManager;

    public void Start()
    {
        // --- Event1 ---
        // Choice 1
        ChoiceConsequence consequence11 = new ChoiceConsequence(
            resourceManager.economy,
            ChoiceConsequence.ValueTypeEnum.ABSOLUTE,
            10,
            0
        );
        ChoiceConsequence consequence12 = new ChoiceConsequence(
            resourceManager.propagationSpeed,
            ChoiceConsequence.ValueTypeEnum.ABSOLUTE,
            -5,
            3
        );

        GameObject choice1 = CreateChoice(
            "Alex > Max",
            new List<ChoiceConsequence>{consequence11, consequence12}
        );

        // Choice 2
        ChoiceConsequence consequence21 = new ChoiceConsequence(
            resourceManager.economy,
            ChoiceConsequence.ValueTypeEnum.PERCENTAGE,
            10,
            0
        );
        ChoiceConsequence consequence22 = new ChoiceConsequence(
            resourceManager.propagationSpeed,
            ChoiceConsequence.ValueTypeEnum.PERCENTAGE,
            -5,
            1
        );

        GameObject choice2 = CreateChoice(
            "Briquet > Max",
            new List<ChoiceConsequence>{consequence21, consequence22}
        );

        List<GameObject> choiceList1 = new List<GameObject>{choice1, choice2};
        GameObject event1 = CreateEvent(
            1,
            Event.Status.UNLOCKED,
            "My first event",
            choiceList1
        );

        // Event2
        GameObject choice3 = CreateChoice(
            "Alex >>> Max",
            new List<ChoiceConsequence>()
        );
        GameObject choice4 = CreateChoice(
            "Briquet >>>> Max",
            new List<ChoiceConsequence>()
        );
        List<GameObject> choiceList2 = new List<GameObject>{choice3, choice4};
        GameObject event2 = CreateEvent(
            2,
            Event.Status.UNLOCKED,
            "Alex est un noob",
            choiceList2
        );

        // EventList
        Debug.Log(event1.transform.position);
        this.events = new List<GameObject>{event1, event2};
    }

    private GameObject CreateEvent(
        int uid,
        Event.Status status,
        string title,
        List<GameObject> choices
    ) {
        GameObject newEvent = Instantiate(resourceManager.eventPrefab, GameObject.Find("Events").transform);
        Event e = newEvent.GetComponent<Event>();
        e.title = title;
        e.uid = uid;
        e.status = status;

        var choiceParent = e.choices;
        foreach(var choice in choices)
        {
            choice.transform.SetParent(choiceParent.transform, false);
        }

        newEvent.SetActive(false);
        return newEvent; 
    }

    private GameObject CreateChoice(
        string title,
        List<ChoiceConsequence> choiceConsequences
    ) {
        GameObject newChoice = Instantiate(resourceManager.choicePrefab);
        newChoice.GetComponent<Choice>().title = title;
        newChoice.GetComponent<Choice>().choiceConsequences = choiceConsequences;
        return newChoice; 
    }
}
