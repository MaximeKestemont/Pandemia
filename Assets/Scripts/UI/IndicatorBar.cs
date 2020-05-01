using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

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
	private const int MIN_VALUE = 0;
	private const int MAX_VALUE = 100;
	
	void Start () {
		borderRect = border.GetComponent<RectTransform> ();
		text.GetComponent<Text>().text = indicatorName;
		filling.GetComponent<Image>().color = color;
	}


	void Update () {
		// Update the bar rendering
		filling.transform.localScale = new Vector3 (
			fillingLevel / 100f * borderRect.localScale.x, 
			filling.transform.localScale.y, 
			filling.transform.localScale.z
		);
	}

	/*
	=====================
	AddValueOverTime
	=====================
	Add a value to the list of bonus and malus. If the timeLeft <= 0, it means this is a value that will be
	applied only once. If > 0, it will last at least during one event.
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
	UpdateIndicator
	=====================
	Update all indicator bars:
	1. Apply bonus/malus for each indicator bar
	2. Update each indicator bar based on its own formula
	*/
	public static void UpdateIndicator(ResourceManager resourceManager) {
        foreach (var IndicatorBar in resourceManager.indicators) {
			IndicatorBar.ApplyBonusMalus();
        }

		
		
		



		//     For the second part, the formula is the following one: 
    	//INFECTED_NUMBER(t + 1) = INFECTED_NUMBER(t) + (TOTAL_POP - IMMUNE_POP) * PROP_SPEED * CONTAGION - NEW_IMMUNE
	}

	
}
