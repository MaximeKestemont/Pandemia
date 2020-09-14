using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public CharacterName characterName;
    public Texture2D image;

    void Start()
    {   
    }

    // !!! Add new character at the END of this enum. Else, it modifies all existing nodes.
    public enum CharacterName {
        GRUMP, // trump
        MINISTER_OF_HEALTH,
        MINISTER_OF_FOREIGN_AFFAIRS,
        MINISTER_OF_INTERNAL_AFFAIRS,
        MISTRESS,
        RESEARCHER,
        JOURNALIST,
        PRIME_MINISTER,
        MINISTER_OF_ECONOMY,
        PHARMA_LOBBYIST
    }

}