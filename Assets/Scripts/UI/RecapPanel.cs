using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecapPanel : MonoBehaviour
{
    public ResourceManager resourceManager;

    //Indicator texts
    public Text healthText;
    public Text economyText;
    public Text propagationSpeedText;
    public Text populationSatisfactionText;
    public Text infectedNumberText;
    public Text populationText;
    public Text deathText;
    public Text curedText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deathText.GetComponent<Text>().text =  $"{resourceManager.newDeath} people died this turn!";
        curedText.GetComponent<Text>().text =  $"{resourceManager.newCured} people were healed this turn!";
        infectedNumberText.GetComponent<Text>().text =  $"{resourceManager.newInfected} people wwere infected this turn!";
        economyText.GetComponent<Text>().text =  $"The economy is now: {resourceManager.newEco}";
        healthText.GetComponent<Text>().text =  $"{resourceManager.infectedNumber.fillingLevel} infected people";
        populationText.GetComponent<Text>().text =  $"Population is now: {resourceManager.population.fillingLevel}";
        healthText.GetComponent<Text>().text =  $"{resourceManager.infectedNumber.fillingLevel} infected people";
        propagationSpeedText.GetComponent<Text>().text =  $"The virus is spreading at a speed of {resourceManager.propagationSpeed.fillingLevel}";
    }
}
