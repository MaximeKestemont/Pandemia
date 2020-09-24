using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{

    public int uid;
    public string objectiveMessage;
    public bool isDone;

    public Text objectiveText;
    public Toggle checkBox;


    void Start()
    {
        this.objectiveText.text = this.objectiveMessage;
        this.checkBox.isOn = this.isDone;

    }


    public void CheckObjective() {
        this.isDone = true;
        this.checkBox.isOn = this.isDone;
    }
}