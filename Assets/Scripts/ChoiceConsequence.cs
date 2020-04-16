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