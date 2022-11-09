using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModelColor : MonoBehaviour
{
    public Model_Color_Data model_Color_Data;
    [SerializeField]
    private GameObject[] Objs;
    
    private Material[] modelMaterials;
    private ELEMENT element;

    public void SetColorData(ELEMENT e)
    {
        element = e;
    }

    public void Init()
    {
        modelMaterials = new Material[Objs.Length];
        for (int i = 0; i < Objs.Length; i++)
        {
            modelMaterials[i] = Objs[i].GetComponent<SkinnedMeshRenderer>().material;
        }
        DisolveModel(0, true);
    }

    /// <summary>
    /// Get the Objs materials, instantiate it and change the Color. Should not use this feature inside Update.
    /// </summary>
    public void ChangeColor()
    {
        // Debug.Log("[ChangeModelColor] ChangeColor: " + model_Color_Data.modelName + " | " + element.ToString());
        
        if (model_Color_Data != null)
        {
            for(int i = 0; i < model_Color_Data.Color.Length; i++)
            {
                if(model_Color_Data.Color[i].element == element)
                {                        
                    for(int n = 0; n < modelMaterials.Length; n++)
                    {
                        modelMaterials[n].SetColor("_ColorR", model_Color_Data.Color[i].redMask);
                        modelMaterials[n].SetColor("_ColorG", model_Color_Data.Color[i].greenMask);
                        modelMaterials[n].SetColor("_ColorB", model_Color_Data.Color[i].blueMask);
                    }
                    return;
                }
            }
        }
    }

    Coroutine disolveThread = null;
    /// <summary>
    /// Disolve the Model, only use with Tint Shader material
    /// </summary>
    /// <param name="_timeToDisolve">
    /// Time for disolve effect finish.
    /// = 0: Disolve immediately.
    /// </param>
    /// <param name="_reverse">
    /// True: From nothing to full Model (Appear the model).
    /// </param>
    public void DisolveModel(float _timeToDisolve, bool _reverse = false)
    {
        if(disolveThread != null)
        {
            StopCoroutine(disolveThread);
        }

        if(_timeToDisolve == 0)
        {
            for (int n = 0; n < modelMaterials.Length; n++)
            {
                modelMaterials[n].SetFloat("_Amount", (_reverse) ? 0  : 1);
            }
        }
        else
        {
            disolveThread = StartCoroutine(DisolveProgress(_timeToDisolve, _reverse));
        }        
    }

    IEnumerator DisolveProgress(float _timeToDisolve, bool _reverse)
    {
        if(_timeToDisolve < 0)
        {
            yield break;
        }

        float currentTime = 0;
        while(currentTime < _timeToDisolve)
        {
            float amount = Mathf.Clamp01((currentTime / _timeToDisolve));
            for (int n = 0; n < modelMaterials.Length; n++)
            {
                modelMaterials[n].SetFloat("_Amount", (_reverse) ? (1 - amount): amount);
            }
            currentTime += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }        
    }

    public void Disolve(float _timeToDisolve)
    {
        DisolveModel(_timeToDisolve);
    }

    public void Appear(float _timeToAppear)
    {
        DisolveModel(_timeToAppear, true);
    }
}
