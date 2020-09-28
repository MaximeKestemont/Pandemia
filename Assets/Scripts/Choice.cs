using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice: MonoBehaviour
{
    public string title {get;set;}
    public List<ChoiceConsequence> choiceConsequences = new List<ChoiceConsequence>();
    
    // some choice may render visible indicators
    public ResourceManager.IndicatorType indicatorToUnlock = ResourceManager.IndicatorType.NONE;

    // some choice may change the current phase
    public ResourceManager.Phase phaseToUnlock = ResourceManager.Phase.NONE;

    public Text choiceTitle;
    public Image choiceImage;

    public Button choiceButton;
    
    // List of event ids that will be unlocked/locked/passed (= permanently locked) once the choice is executed
    public List<int> unlockedEvents = new List<int>();
    public List<int> lockedEvents = new List<int>();
    public List<int> passedEvents = new List<int>();

    public GameObject followingDialogue = null; // next dialogue if this choice is chosen

    // Objective checked by this choice
    public int objectiveId = -1;
    
    private ResourceManager resourceManager;

    // Start is called before the first frame update
    void Start()
    {
        choiceTitle.GetComponent<Text>().text = title;
        resourceManager = ResourceManager.resourceManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /*
	=====================
	Execute
	=====================
	Apply the effects and consequences when this choice is selected
	*/
    public void Execute() {
        Debug.Log($"Execute choice {this.title}");

        // Update indicators values
        foreach(var consequence in choiceConsequences) {    
            ValueOverTime valueOverTime = null;
            switch (consequence.valueType) 
            {
                case ChoiceConsequence.ValueTypeEnum.ABSOLUTE:
                    valueOverTime = new AbsoluteValueOverTime(consequence.value, consequence.duration);
                    break;
                case ChoiceConsequence.ValueTypeEnum.PERCENTAGE:
                    valueOverTime = new PercentageValueOverTime(consequence.value, consequence.duration);
                    break;
                default:
                    Debug.Log($"The choice type {consequence.valueType} is not supported !");
                    break;

            }
            if (valueOverTime != null ) consequence.indicatorBar.AddValueOverTime(valueOverTime);
        }

        // Check objectives
        if (objectiveId > 0) {
            resourceManager.objectivesMap[objectiveId].CheckObjective();
        }

        // Change the current phase
        if (phaseToUnlock != ResourceManager.Phase.NONE) {
            resourceManager.gameController.UpdatePhase(phaseToUnlock);
        }

        // Lock/unlock other events
        foreach(var eventId in lockedEvents) {
            if(resourceManager.eventsMap[eventId].GetComponent<Event>().unlockedByChoice != Event.Status.PASSED){
                resourceManager.eventsMap[eventId].GetComponent<Event>().unlockedByChoice = Event.Status.LOCKED;
                resourceManager.eventsMap[eventId].GetComponent<Event>().status = Event.Status.LOCKED;
            }
        }
        foreach(var eventId in unlockedEvents) {
            if(resourceManager.eventsMap[eventId].GetComponent<Event>().unlockedByChoice != Event.Status.PASSED){
                resourceManager.eventsMap[eventId].GetComponent<Event>().unlockedByChoice = Event.Status.UNLOCKED;
                resourceManager.eventsMap[eventId].GetComponent<Event>().status = Event.Status.UNLOCKED;
            }
        }
        foreach(var eventId in passedEvents) {
            resourceManager.eventsMap[eventId].GetComponent<Event>().unlockedByChoice = Event.Status.PASSED;
            resourceManager.eventsMap[eventId].GetComponent<Event>().status = Event.Status.PASSED;
        }

        // Render indicators
        if (indicatorToUnlock != ResourceManager.IndicatorType.NONE) {
            resourceManager.GetIndicatorBar(indicatorToUnlock).SetVisible(true);
        }

        if (followingDialogue) {
            this.GetComponentInParent<NPCDialogue>().gameObject.SetActive(false);
            followingDialogue.SetActive(true);
            followingDialogue.GetComponent<NPCDialogue>().UpdateDiscoveredCharacter();
        } else {
            // End of event --> go to next event
            resourceManager.gameController.NextEvent();
        }
    }


}
