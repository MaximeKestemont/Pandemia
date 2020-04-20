using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choice: MonoBehaviour
{
    public string title {get;set;}
    public List<ChoiceConsequence> choiceConsequences = new List<ChoiceConsequence>();

    public Text choiceTitle;
    public Image choiceImage;

    public Button choiceButton;
    
    // List of event ids that will be unlocked/locked/passed (= permanently locked) once the choice is executed
    public List<int> unlockedEvents = new List<int>();
    public List<int> lockedEvents = new List<int>();
    public List<int> passedEvents = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        choiceTitle.GetComponent<Text>().text = title;
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

        // Lock/unlock other events
        foreach(var eventId in lockedEvents) {
            ResourceManager.resourceManager.eventsMap[eventId].GetComponent<Event>().status = Event.Status.LOCKED;
        }
        foreach(var eventId in unlockedEvents) {
            ResourceManager.resourceManager.eventsMap[eventId].GetComponent<Event>().status = Event.Status.UNLOCKED;
        }
        foreach(var eventId in passedEvents) {
            ResourceManager.resourceManager.eventsMap[eventId].GetComponent<Event>().status = Event.Status.PASSED;
        }


        ResourceManager.resourceManager.gameController.NextEvent();
    }


}
