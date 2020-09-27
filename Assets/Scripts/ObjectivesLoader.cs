using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesLoader : MonoBehaviour
{
    public ResourceManager resourceManager;
    public Transform objectivesParent;
    
    void Start()
    {
        CreateObjective(1, false, "Plus on est de fous, plus on rit");
        CreateObjective(2, false, "Devenez l'heureux possesseur d'une ferrari");
        CreateObjective(3, false, "Alliez-vous à une puissance étrangère");
        CreateObjective(4, false, "Construisez un mur");
        CreateObjective(5, false, "Instaurez une dictature");
        CreateObjective(6, false, "Créer votre propre compte twitter");

    }

    void CreateObjective(int uid, bool flag, string message) {
        GameObject objectiveObject = Instantiate(resourceManager.objectivePrefab, objectivesParent);
        Objective newObjective = objectiveObject.GetComponent<Objective>();
        newObjective.uid = uid;
        newObjective.isDone = flag;
        newObjective.objectiveMessage = message;

        resourceManager.objectivesMap.Add(uid, newObjective);
    }
}
