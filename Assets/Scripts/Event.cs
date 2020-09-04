﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{
    public int uid; // Unique identifier of the event
    public Status status; // Will determine if this event can appear as next event or not

    public string title;
    public Text textTitle;
    public Image eventImage;

    public GameObject dialogueContainer;

    // Conditions required to unlocked the event. It only needs to validate one EventCondition, but inside an EventCondition,
    // all constraints need to be validated
    public List<EventCondition> eventConditions;

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
