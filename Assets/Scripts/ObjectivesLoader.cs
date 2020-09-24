using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesLoader : MonoBehaviour
{
    public ResourceManager resourceManager;
    public Transform objectivesParent;
    
    void Start()
    {
        CreateObjective(1, false, "test1");
        CreateObjective(2, false, "test2");
        CreateObjective(3, false, "test3");
        CreateObjective(4, false, "test4");
        CreateObjective(5, false, "test5");

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
