using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ELEMENT
{
    IGNIS = 0,
    PLANT = 1,
    ANIMA = 2,
    EARTH = 3,
    ELEKI = 4,
    AQUA = 5,
}

[Serializable]
public class Element_Color
{
    public ELEMENT element;
    public Color32 redMask;
    public Color32 greenMask;
    public Color32 blueMask;
}

[CreateAssetMenu(fileName = "New Model Color Data", menuName = "BountyKind/Model Color", order = 1)]
public class Model_Color_Data : ScriptableObject
{
    public string modelName;
    
    public Element_Color[] Color;
}
