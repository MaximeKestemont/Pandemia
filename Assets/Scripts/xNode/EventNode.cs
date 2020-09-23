using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#3459eb")]
public class EventNode : Node {
	public string title;
	public int uid;
	public Event.Status status;

	[Input] public ChoiceNode unlockingChoices;
	[Input] public ChoiceNode lockingChoices;
	[Input] public ChoiceNode passingChoices;

	[Output] public NPCDialogueNode dialogue;

	public ResourceManager.Phase eventPhase = ResourceManager.Phase.ALL;
	public List<EventCondition> conditions;

	// Use this for initialization
	protected override void Init() {
		base.Init();
		
		// TODO commented for now. 
		// Should uncomment to auto generate uids, but as it is random the starting event will as as well
		//this.uid = EventChoiceGraph.nextEventUid++;
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

}