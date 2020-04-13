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
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
