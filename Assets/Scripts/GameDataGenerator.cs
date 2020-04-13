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
        GameObject event1 = Instantiate(resourceManager.eventPrefab);
        event1.GetComponentInChildren<Event>().choices = choiceList1;

        // Event2
        Choice choice3 = new Choice("Choice2.1");
        Choice choice4 = new Choice("Choice2.2");
        Choice[] choices2 = {choice3, choice4};
        List<Choice> choiceList2 = new List<Choice>(choices2);
        GameObject event2 = Instantiate(resourceManager.eventPrefab);
        event2.GetComponentInChildren<Event>().choices = choiceList2;

        // EventList
        GameObject[] events = {event1, event2};
        this.events = new List<GameObject>(events);
    }

}
