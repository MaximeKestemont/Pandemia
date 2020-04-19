using System.Collections.Generic;   
/*
* Define for a given indicator the bonus/malus to apply to it and its type (percentage, absolute value, etc.)
* If the duration is 0, it will be a one shot bonus or malus. Else, it will last X events.
*/
public class ChoiceConsequence {
    public IndicatorBar indicatorBar;
    public ValueTypeEnum valueType;
    public int value;
    public int duration;

    // List of event ids that will be unlocked/locked/passed (= permanently locked) once the choice is executed        
    public List<int> unlockedEvents = new List<int>();
    public List<int> lockedEvents = new List<int>();
    public List<int> passedEvents = new List<int>();

    public ChoiceConsequence(
        IndicatorBar indicatorBar,
        ValueTypeEnum valueType,
        int value,
        int duration,
        List<int> unlockedEvents,
        List<int> lockedEvents,
        List<int> passedEvents
    ) {
        this.indicatorBar = indicatorBar;
        this.valueType = valueType;
        this.value = value;
        this.duration = duration;

        this.unlockedEvents = unlockedEvents;
        this.lockedEvents = lockedEvents;
        this.passedEvents = passedEvents;
    }

    public enum ValueTypeEnum {ABSOLUTE, PERCENTAGE};
}