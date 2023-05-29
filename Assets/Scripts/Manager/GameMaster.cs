using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MyMonoBehaviour
{
    public bool GrapplingGun_Unlock;
    public bool SatchelOut_Unlock;
    private static GameMaster instance;
    public static GameMaster Instance { get => instance ;} 
    public Vector3 LastCheckPoint;
    public float DefaultCamSize, BossFightCamSize;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadData();
    }
    protected virtual void LoadData()
    {
        this.DefaultCamSize = 15;
        this.BossFightCamSize = 20;
        LastCheckPoint = FindObjectOfType<PlayerMoving>().transform.position;
    }
    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
}
