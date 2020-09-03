using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public CharacterName name;
    public Image image;

    void Start()
    {   
    }

    public enum CharacterName {
        GRUMP, // trump
        MINISTER_OF_HEALTH,
        RESEARCHER
    }

}