using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unsafe singleton object, should only be initialized once
public class ResourceManager : MonoBehaviour {
    public static ResourceManager resourceManager;

    //--- Managers ---
    public GameController gameController;

    // --- UI objects ---
    public GameObject menuPanel;

    // --- Prefab ---
    public GameObject eventPrefab;
    public GameObject choicePrefab;

    // --- GameObjects ---
    // Events
    public Dictionary<int, GameObject> eventsMap;
    
    // Indicators
    public IndicatorBar health;
    public IndicatorBar economy;
    public IndicatorBar propagationSpeed;
    public IndicatorBar populationSatisfaction;
    public IndicatorBar infectedNumber;

    public List<IndicatorBar> indicators;

    void Start() {
        resourceManager = this;

        this.indicators = new List<IndicatorBar>{health, economy, propagationSpeed, populationSatisfaction, infectedNumber};
    }

}