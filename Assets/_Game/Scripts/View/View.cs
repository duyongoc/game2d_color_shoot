using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    
    // called when view start 
    public abstract void StartState();

    // called when view update 
    public abstract void UpdateState();

    // called when view end 
    public abstract void EndState();

}
