using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TurnData
{
    [Space(10)]
    public bool randomRotate;
    public int shieldCount;
    public int minValue;
    public int maxValue;

    [Space(10)]
    public float minShieldSpeed;
    public float maxShieldSpeed;
    public float timeChangeSpeed;


}


[CreateAssetMenu(fileName = "Setting", menuName = "Setting/Setting", order = 1)]
public class SOSetting : ScriptableObject
{
    public int arrows = 3;

    [Space(10)]
    public List<TurnData> turnEasy;
    public List<TurnData> turnMedium;
    public List<TurnData> turnHard;
    public List<TurnData> nightmare;
}
