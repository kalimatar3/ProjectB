using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool GrapplingGun_Unlock;
    public bool SatchelOut_Unlock;
    private static GameMaster instance;
    public static GameMaster Instance { get => instance ;} 
    public Vector3 LastCheckPoint;
    public float DefaultCamSize, BossFightCamSize;
    private void Awake()
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
    void Reset()
    {
        LastCheckPoint = FindObjectOfType<PlayerController>().transform.position;
    }
}
