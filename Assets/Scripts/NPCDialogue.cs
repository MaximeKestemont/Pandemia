using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{

    public Character character;
    public string dialogue;
    public Text textDialogue;
    public Text textCharacterName;

    public GameObject choices;

    // Start is called before the first frame update
    void Start()
    {
        textDialogue.GetComponent<Text>().text = this.dialogue;
        textCharacterName.GetComponent<Text>().text = this.character.name.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
