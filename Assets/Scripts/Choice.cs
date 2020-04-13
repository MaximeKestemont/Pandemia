using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice
{
    public string title {get;}

    public Choice(
        string title
    ) {
        this.title = title;
    }
    
    /*
	=====================
	Execute
	=====================
	Apply the effects and consequences when this choice is selected
	*/
    public void Execute() {
        Debug.Log($"Execute choice {this.title}");
    }
}
