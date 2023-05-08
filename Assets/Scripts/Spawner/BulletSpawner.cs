using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private static BulletSpawner instance;
    public static BulletSpawner Instance => instance;
    public static string SatchelOut = "SatchelOut";
    protected override void Awake()
    {
        base.Awake();
        if(BulletSpawner.instance != null)
        {
            Debug.LogError(" Onlly 1 BulletSpawner allow to exits");
            Destroy(this);
        }
        else BulletSpawner.instance = this;
    }
}
