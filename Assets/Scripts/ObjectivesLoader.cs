﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesLoader : MonoBehaviour
{
    public ResourceManager resourceManager;
    public GameObject objectivesParent;
    public GameObject charactersParent;

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

        // Fill characters gallery
        LoadCharacters();
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

    void LoadCharacters() {
        // Update size of scroll area
        int characterCount = ResourceManager.resourceManager.charactersMap.Count;
        float cellSizeY = charactersParent.GetComponent<GridLayoutGroup>().cellSize.y;
        float marginSizeY = charactersParent.GetComponent<GridLayoutGroup>().spacing.y;
        RectTransform rt = charactersParent.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, ((characterCount + 1 ) / 2) * (cellSizeY + marginSizeY));

        foreach (KeyValuePair<Character.CharacterName, Character> kv in ResourceManager.resourceManager.charactersMap) {
            GameObject portraitObject = Instantiate(resourceManager.characterPortraitPrefab, charactersParent.transform);
            // TODO below line is ugly, rely on hardcoded name -> should create a script to hold those reference
            portraitObject.transform.Find("CharacterImage").GetComponent<RawImage>().texture = kv.Value.image;
            portraitObject.transform.Find("TextBackground").GetComponentInChildren<Text>().text = kv.Value.characterName.ToString();
        }
    }
}
