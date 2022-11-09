using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Element_Effect_Color
{
    public ELEMENT element;
    [SerializeField]
    [Tooltip("Particle Start color for each element")]
    public Color particlesColors;
    [SerializeField]
    [Tooltip("Particle color over lifetime for each element")]
    public Gradient colorsOverLifetime;
}

[CreateAssetMenu(fileName = "New Effect Color Data", menuName = "BountyKind/Effect Color", order = 2)]
public class Effect_Color_Data : ScriptableObject
{
    public string effectName;

    public Element_Effect_Color[] color;
}
