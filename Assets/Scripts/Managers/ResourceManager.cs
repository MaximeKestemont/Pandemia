using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    // --- UI objects ---
    public GameObject menuPanel;

    // --- Prefab ---
    public GameObject eventPrefab;

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
        this.indicators = new List<IndicatorBar>{health, economy, propagationSpeed, populationSatisfaction, infectedNumber};
    }
    
}