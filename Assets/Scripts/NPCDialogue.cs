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
    public Button button;

    public GameObject choices;

    // Start is called before the first frame update
    void Start()
    {
        textDialogue.text = this.dialogue;
        textCharacterName.text = this.character.characterName.ToString();
        image.texture = this.character.image;

        // If no following choice, display the next event button
        if (!choices.GetComponentInChildren<Choice>()) {
            button.onClick.AddListener(() => ResourceManager.resourceManager.gameController.NextEvent());
        } else {
            button.gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDiscoveredCharacter() {
        if (!this.character.alreadySeen) {
            this.character.alreadySeen = true;
            this.character.UpdatePortrait();
        };

    }
}
