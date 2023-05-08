using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameReciver : MyMonoBehaviour
{
    protected float MaxHp;
    protected float CurrentHp;
    protected void IsReducedHp(float Dame)
    {
        if(CurrentHp < 0 ) CurrentHp = 0;
        CurrentHp -= Dame;
    }
    protected void IsIncreacsedHp(float Dame)
    {
        if(CurrentHp > MaxHp ) CurrentHp = MaxHp;
        CurrentHp += Dame;
    }
}
