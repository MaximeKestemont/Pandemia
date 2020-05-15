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
    public GameObject gameOverPanel;
    public GameObject recapPanel;

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
    public IndicatorBar population;
    public IndicatorBar death;
    public IndicatorBar cured;

    public List<IndicatorBar> indicators;

    // Viruses
    public Virus vonCreed666;
    public Virus covid19;

    // NewStats
    public int newDeath;
    public int newCured;
    public int newInfected;
    public int newEco;
    
    

    void Start() {
        resourceManager = this;

        this.indicators = new List<IndicatorBar>{
            health, 
            economy, 
            propagationSpeed, 
            populationSatisfaction, 
            infectedNumber,
            population,
            death,
            cured
        };
    }

    /*
	=====================
	GetIndicatorBar
	=====================
	Mapping between the enum type from ChoideNode (i.e. internalType) and the actual game object.
    This is needed to convert the consequences created in xNode editor to game objects linked to the corresponding indicators.
	*/
    public IndicatorBar GetIndicatorBar(ChoiceNode.IndicatorType internalType) {
        switch (internalType) {
            case ChoiceNode.IndicatorType.HEALTH:
                return health;
            case ChoiceNode.IndicatorType.ECONOMY:
                return economy;
            case ChoiceNode.IndicatorType.INFECTED_NUMBER:
                return infectedNumber;
            case ChoiceNode.IndicatorType.POP_SATISFACTION:
                return populationSatisfaction;
            case ChoiceNode.IndicatorType.PROPAGATION_SPEED:
                return propagationSpeed;
            case ChoiceNode.IndicatorType.POPULATION:
                return population;
            case ChoiceNode.IndicatorType.DEATH:
                return death;
            case ChoiceNode.IndicatorType.CURED:
                return cured;
            default:
                Debug.LogError("IndicatorType in xNode editor not supported yet!");
                return null;
        }
    }

}