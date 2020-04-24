using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System;

[NodeTint("#34eb3d")]
public class ChoiceNode : Node { 
	[Input] public EventNode correspondingEvent;
    [Output] public EventNode unlockingEvents;
    [Output] public EventNode lockingEvents;
    [Output] public EventNode passingEvents;
    

    public string description;
    public List<ChoiceNodeConsequence> choiceNodeConsequences;

	protected override void Init() {
		base.Init();
	}

    // Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

    public enum IndicatorType {
        HEALTH,
        ECONOMY,
        POP_SATISFACTION,
        INFECTED_NUMBER,
        PROPAGATION_SPEED
    }

    [Serializable]
    public class ChoiceNodeConsequence {
        public IndicatorType indicatorType;
        public ChoiceConsequence.ValueTypeEnum valueType;
        public int value;
        public int duration;

        public ChoiceNodeConsequence(
            IndicatorType indicatorType,
            ChoiceConsequence.ValueTypeEnum valueType,
            int value,
            int duration
        ) {
            this.indicatorType = indicatorType;
            this.valueType = valueType;
            this.value = value;
            this.duration = duration;
        }

    }
}