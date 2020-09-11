using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecapPanel : MonoBehaviour
{
    public ResourceManager resourceManager;

    public Text dayText;

    // Start is called before the first frame update
    void Start()
    {    
    }

    public IEnumerator WaitAndClose() {
        dayText.text = "Day " + resourceManager.gameController.day;
        yield return new WaitForSeconds(3);
        resourceManager.recapPanel.SetActive(false);
    }


}
