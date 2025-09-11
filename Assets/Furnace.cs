using UnityEngine;

public class Furnace : MonoBehaviour
{
    [SerializeField]
    private TempatureMeter tempMeter;

    public void AddCoal()
    {
        tempMeter.AddHeat(10);
    }

    public float GetHeat()
    {
        return tempMeter.Heat;
    }

}
