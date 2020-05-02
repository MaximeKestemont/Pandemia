using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GameDataGenerator gameDataGenerator;
    public GraphLoader graphLoader;
    
    public GameOverPanel gameOverPanel;

    public int currentEventId;
    private GameObject currentEvent;

    private Virus virus; // virus selected for the game
    private IndicatorBar indicatorBar;

    private int eventCount = 0;

    public void StartGame()
    {
        Debug.Log ("Starting the game...");
        resourceManager.gameOverPanel.SetActive(false);
        resourceManager.menuPanel.SetActive(false);

        Debug.Log ("Initializing game data and storing them in resource manager...");
        // Init events
        resourceManager.eventsMap = graphLoader.gameEvents;
        
        // Chose a virus
        this.virus = resourceManager.vonCreed666; // TODO should be randomized/selected by the player
        Debug.Log ($"Virus {this.virus.name} has been selected for this game");
        
        // Activate first event
        currentEventId = 1;
        currentEvent = resourceManager.eventsMap[currentEventId];
        currentEvent.SetActive(true);
        Debug.Log($"Current event: {currentEventId}");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextEvent() {
        Debug.Log("Resolving the event...");
        IndicatorBar.UpdateIndicator(resourceManager, virus);
    
        eventCount++;
        if (resourceManager.infectedNumber.fillingLevel == resourceManager.infectedNumber.MAX_VALUE) {
            FinishGame(false);
        } else if ((resourceManager.infectedNumber.fillingLevel == resourceManager.infectedNumber.MIN_VALUE) && (eventCount != 0)) {
            FinishGame(true);
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
            Debug.Log($"Current event: {currentEventId}");
            currentEvent = resourceManager.eventsMap[currentEventId];
            currentEvent.SetActive(true);
        } else {
            FinishGame(true);
        }
    }

    public void FinishGame(bool isWinner) {
        if(isWinner) {
            Debug.Log("No more event. Game finished.");
            gameOverPanel.SetEndingScreen(true);
        } else {
            Debug.Log("Game Over panel activated.");
            gameOverPanel.SetEndingScreen(true);
        }
        resourceManager.gameOverPanel.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
