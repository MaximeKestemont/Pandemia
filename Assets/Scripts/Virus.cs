using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Virus : MonoBehaviour
{
    public int contagionProb = 3;   // probability to contaminate someone
    public int healingTime = 20;    // nb of days needed to recover (or die) from the virus
}