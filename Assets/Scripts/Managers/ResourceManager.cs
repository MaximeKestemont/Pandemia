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
    public GameObject dialoguePrefab;

    // --- GameObjects ---
    // Events
    public Dictionary<int, GameObject> eventsMap;

    // Characters (name -> object)
    public GameObject characters;
    public Dictionary<Character.CharacterName, Character> charactersMap = new Dictionary<Character.CharacterName, Character>();
    
    // Indicators
    public IndicatorBar health;
    public IndicatorBar economy;
    public IndicatorBar propagationSpeed;
    public IndicatorBar populationSatisfaction;
    public IndicatorBar infectedNumber;
    public IndicatorBar population;
    public IndicatorBar death;
    public IndicatorBar cured;
    public IndicatorBar virusKnowledge;

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

        // Fill characters dict
        foreach(Character character in this.characters.GetComponentsInChildren<Character>()) {
            charactersMap.Add(character.characterName, character);
        }

        this.indicators = new List<IndicatorBar>{
            health, 
            economy, 
            propagationSpeed, 
            populationSatisfaction, 
            infectedNumber,
            population,
            death,
            cured,
            virusKnowledge
        };

        // Hide some indicators at the start
        infectedNumber.SetVisible(false);
        death.SetVisible(false);
        virusKnowledge.SetVisible(false);
        propagationSpeed.SetVisible(false);
        cured.SetVisible(false);
    }

    /*
	=====================
	GetIndicatorBar
	=====================
	Mapping between the enum type from ChoideNode (i.e. internalType) and the actual game object.
    This is needed to convert the consequences created in xNode editor to game objects linked to the corresponding indicators.
	*/
    public IndicatorBar GetIndicatorBar(IndicatorType internalType) {
        switch (internalType) {
            case IndicatorType.HEALTH:
                return health;
            case IndicatorType.ECONOMY:
                return economy;
            case IndicatorType.INFECTED_NUMBER:
                return infectedNumber;
            case IndicatorType.POP_SATISFACTION:
                return populationSatisfaction;
            case IndicatorType.PROPAGATION_SPEED:
                return propagationSpeed;
            case IndicatorType.POPULATION:
                return population;
            case IndicatorType.DEATH:
                return death;
            case IndicatorType.CURED:
                return cured;
            case IndicatorType.VIRUS_KNOWLEDGE:
                return virusKnowledge;
            
            default:
                Debug.LogError("IndicatorType in xNode editor not supported yet!");
                return null;
        }
    }

    public enum IndicatorType {
        HEALTH,
        ECONOMY,
        POP_SATISFACTION,
        INFECTED_NUMBER,
        PROPAGATION_SPEED,
        POPULATION,
        DEATH,
        CURED,
        VIRUS_KNOWLEDGE,
        NONE // null type
    }

}