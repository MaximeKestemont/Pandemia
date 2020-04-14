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
        // Event1
        GameObject choice1 = CreateChoice(
            "Alex > Max"
        );
        GameObject choice2 = CreateChoice(
            "Briquet > Max"
        );
        List<GameObject> choiceList1 = new List<GameObject>{choice1, choice2};
        GameObject event1 = CreateEvent(
            "My first event",
            choiceList1
        );

        // Event2
        GameObject choice3 = CreateChoice(
            "Alex >>> Max"
        );
        GameObject choice4 = CreateChoice(
            "Briquet >>>> Max"
        );
        List<GameObject> choiceList2 = new List<GameObject>{choice3, choice4};
        GameObject event2 = CreateEvent(
            "Alex est un noob",
            choiceList2
        );

        // EventList
        Debug.Log(event1.transform.position);
        this.events = new List<GameObject>{event1, event2};
    }

    private GameObject CreateEvent(
        string title,
        List<GameObject> choices
    ) {
        GameObject newEvent = Instantiate(resourceManager.eventPrefab, GameObject.Find("Events").transform);
        newEvent.GetComponentInChildren<Event>().title = title;

        foreach(var choice in choices)
        {
            choice.transform.SetParent(newEvent.transform, false);
        }

        newEvent.SetActive(false);
        return newEvent; 
    }

    private GameObject CreateChoice(
        string title
    ) {
        GameObject newChoice = Instantiate(resourceManager.choicePrefab);
        newChoice.GetComponentInChildren<Choice>().title = title;
        return newChoice; 
    }
}
