using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GameDataGenerator gameDataGenerator;
    public int currentEventIndex;
    private GameObject currentEvent;

    public void StartGame()
    {
        Debug.Log ("Starting the game...");
        resourceManager.menuPanel.SetActive(false);

        Debug.Log ("Initializing game data and storing them in resource manager...");
        // Init events
        resourceManager.eventsList = gameDataGenerator.events;;
        
        // Activate first event
        currentEventIndex = 0;
        currentEvent = resourceManager.eventsList[currentEventIndex];
        currentEvent.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextEvent() {
        Debug.Log("Resolving the event...");
        foreach (var IndicatorBar in resourceManager.indicators) {
            IndicatorBar.UpdateIndicator();
        }

        Debug.Log("Moving to next event...");
        currentEvent.GetComponent<Event>().status = Event.Status.PASSED;
        currentEvent.SetActive(false);
        currentEventIndex++;

        // TODO need to be improved at some point to randomly chose in a specific set of event the next event
        if (currentEventIndex < resourceManager.eventsList.Count) {
            currentEvent = resourceManager.eventsList[currentEventIndex];
            currentEvent.SetActive(true);
        } else {
            FinishGame();
        }
        
    }

    public void FinishGame() {
        Debug.Log("No more event. Game finished.");
    }
    
}
