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
    }

    

    /*
    * Define for a given indicator the bonus/malus to apply to it and its type (percentage, absolute value, etc.)
    * If the duration is 0, it will be a one shot bonus or malus. Else, it will last X events.
    */
    public class ChoiceConsequence {
        public IndicatorBar indicatorBar;
        public ValueTypeEnum valueType;
        public int value;
        public int duration;

        public ChoiceConsequence(
            IndicatorBar indicatorBar,
            ValueTypeEnum valueType,
            int value,
            int duration
        ) {
            this.indicatorBar = indicatorBar;
            this.valueType = valueType;
            this.value = value;
            this.duration = duration;

        }

        public enum ValueTypeEnum {ABSOLUTE, PERCENTAGE};
    }
}
