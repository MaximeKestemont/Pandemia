using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unsafe singleton object, should only be initialized once
public class ResourceManager : MonoBehaviour {
    public static ResourceManager resourceManager;

    // --- UI objects ---
    public GameObject menuPanel;

    // --- Prefab ---
    public GameObject eventPrefab;
    public GameObject choicePrefab;

    // --- GameObjects ---
    // Events
    public List<GameObject> eventsList;
    
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

    public void NextEvent() {
        Debug.Log("Resolving the event...");
        foreach (var IndicatorBar in indicators) {
            IndicatorBar.UpdateIndicator();
        }

        Debug.Log("Moving to next event...");
    }
    
}