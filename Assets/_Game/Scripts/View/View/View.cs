using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    
    public abstract void StartState();
    public abstract void UpdateState();
    public abstract void EndState();

}
