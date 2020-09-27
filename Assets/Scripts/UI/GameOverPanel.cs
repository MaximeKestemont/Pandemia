using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour {

    public Text endingTitle;
    public Button retryButton;

    public Button QuitButton;

    // Winning messages
    public static string knowledgeMessage = "Congratulations, you managed to kill the virus";
    public static string survivingMessage = "Congratulations, you managed to survive long enough !";

    // Losing messages
    public static string losingMessage = "Game Over";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEndingScreen(bool isWinning, string endingMesage)
    {   
        if (isWinning) {
            endingTitle.GetComponent<Text>().text = endingMesage;
        } else {
            endingTitle.GetComponent<Text>().text = endingMesage;
        }

    }
}
