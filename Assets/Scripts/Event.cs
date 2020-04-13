using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event : MonoBehaviour
{

    public List<Choice> choices {get;set;}
    public string title;
    public string description;
    public Text textTitle;
    public Image eventImage;

    // Start is called before the first frame update
    void Start()
    {
        textTitle.GetComponent<Text>().text = title;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
