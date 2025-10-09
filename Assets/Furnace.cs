using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Furnace : MonoBehaviour
{
    [SerializeField] private List<TempatureMeter> tempMeter;

    [Button]
    public void AddCoal()
    {
        foreach (var tempMeter in tempMeter)
        tempMeter.AddHeat(10);
    }

    public float GetHeat()
    {
        foreach (var tempMeter in tempMeter)
        {
            if (tempMeter != null)
                return tempMeter.Heat;
        }
        
        return 0f;
    }
}
