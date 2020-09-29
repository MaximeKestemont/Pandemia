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

    // Counters needed to compute the progress bar
    public static int totalObjectiveCount = 0;
    public static int checkedObjectiveCount = 0;

    void Awake() {
        totalObjectiveCount++;
    }

    void Start()
    {
        this.objectiveText.text = this.objectiveMessage;
        this.checkBox.isOn = this.isDone;
    }

    // Only called once, at the start, so that it update the progress bar
    // totalObjectiveCount is given as parameter, because Awake method is only called when the panel is loaded,
    // and the progress bar needs to be initialized before this
    public static void InitializeProgressBar(int totalObjectiveCount) {
        ResourceManager.resourceManager.objectivesProgressBar.UpdateProgressBar(checkedObjectiveCount, totalObjectiveCount);
    }

    public void CheckObjective() {
        if (!this.isDone) {
            this.isDone = true;
            this.checkBox.isOn = this.isDone;

            checkedObjectiveCount++;
            ResourceManager.resourceManager.objectivesProgressBar.UpdateProgressBar(checkedObjectiveCount, totalObjectiveCount);
        }
    }
}