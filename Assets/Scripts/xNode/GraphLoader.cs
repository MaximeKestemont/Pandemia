using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// This class will be responsible of loading all events, choices and choiceConsequences
// from the xNode graph, and to instante objects based on it
public class GraphLoader: MonoBehaviour
{
    public ResourceManager resourceManager;

    public Dictionary<int, GameObject> gameEvents {get;set;}
    public List<GameObject> eventObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start parsing xNode graph...");
        // TODO change the string resource name here to load another event graph
        EventChoiceGraph graph = Resources.Load("TestGraph2") as EventChoiceGraph;

        var eventsParent = GameObject.Find("Events").transform;

        Debug.Log($"Nodes count: {graph.CountNodes()}");

        // Loop over events and create corresponding game objects
        List<EventNode> events = graph.GetEventNodes();
        foreach (var eventNode in events) {
            GameObject newEventObject = Instantiate(resourceManager.eventPrefab, eventsParent);

            Event newEvent = newEventObject.GetComponent<Event>();
            newEvent.title = eventNode.title;
            newEvent.uid = eventNode.uid;
            newEvent.status = eventNode.status;
                        
            newEventObject.SetActive(false);
            eventObjects.Add(newEventObject);
            Debug.Log($"Event {newEvent.uid} has been properly created");
        }

        gameEvents = eventObjects.ToDictionary(x => x.GetComponent<Event>().uid);

        // Loop over dialogues, create corresponding games objects, their choices objects and link dialogue to event
        // We loop 2 times, as we first need to instantiate all dialogues before being able to link the choices to it,
        // as a choice could be connected to a not yet identified dialogue
        // Note: could be refactored with a recursive method + a stack of not yet accessed dialogue
        Dictionary<int, GameObject> dialoguesDict = new Dictionary<int, GameObject>(); // needed to retrieve it later and link it to choices
        List<NPCDialogueNode> dialogues = graph.GetDialogueNodes();
        foreach(var dialogueNode in dialogues) {
            GameObject newDialogueObject = Instantiate(resourceManager.dialoguePrefab);
            newDialogueObject.SetActive(false);

            // Fill the object variable
            NPCDialogue newDialogue = newDialogueObject.GetComponent<NPCDialogue>();
            newDialogue.character = resourceManager.charactersMap[dialogueNode.character];
            newDialogue.dialogue = dialogueNode.dialogue;

            // Link it to initial event (and activate only the first dialogue)
            var eventConnections = dialogueNode.GetInputPort("initialEvent").GetConnections();
            NPCDialogueNode previousDialogueNode = dialogueNode; 
            bool isFirstDialogue = true;

            while (eventConnections.Count() == 0) {
                ChoiceNode previousChoice = previousDialogueNode.GetInputPort("incomingChoice").GetConnections()[0].node as ChoiceNode;
                previousDialogueNode = previousChoice.GetInputPort("correspondingDialogue").GetConnections()[0].node as NPCDialogueNode;
                eventConnections = previousDialogueNode.GetInputPort("initialEvent").GetConnections();
                isFirstDialogue = false;
            }

            int initialEventId = (eventConnections[0].node as EventNode).uid;
            Event initialEvent = gameEvents[initialEventId].GetComponent<Event>();
            newDialogueObject.transform.SetParent(initialEvent.dialogueContainer.transform, false);
            if (isFirstDialogue) newDialogueObject.SetActive(true);
            
            dialoguesDict.Add(dialogueNode.uid, newDialogueObject);
        }

        
        foreach(var dialogueNode in dialogues) {
            // Retrieve the already created dialogue from its uid
            
            NPCDialogue dialogueInstance = dialoguesDict[dialogueNode.uid].GetComponent<NPCDialogue>();

            // Create choices associated to this dialogue
            var choiceConnections = dialogueNode.GetOutputPort("outcomingChoices").GetConnections();
            foreach (var choiceConnection in choiceConnections) {
                ChoiceNode choiceNode = choiceConnection.node as ChoiceNode;
                
                GameObject newChoiceObject = Instantiate(resourceManager.choicePrefab);
                newChoiceObject.transform.SetParent(dialogueInstance.choices.transform, false);

                Choice newChoice = newChoiceObject.GetComponent<Choice>();
                newChoice.title = choiceNode.description;

                // Add the following dialogue to the attribute of the newChoice instance
                var followingDialogueConnections = choiceNode.GetOutputPort("followingDialogue").GetConnections();
                foreach (var followingDialogueConnection in followingDialogueConnections) {
                    NPCDialogueNode followingDialogueNode = followingDialogueConnection.node as NPCDialogueNode;
                    newChoice.followingDialogue = dialoguesDict[followingDialogueNode.uid];
                }

                // Fill the unlock/lock event lists based on the Node output connections
                var unlockingEventConnections = choiceNode.GetOutputPort("unlockingEvents").GetConnections();
                foreach (var unlockingEventConnection in unlockingEventConnections) {
                    EventNode unlockedEventNode = unlockingEventConnection.node as EventNode;
                    newChoice.unlockedEvents.Add(unlockedEventNode.uid);
                }

                var lockingEventConnections = choiceNode.GetOutputPort("lockingEvents").GetConnections();
                foreach (var lockingEventConnection in lockingEventConnections) {
                    EventNode lockedEventNode = lockingEventConnection.node as EventNode;
                    newChoice.lockedEvents.Add(lockedEventNode.uid);
                }

                var passingEventConnections = choiceNode.GetOutputPort("passingEvents").GetConnections();
                foreach (var passingEventConnection in passingEventConnections) {
                    EventNode passedEventNode = passingEventConnection.node as EventNode;
                    newChoice.passedEvents.Add(passedEventNode.uid);
                }

                // Create the choiceConsequences as well
                foreach (var consequence in choiceNode.choiceNodeConsequences) {
                    ChoiceConsequence choiceConsequence = new ChoiceConsequence(
                        resourceManager.GetIndicatorBar(consequence.indicatorType),
                        consequence.valueType,
                        consequence.value,
                        consequence.duration
                    );
                    newChoice.choiceConsequences.Add(choiceConsequence);
                }
                
            }      
        }

        // Render the menu panel active so that the game can be started
        resourceManager.menuPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
