public abstract class ValueOverTime {
	public int value;
	public int timeLeft;

    public abstract int ApplyValue(int indicatorValue);
}

public class AbsoluteValueOverTime: ValueOverTime {
	public new int value;
	public new int timeLeft;
		
	public AbsoluteValueOverTime(
		int value,
		int timeLeft
	) {
		this.value = value;
		this.timeLeft = timeLeft;
	}

    public override int ApplyValue(int indicatorValue) {
        return (indicatorValue - this.value);
    }
}

public class PercentageValueOverTime: ValueOverTime {
	public new int value;
	public new int timeLeft;
		
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
        return (100 + this.value) / 100 * indicatorValue;
    }
}
