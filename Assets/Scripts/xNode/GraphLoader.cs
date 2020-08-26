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
        EventChoiceGraph graph = Resources.Load("TestGraph") as EventChoiceGraph;

        Debug.Log($"Nodes count: {graph.CountNodes()}");

        // Loop over events and create corresponding game objects
        List<EventNode> events = graph.GetEventNodes();
        foreach (var eventNode in events) {
            GameObject newEventObject = Instantiate(resourceManager.eventPrefab, GameObject.Find("Events").transform);

            Event newEvent = newEventObject.GetComponent<Event>();
            newEvent.title = eventNode.title;
            newEvent.description = eventNode.description;
            newEvent.uid = eventNode.uid;
            newEvent.status = eventNode.status;

            // Create the choices as well
            var connections = eventNode.GetOutputPort("eventChoices").GetConnections();
            foreach (var connection in connections) {
                ChoiceNode choiceNode = connection.node as ChoiceNode;
                
                GameObject newChoiceObject = Instantiate(resourceManager.choicePrefab);
                newChoiceObject.transform.SetParent(newEvent.choices.transform, false);

                Choice newChoice = newChoiceObject.GetComponent<Choice>();
                newChoice.title = choiceNode.description;
                
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

            newEventObject.SetActive(false);
            eventObjects.Add(newEventObject);
            Debug.Log($"Event {newEvent.uid} has been properly created");
        }

        gameEvents = eventObjects.ToDictionary(x => x.GetComponent<Event>().uid);

        // Render the menu panel active so that the game can be started
        resourceManager.menuPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
