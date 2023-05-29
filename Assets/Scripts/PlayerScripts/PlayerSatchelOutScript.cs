using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSatchelOutScript : MyMonoBehaviour
{
    protected Rigidbody2D Mybody;
    protected Transform FiredBullet;
    protected float NextFire, FireRate = 0.5f;
    [SerializeField]  protected bool CanFire;
    [SerializeField] protected Transform GunTip;



    public GameObject ExplodePrefab;
    public float fieldofImpact;
    [SerializeField] protected float force;
    public LayerMask LayertoHit;
    protected Vector2 Distance;

    protected override void Start()
    {
        CanFire = true;
        Mybody = GetComponent<Rigidbody2D>();
    }
    protected void FixedUpdate()
    {
        this.Firing();
    }
    protected void Firing()
    {
        if(InputManager.Instance.RightMouse && Time.time >= NextFire)
        {
        if(CanFire)   this.Fire();   
        else  
            {
            NextFire = Time.time + FireRate;
            this.Explode();
            }
        CanFire = !CanFire;
        }      
    }
    protected void Fire()
    {      
        NextFire = Time.time + FireRate;
        FiredBullet = BulletSpawner.Instance.Spawn(BulletSpawner.SatchelOut,GunTip.position,GunTip.rotation);
        FiredBullet.gameObject.GetComponent<Rigidbody2D>().velocity = GunTip.up * 10f;
    }
    protected void Explode()
    {
        if(FiredBullet  != null)
        {
        Collider2D[] objects2 = Physics2D.OverlapCircleAll(FiredBullet.transform.position, fieldofImpact, LayertoHit);
        if(objects2 != null)   
        {
            Instantiate(ExplodePrefab,FiredBullet.transform.position,FiredBullet.transform.rotation);
        //    PlayerMoving.Instance.DoStun(0.25f);
        }
        foreach (Collider2D obj in objects2)
        {   
            Distance = (obj.transform.position - FiredBullet.transform.position);
            obj.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(Vector3.Normalize(Distance).x * force,Vector3.Normalize(Distance).y * (force+ 10f))); 
        }
        Destroy(FiredBullet.gameObject);
        } 
    }
}
