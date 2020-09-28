using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesLoader : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GameObject objectivesParent;

    private int objectiveCount = 0;
    
    void Start()
    {
        CreateObjective(1, false, "Plus on est de fous, plus on rit");
        CreateObjective(2, false, "Devenez l'heureux possesseur d'une ferrari");
        CreateObjective(3, false, "Alliez-vous à une puissance étrangère");
        CreateObjective(4, false, "Construisez un mur");
        CreateObjective(5, false, "Instaurez une dictature");
        CreateObjective(6, false, "Créer votre propre compte twitter");

        // Update size of scroll area
        float cellSizeY = objectivesParent.GetComponent<GridLayoutGroup>().cellSize.y;
        RectTransform rt = objectivesParent.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, objectiveCount * cellSizeY);

    }

    void CreateObjective(int uid, bool flag, string message) {
        GameObject objectiveObject = Instantiate(resourceManager.objectivePrefab, objectivesParent.transform);
        Objective newObjective = objectiveObject.GetComponent<Objective>();
        newObjective.uid = uid;
        newObjective.isDone = flag;
        newObjective.objectiveMessage = message;

        objectiveCount++;

        resourceManager.objectivesMap.Add(uid, newObjective);
    }
}
