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
            if (valueOverTime != null )consequence.indicatorBar.AddValueOverTime(valueOverTime);
        }

        ResourceManager.resourceManager.gameController.NextEvent();
    }


}
