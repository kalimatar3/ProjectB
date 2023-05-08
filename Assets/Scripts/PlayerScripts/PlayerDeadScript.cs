using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadScript : MonoBehaviour
{
    private static PlayerDeadScript instance;

    public static PlayerDeadScript Instance {get => instance;}
    public bool DF;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else instance = this;
    }
    private void Start()
    {
        DF = false;
    }
    private void FixedUpdate()
    {
        this.DeadbyFlood();
    }


    
    private void DeadbyFlood()
    {
     if (DF == true)
     {
     Loader.Load(Loader.Scene.DeadScene);
     }
    }
}