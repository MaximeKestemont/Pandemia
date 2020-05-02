using UnityEngine;

public abstract class ValueOverTime {
	public abstract int value {get; set;}
	public abstract int timeLeft {get; set;}

	public abstract int ApplyValue(int indicatorValue);
}

public class AbsoluteValueOverTime: ValueOverTime {
	public override int value {get; set;}
	public override int timeLeft {get; set;}
		
	public AbsoluteValueOverTime(
		int value,
		int timeLeft
	) {
		this.value = value;
		this.timeLeft = timeLeft;
	}

    public override int ApplyValue(int indicatorValue) {
        return (indicatorValue + this.value);
    }
}

public class PercentageValueOverTime: ValueOverTime {
	public override int value {get; set;}
	public override int timeLeft {get; set;}
		
	public PercentageValueOverTime(
		int value,
		int timeLeft
	) {
		this.value = value;
		this.timeLeft = timeLeft;
	}

    // +5% -> 105% of indicatorValue
    // -5% -> 95% of indicatorValue
    public override int ApplyValue(int indicatorValue) {    
        return (int)((100f + this.value) / 100 * indicatorValue);
    }
}
