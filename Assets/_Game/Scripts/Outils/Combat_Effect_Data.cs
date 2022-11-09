using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Effect_Data : MonoBehaviour
{
    [Header ("Effect Description")]
    public string EffectName;

    [Header ("Effect Data")]
    [SerializeField]
    private GameObject EffectRoot;
    [SerializeField]
    private ParticleSystem[] particles;
    [SerializeField]
    private Effect_Color_Data effectColor;
    [SerializeField]
    [Tooltip ("Particle Id to change Color")]
    private int[] particleToChangeColor;

    public void SetParticleElement(ELEMENT element)
    {
        for(int i = 0; i < effectColor.color.Length; i++)
        {
            if(effectColor.color[i].element == element)
            {
                Color color = effectColor.color[i].particlesColors;
                Gradient grad = effectColor.color[i].colorsOverLifetime;

                for (int n = 0; n < particleToChangeColor.Length; n++)
                {
                    var main = particles[particleToChangeColor[n]].main;
                    main.startColor = color;
                    var col = particles[particleToChangeColor[n]].colorOverLifetime;
                    col.color = grad;
                }
            }
        }        
    }

    public void Play()
    {
        particles[0].Play();
    }

    public void Pause()
    {
        particles[0].Pause();
    }

    public void Stop()
    {
        particles[0].Clear();
        particles[0].Stop();
    }

    public ParticleSystem GetParticleRoot()
    {
        return particles[0];
    }
}
