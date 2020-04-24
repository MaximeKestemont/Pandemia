using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generate game data (events, choices, etc.)
public class GameDataGenerator: MonoBehaviour
{

    public Dictionary<int, GameObject> events {get;set;}
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
            new List<ChoiceConsequence>{consequence11, consequence12},
            new List<int>{3},
            new List<int>(),
            new List<int>()
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
            new List<ChoiceConsequence>{consequence21, consequence22},
            new List<int>(),
            new List<int>{2},
            new List<int>()
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
            new List<ChoiceConsequence>(),
            new List<int>(),
            new List<int>(),
            new List<int>()
        );
        GameObject choice4 = CreateChoice(
            "Briquet >>>> Max",
            new List<ChoiceConsequence>(),
            new List<int>(),
            new List<int>(),
            new List<int>()
        );
        List<GameObject> choiceList2 = new List<GameObject>{choice3, choice4};
        GameObject event2 = CreateEvent(
            2,
            Event.Status.UNLOCKED,
            "Alex est un noob",
            choiceList2
        );

        // Event3
        GameObject choice31 = CreateChoice(
            "Choice 31",
            new List<ChoiceConsequence>(),
            new List<int>(),
            new List<int>(),
            new List<int>()
        );
        GameObject choice32 = CreateChoice(
            "Choice 32",
            new List<ChoiceConsequence>(),
            new List<int>(),
            new List<int>(),
            new List<int>()
        );
        List<GameObject> choiceList3 = new List<GameObject>{choice31, choice32};
        GameObject event3 = CreateEvent(
            3,
            Event.Status.LOCKED,
            "Optional event",
            choiceList3
        );

        // EventList
        
        this.events = new Dictionary<int, GameObject>{
            {event1.GetComponent<Event>().uid, event1}, 
            {event2.GetComponent<Event>().uid, event2},
            {event3.GetComponent<Event>().uid, event3}
        };
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
        List<ChoiceConsequence> choiceConsequences,
        List<int> unlockedEvents,
        List<int> lockedEvents,
        List<int> passedEvents
    ) {
        GameObject newChoiceObject = Instantiate(resourceManager.choicePrefab);
        Choice newChoice = newChoiceObject.GetComponent<Choice>();
        newChoice.title = title;
        newChoice.choiceConsequences = choiceConsequences;
        newChoice.unlockedEvents = unlockedEvents;
        newChoice.lockedEvents = lockedEvents;
        newChoice.passedEvents = passedEvents;
        return newChoiceObject; 
    }
}
