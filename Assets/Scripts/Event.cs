using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    public int uid; // Unique identifier of the event
    public Status status; // Will determine if this event can appear as next event or not

    public string title;
    public string description;
    public Text textTitle;
    public Text textDescription;
    public Image eventImage;

    public GameObject choices;
    public GameObject dialogueContainer;

    // Start is called before the first frame update
    void Start()
    {
        textTitle.GetComponent<Text>().text = this.title;
        textDescription.GetComponent<Text>().text = this.description;
        
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
