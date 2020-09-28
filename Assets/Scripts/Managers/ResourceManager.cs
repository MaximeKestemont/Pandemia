using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Unsafe singleton object, should only be initialized once
public class ResourceManager : MonoBehaviour {
    public static ResourceManager resourceManager;

    //--- Managers ---
    public GameController gameController;

    // --- UI objects ---
    public GameObject menuPanel;
    public GameObject gameOverPanel;
    public GameObject recapPanel;
    public GameObject settingsPanel;
    public GameObject objectivePanel;
    public GameObject progressPanel;
    public GameObject galleryPanel;

    public Text currentPhaseText;

    public ProgressBar objectivesProgressBar;
    public ProgressBar galleryProgressBar;

    // --- Prefab ---
    public GameObject eventPrefab;
    public GameObject choicePrefab;
    public GameObject dialoguePrefab;
    public GameObject objectivePrefab;
    public GameObject characterPortraitPrefab;

    // --- GameObjects ---
    // Events
    public Dictionary<int, GameObject> eventsMap;

    // Objectives (id -> object)
    public Dictionary<int, Objective> objectivesMap = new Dictionary<int, Objective>();

    // Characters (name -> object)
    public GameObject characters;
    public Dictionary<Character.CharacterName, Character> charactersMap = new Dictionary<Character.CharacterName, Character>();

    // Indicators
    public IndicatorBar health;
    public IndicatorBar economy;
    public IndicatorBar propagationSpeed;
    public IndicatorBar populationSatisfaction;
    public IndicatorBar infectedNumber;
    public IndicatorBar death;
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
            death,
            virusKnowledge
        };

        // Hide some indicators at the start
        infectedNumber.SetVisible(false);
        death.SetVisible(false);
        virusKnowledge.SetVisible(false);
        propagationSpeed.SetVisible(false);

        // Hide panels
        gameOverPanel.SetActive(false);
        menuPanel.SetActive(false);
        recapPanel.SetActive(false);
        settingsPanel.SetActive(false);
        objectivePanel.SetActive(false);
        progressPanel.SetActive(false);
        galleryPanel.SetActive(false);
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
            case IndicatorType.DEATH:
                return death;
            case IndicatorType.VIRUS_KNOWLEDGE:
                return virusKnowledge;
            
            default:
                Debug.LogError("IndicatorType in xNode editor not supported yet/anymore!");
                return null;
        }
    }

    public enum IndicatorType {
        HEALTH,
        ECONOMY,
        POP_SATISFACTION,
        INFECTED_NUMBER,
        PROPAGATION_SPEED,
        POPULATION, // useless but if removed, all nodes created with values bigger in the enums will be modified
        DEATH,
        CURED, // useless but if removed, all nodes created with values bigger in the enums will be modified
        VIRUS_KNOWLEDGE,
        NONE // null type
    }

    public enum Phase {
        NONE,       // only used for the ChoiceNode, to indicate that it does not unlock any phase
        ALL,        // event can always happen, regardless of the current phase  
        NORMAL,     // event can only happen if phase is normal
        LOCKDOWN,   // event can only happen if phase is lockdown
        PARTIAL_LOCKDOWN,
        WAR,
        DICTATORSHIP
    }

}