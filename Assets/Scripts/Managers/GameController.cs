using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GameDataGenerator gameDataGenerator;

    public void StartGame()
    {
        Debug.Log ("Starting the game...");
        resourceManager.menuPanel.SetActive(false);

        Debug.Log ("Initializing game data and storing them in resource manager...");
        // Init events
        resourceManager.eventsList = gameDataGenerator.events;;
        
        // Activate first event
        GameObject firstEvent = resourceManager.eventsList[0];
        firstEvent.SetActive(true);

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
    }
    
}
