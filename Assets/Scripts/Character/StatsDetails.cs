using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDetails : MonoBehaviour
{
    
}

[System.Serializable]
public class Stat
{
    [SerializeField] protected int baseValue;

    private List<int> effectModifiers = new List<int>();
    public int GetModifiedValue()
    {
        int finalValue = baseValue;
        effectModifiers.ForEach(x => finalValue += x); //Add each modified value x to final value and return that value
        return finalValue;
    }

    public void AddEffectModifier(int modifiedvalue)
    {
        if (modifiedvalue != 0)
        {
            effectModifiers.Add(modifiedvalue);
            Debug.Log("Modified value added " + modifiedvalue);
        }
    }

    public void RemoveEffectModifier(int modifiedvalue)
    {
        if (modifiedvalue != 0)
        {
            effectModifiers.Remove(modifiedvalue);
            Debug.Log("Modified value removed " + modifiedvalue);
        }
    }
}