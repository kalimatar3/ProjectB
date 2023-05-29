using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private static BulletSpawner instance;
    public static BulletSpawner Instance => instance;
    public List<string> BulletName;
    public static string SatchelOut = "SatchelOut";
    public static string Arrow = "Arrow";
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletName();
    }
    protected void LoadBulletName()
    {
        for(int  i = 0 ; i < prefabs.Count; i++)
        {
            if(BulletName.Count < prefabs.Count) BulletName.Add("");
            BulletName[i] = prefabs[i].name;
        }
    }
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
