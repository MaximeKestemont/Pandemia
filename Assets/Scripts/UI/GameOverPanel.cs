using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {

    public Text endingTitle;
    public Button retryButton;

    public Button QuitButton;

    private string winningMessage = "Congratulation you managed to kill the virus";

    private string losingMessage = "Game Over";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEndingScreen(bool isWinning)
    {   
        if(isWinning) {
            endingTitle.GetComponent<Text>().text =  winningMessage;
        } else {
            endingTitle.GetComponent<Text>().text =  losingMessage;
        }

    }
}
