using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class EventNode : Node {
	[Input] public ChoiceNode incomingChoices; // should be split between lock/unlock ?
	[Output] public ChoiceNode eventChoices;

	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}