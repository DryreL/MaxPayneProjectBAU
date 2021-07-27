using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Returns a random float value within the provided range. 
/// </summary>
[Serializable]
public class RandomVarianceFloat
{    
    public float from;
    public float to;

    public RandomVarianceFloat(float from, float to)
    {
        this.from = from; 
        this.to = to;
    }

    public float NextValue()
    {
        if (from == to)
            return from; 

        return UnityEngine.Random.Range(from, to); 
    }

    public float NextValue(int digits)
    {        
        return (float)Math.Round(this.NextValue(), digits); 
    }
}