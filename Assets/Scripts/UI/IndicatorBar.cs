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

	public void UpdateIndicator() {
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

		// Check if the value went outside its range
		if (fillingLevel < MIN_VALUE) {
			Debug.Log ($"Indicator {indicatorName} is too low !");
		}

		if (fillingLevel > MAX_VALUE) {
			Debug.Log ($"Indicator {indicatorName} is too high !");
		}
	}

	
}
