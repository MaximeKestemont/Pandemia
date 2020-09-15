using System.Collections.Generic;   
using System;
/*
* Define the condition for an event to be unlocked. This does not concern locking/unlocking due to choices or phases.
*/
[Serializable]
public class EventCondition {
    // for each bar, the condition it should validate so that the event is unlocked. It should validate ALL conditions.
    public List<Condition> conditions = new List<Condition>();

    public EventCondition(
        List<Condition> conditions
    ) {
        this.conditions = conditions;
    }

    public enum ConditionType {ABOVE, BELOW};

    // indicatorBar.fillingLevel conditionType (< or >) threshold
    [Serializable]
    public class Condition {
        public ResourceManager.IndicatorType indicatorType;
        public int threshold;
        public ConditionType conditionType;

        public Condition(ResourceManager.IndicatorType indicatorType, int threshold, ConditionType conditionType) {
            this.indicatorType = indicatorType;
            this.threshold = threshold;
            this.conditionType = conditionType;
        }
    }
}