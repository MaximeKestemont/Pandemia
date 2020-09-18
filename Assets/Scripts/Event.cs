using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
Event can be locked, unlocked or passed, depending on several conditions:
- several EventCondition (only one needs to be valid), which themselves can consist of several constraints (that all need to be valid)
- previous choices that locked/unlocked the Event
If those 2 conditions are present (EventCondition and Choices), BOTH needs to be valid to unlock the Event.

To model this, there is 2 status, one global and one specific to the choices ("unlockedByChoice"). The rational behind this
second status is that we need to be able to know if the event has been unlocked by a choice or not, and we cannot track this
with only one status.
Example:
1. Event1 is locked by default, and have 1 EventCondition1 attached to it, as well as one previous choice (choice1) that can unlock it.
2. Choice1 is not executed. It thus does not unlock the Event1, also because EventCondition1 is still not valid.
3. Another event is played, and the consequences make EventCondition1 to be valid.
4. EventCondition1 is now valid, but the Event1 should still be locked as Choice1 has never been executed.!--
With only one global state, the above cannot be modeled properly, as we would have "forgotten" that Choice1 was never executed.
*/
public class Event : MonoBehaviour
{
    public int uid; // Unique identifier of the event
    public Status status; // Will determine if this event can appear as next event or not
    
    public Status unlockedByChoice;

    public string title;
    public Text textTitle;
    public Image eventImage;

    public GameObject dialogueContainer;

    // Conditions required to unlocked the event. It only needs to validate one EventCondition, but inside an EventCondition,
    // all constraints need to be validated
    public List<EventCondition> eventConditions;

    // Condition to lock/unlock events based on a specific phase - if Normal, no constraint applied
    public ResourceManager.Phase eventPhase;

    // Start is called before the first frame update
    void Start()
    {
        textTitle.GetComponent<Text>().text = this.title;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum Status {
        UNLOCKED,   // event that can occur as next event
        LOCKED,     // event that cannot occur as next event for now
        PASSED      // event that cannot occur as next event, as it has already been done or permanently removed
        }
}
