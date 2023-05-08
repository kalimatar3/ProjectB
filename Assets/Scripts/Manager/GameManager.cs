using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MyMonoBehaviour
{
public static GameManager instance;
private static GameManager Instance  => instance;
    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    protected void FixedUpdate()
    {
        
    }
}
