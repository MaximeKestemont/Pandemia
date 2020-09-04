using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public CharacterName characterName;
    public Image image;

    void Start()
    {   
    }

    public enum CharacterName {
        GRUMP, // trump
        MINISTER_OF_HEALTH,
        MINISTER_OF_FOREIGN_AFFAIRS,
        MINISTER_OF_INTERNAL_AFFAIRS,
        RESEARCHER
    }

}