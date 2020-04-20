using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GameDataGenerator gameDataGenerator;
    public int currentEventId;
    private GameObject currentEvent;

    public void StartGame()
    {
        Debug.Log ("Starting the game...");
        resourceManager.menuPanel.SetActive(false);

        Debug.Log ("Initializing game data and storing them in resource manager...");
        // Init events
        resourceManager.eventsMap = gameDataGenerator.events;;
        
        // Activate first event
        currentEventId = 1;
        currentEvent = resourceManager.eventsMap[currentEventId];
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


        // Candidate ids for next event = all "UNLOCKED" events
        List<int> filteredIds = resourceManager.eventsMap
            .Where(x => x.Value.GetComponent<Event>().status == Event.Status.UNLOCKED)
            .Select(x => x.Key)
            .ToList();

        // Select the next event id and the correspond event, or finish the game
        if (filteredIds.Count > 0) {
            var randomIndex = Random.Range(0, filteredIds.Count); 
            currentEventId = filteredIds[randomIndex];
            Debug.Log($"New event: {currentEventId}");
            currentEvent = resourceManager.eventsMap[currentEventId];
            currentEvent.SetActive(true);
        } else {
            FinishGame();
        }
        
    }

    public void FinishGame() {
        Debug.Log("No more event. Game finished.");
    }
    
}
