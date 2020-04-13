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
        Choice choice1 = new Choice("Choice1.1");
        Choice choice2 = new Choice("Choice1.2");
        Choice[] choices1 = {choice1, choice2};
        List<Choice> choiceList1 = new List<Choice>(choices1);
        GameObject event1 = CreateEvent(
            "My first event",
            choiceList1
        );

        // Event2
        Choice choice3 = new Choice("Choice2.1");
        Choice choice4 = new Choice("Choice2.2");
        Choice[] choices2 = {choice3, choice4};
        List<Choice> choiceList2 = new List<Choice>(choices2);
        GameObject event2 = CreateEvent(
            "Alex est un noob",
            choiceList2
        );

        // EventList
        GameObject[] events = {event1, event2};
        Debug.Log(event1.transform.position);
        this.events = new List<GameObject>(events);
    }

    private GameObject CreateEvent(
        string title,
        List<Choice> choices
    ) {
        GameObject newEvent = Instantiate(resourceManager.eventPrefab, GameObject.Find("Events").transform);
        newEvent.GetComponentInChildren<Event>().choices = choices;
        newEvent.GetComponentInChildren<Event>().title = title;

        newEvent.SetActive(false);
        return newEvent; 
    }

}
