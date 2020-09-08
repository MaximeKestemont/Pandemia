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
    public RawImage image;

    public GameObject choices;

    // Start is called before the first frame update
    void Start()
    {
        textDialogue.text = this.dialogue;
        textCharacterName.text = this.character.characterName.ToString();
        image.texture = this.character.image;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
