using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ChoiceNode : Node { 
	[Input] public EventNode correspondingEvent;
    [Output] public EventNode lockingEvent;
    [Output] public EventNode unlockingEvent;
    [Output] public EventNode passingEvent;

	protected override void Init() {
		base.Init();
	}

    // Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}