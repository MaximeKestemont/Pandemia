using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#FFBD2E")]
public class NPCDialogueNode : Node {
	[Input] public EventNode initialEvent; // only needed for the first dialogue of an event
	[Input] public ChoiceNode incomingChoice;
	[Output] public ChoiceNode outcomingChoices;

	public Character.CharacterName character;
	public string dialogue;
	public int uid;

	// Use this for initialization
	protected override void Init() {
		base.Init();
		
		this.uid = EventChoiceGraph.nextDialogueUid++;
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

}