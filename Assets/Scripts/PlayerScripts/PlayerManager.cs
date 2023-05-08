using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] protected GameObject PlayerGrapplingGun;
    [SerializeField] protected GameObject PlayerSatchelout;
    private static PlayerManager instance;
    public static PlayerManager Instance  => instance;
    protected void Awake()
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
        this.PlayerUnLockGrapllingGun();
        this.PlayerUnLockSatchelout();
    }
    protected void PlayerUnLockGrapllingGun()
    {
        if(GameMaster.Instance.GrapplingGun_Unlock)
        {
        PlayerGrapplingGun.SetActive(true);
        }
    }

    protected void PlayerUnLockSatchelout()
    {
        if(GameMaster.Instance.SatchelOut_Unlock)
        {
        PlayerSatchelout.SetActive(true);
        }
    }
}
