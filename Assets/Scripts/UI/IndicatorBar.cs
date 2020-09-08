using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System;

/*
Indicator bar, handling the UI update.
Range between 0 and 100.
 */
public class IndicatorBar : MonoBehaviour {

	public GameObject filling;
	public GameObject border;
	public GameObject text;

	public string indicatorName = "DefaultName";
	public int fillingLevel = 75;
	public Color color = Color.red;

	public List<ValueOverTime> bonusAndMalus = new List<ValueOverTime>();
	
	private RectTransform borderRect;
	public int MIN_VALUE = 0;
	public int MAX_VALUE = 100;
	
	void Start () {
		borderRect = border.GetComponent<RectTransform> ();
		text.GetComponent<Text>().text = indicatorName;
		filling.GetComponent<Image>().color = color;
	}


	void Update () {
		// Update the bar rendering
		filling.transform.localScale = new Vector3 (
			(float) fillingLevel / (float) MAX_VALUE * borderRect.localScale.x, 
			filling.transform.localScale.y, 
			filling.transform.localScale.z
		);
	}

	/*
	=====================
	AddValueOverTime
	=====================
	Add a value to the list of bonus and malus. If the timeLeft <= 0, it means this is a value that will be
	applied only once. If > 0, it will last at least during one event (so be applied at least 2 times)
	*/
	public void AddValueOverTime(ValueOverTime valueOverTime) {
		bonusAndMalus.Add(valueOverTime);
	}

	protected void ApplyBonusMalus() {
		// Need this filtered list as we may remove some bonus/malus
		List<ValueOverTime> updatedBonusMalus = new List<ValueOverTime>();

		foreach (var valueOverTime in bonusAndMalus) {
			valueOverTime.timeLeft = valueOverTime.timeLeft - 1;
			this.fillingLevel = valueOverTime.ApplyValue(this.fillingLevel);

			if (valueOverTime.timeLeft >= 0) {
				updatedBonusMalus.Add(valueOverTime);
			}
		}

		// Update the bonusMalus list with the filtered one
		this.bonusAndMalus = updatedBonusMalus;
	}

	/*
	=====================
	SetVisible
	=====================
	Hide or render the indicator, by updating its children renderers
	*/
	public void SetVisible(bool isVisible) {
		int alpha;
		if (isVisible) {
			alpha = 1;
		} else {
			alpha = 0;
		}
		this.gameObject.GetComponent<CanvasGroup>().alpha = alpha;
	}

	/*
	=====================
	UpdateIndicator
	=====================
	Update all indicator bars:
	1. Apply bonus/malus for each indicator bar
	2. Update each indicator bar based on its own formula
	*/
	public static void UpdateIndicator(ResourceManager resourceManager, Virus virus) {
        
		// --- Apply bonus malus for each indicators ---
		foreach (var IndicatorBar in resourceManager.indicators) {
			IndicatorBar.ApplyBonusMalus();
        }

		/* No hidden computation anymore. 
		// Values needed to compute the future values of indicators
		int currentInfected = resourceManager.infectedNumber.fillingLevel;
		int currentHealth = resourceManager.health.fillingLevel;
		int currentEco = resourceManager.economy.fillingLevel;
		int propagation_speed = resourceManager.propagationSpeed.fillingLevel;
		int population = resourceManager.population.fillingLevel;
		int cured = resourceManager.cured.fillingLevel;

		int maxHealth = resourceManager.health.MAX_VALUE;

		// New death = nb_infected * ( (100 - health) / 100 ) * ( 1 / healingTime)
		// Second term represents the healing (mis)ability, last term the average healing/death time
		int newDeath = (int)(currentInfected * (maxHealth - currentHealth) / 100f / virus.healingTime);
		Debug.Log(currentInfected);
		Debug.Log((maxHealth - currentHealth) / 100f / virus.healingTime);
		Debug.Log($"{newDeath} people died this turn !");

		int newCured = (int)(currentInfected * (currentHealth) / 100f / virus.healingTime);
		Debug.Log($"{newCured} people were healed this turn !");
		
		// New infected = old_infected + old_infected * propagation_speed * (pop - cured) * (contagion_prob / 100) - newCured
		// Second term represents the fact that infected people are contaminating new people, and that once cured,
		// you cannot be infected again
		int newInfected = (int)(currentInfected + currentInfected * propagation_speed / 100 * (population - cured) * virus.contagionProb / 100f - newCured);
		
		// ugly upper bound so that it does not grow too fast
		newInfected = Math.Min(20, newInfected);
		Debug.Log($"{newInfected} people were infected this turn !");

		// New eco = old_eco * (pop - death - new_infected) / pop
		// Should reflect that part of the population is dead/sick and thus cannot contribute to the economy
		int newEco = (int)(currentEco * (population - newDeath) / population);
		Debug.Log($"New economy is {newEco}, it was {currentEco} before.");

		// Update indicators
		resourceManager.death.fillingLevel += newDeath;
		resourceManager.population.fillingLevel -= newDeath;
		resourceManager.cured.fillingLevel += newCured;
		resourceManager.infectedNumber.fillingLevel = currentInfected + newInfected - newCured - newDeath;
		resourceManager.economy.fillingLevel = newEco; // overwriten on purpose

		resourceManager.newDeath = newDeath;
		resourceManager.newCured = newCured;
		resourceManager.newEco = newEco;
		resourceManager.newInfected = newInfected;

		Debug.Log($"{resourceManager.infectedNumber.fillingLevel} infected people, from {currentInfected}");
		*/
	}

}
