using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GraphLoader graphLoader;
    public RecapPanel recapPanel;
    
    public GameOverPanel gameOverPanel;

    public int currentEventId;
    private GameObject currentEvent;

    private Virus virus; // virus selected for the game
    private IndicatorBar indicatorBar;

    private int eventCount = 0;

    // Counter to track the player progress
    public int day = 0;

    public void StartGame()
    {
        Debug.Log ("Starting the game...");
        resourceManager.gameOverPanel.SetActive(false);
        resourceManager.menuPanel.SetActive(false);
        resourceManager.recapPanel.SetActive(false);

        Debug.Log ("Initializing game data and storing them in resource manager...");
        // Init events
        resourceManager.eventsMap = graphLoader.gameEvents;
        
        // Chose a virus
        this.virus = resourceManager.vonCreed666; // TODO should be randomized/selected by the player
        Debug.Log ($"Virus {this.virus.name} has been selected for this game");
        
        // Activate first event
        currentEventId = 0;
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
    
        Debug.Log("Checking if game is not over");
        eventCount++;

        if (CheckLosingConditions()) {
            FinishGame(false, GameOverPanel.losingMessage);
        } else if (CheckWinningConditions()) {
            FinishGame(true, GameOverPanel.knowledgeMessage);
        } else {

        Debug.Log("Updating the day counter and displaying the corresponding panel");
        this.day += 1;
        RecapPanel recapPanel = resourceManager.recapPanel.GetComponent<RecapPanel>();
        recapPanel.gameObject.SetActive(true);
        recapPanel.StartCoroutine(recapPanel.WaitAndClose());

        // check if events are meeting conditions to be unlocked/locked
        Debug.Log("Checking if events should be unlocked or locked");
        foreach(GameObject eventObject in resourceManager.eventsMap.Values) {
            Event eventInstance = eventObject.GetComponent<Event>();

            if (eventInstance.status != Event.Status.PASSED && eventInstance.eventConditions.Count > 0) {
                bool unlocked = false;
                
                // If one EventCondition is fully met, unlock the event. Else, it should be locked.
                // unlocked = EventCondition1 OR EventCondition2, but EventCondition1 = Condition1 AND Condition2
                foreach(EventCondition eventCondition in eventInstance.eventConditions) {
                    bool allConditionsChecked = true;

                    // Check that all conditions in an EventCondition are met
                    foreach(EventCondition.Condition condition in eventCondition.conditions) {
                        bool conditionChecked;
                        if (condition.conditionType == EventCondition.ConditionType.ABOVE) {
                            conditionChecked = resourceManager.GetIndicatorBar(condition.indicatorType).fillingLevel >= condition.threshold;
                        } else {
                            conditionChecked = resourceManager.GetIndicatorBar(condition.indicatorType).fillingLevel < condition.threshold;
                        }
                        if (!conditionChecked) {
                            allConditionsChecked = false;
                        } 
                    }
                    
                    // Only setting it to true (optionnally), as we do not want to erase a previous True
                    if (allConditionsChecked) {
                        unlocked = true;
                    }
                }

                if (unlocked) {
                    eventInstance.status = Event.Status.UNLOCKED;
                } else {
                    eventInstance.status = Event.Status.LOCKED;
                }
            }
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
            FinishGame(true, GameOverPanel.survivingMessage);
        }
        }
    }

    public bool CheckLosingConditions() {
        bool economyCondition = resourceManager.economy.fillingLevel == resourceManager.economy.MIN_VALUE;
        bool deathCondition = resourceManager.death.fillingLevel == resourceManager.death.MAX_VALUE;
        bool populationCondition = resourceManager.population.fillingLevel == resourceManager.population.MIN_VALUE;
        bool satisfactionCondition = resourceManager.populationSatisfaction.fillingLevel == resourceManager.populationSatisfaction.MIN_VALUE;
        return economyCondition || deathCondition || populationCondition || satisfactionCondition;
    }

    public bool CheckWinningConditions() {
        bool eventsCondition = eventCount > 10;
        bool virusKnowledgeCondition = resourceManager.virusKnowledge.fillingLevel == resourceManager.virusKnowledge.MAX_VALUE;
        bool infectedNumberCondition = resourceManager.infectedNumber.fillingLevel == resourceManager.infectedNumber.MIN_VALUE;
        return (virusKnowledgeCondition || infectedNumberCondition) && eventsCondition;
    }

    public void FinishGame(bool isWinner, string endingMessage) {
        gameOverPanel.SetEndingScreen(isWinner, endingMessage);
        resourceManager.gameOverPanel.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
