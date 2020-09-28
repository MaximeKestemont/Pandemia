using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Hold references to children component for the progress bar.
// The update of the filling is not triggered here.
public class ProgressBar : MonoBehaviour
{

    public Text taskCompleted;
    public RectTransform filling;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateProgressBar(int completedTasks, int totalTasks) {
        float scaling = (float) completedTasks / (float) totalTasks;
        filling.localScale = new Vector2(scaling, filling.localScale.y);
    }
}
