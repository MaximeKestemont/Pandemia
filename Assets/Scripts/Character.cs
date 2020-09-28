using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public CharacterName characterName;
    public Texture2D image;
    public bool alreadySeen = false;

    // ref to the portrait corresponding to this character in the Gallery pannel - initialized in the AccomplishmentsLoader class
    public GameObject characterPortrait;

    void Start()
    {
    }

    public void UpdatePortrait() {
            // TODO below lines are ugly, rely on hardcoded name -> should create a script to hold those reference
            characterPortrait.transform.Find("CharacterImage").GetComponent<RawImage>().texture = this.image;
            characterPortrait.transform.Find("TextBackground").GetComponentInChildren<Text>().text = this.characterName.ToString();
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
        PHARMA_LOBBYIST,
        CHINESE_PRESIDENT,
        DRUNKEN_MAN,
        POLICEMAN,
        TWITTER_EXPERT,
        PRIEST
    }

}