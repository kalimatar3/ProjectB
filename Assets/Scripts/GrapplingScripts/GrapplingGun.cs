using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MyMonoBehaviour
{
    protected SpringJoint2D m_springJoint2D;
    protected Rigidbody2D MyBody;
    protected Vector2 BulletPos,tachtrongluc;
    //DrawRope
    [SerializeField] protected AnimationCurve AnimationRopeWave;
    [SerializeField] protected AnimationCurve AnimationRopestraight;
    protected LineRenderer GrapplingGun_linerenderer;
    //Bullet
    protected GameObject Bullet;
    protected GrapplingBullet grapplingBullet;
    [SerializeField] protected GameObject GrapplingBullet;
    protected Vector3 TarGetPos;
    [SerializeField] protected LayerMask StepLayer;
    [SerializeField] protected RaycastHit2D IsHitted;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float  FireRate;
    protected float NextFire,distance,Mindistance;
    private int fire_gate,rope_gate;
    [SerializeField] protected Transform huongsung;
    protected Vector3 HuongsungPos;
    [SerializeField] protected float RopeLength;
    protected float moveTime;
    protected override void Start()
    {
        Mindistance = 3f;
        NextFire = 0;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        GrapplingGun_linerenderer = GetComponent<LineRenderer>();
        MyBody = GetComponentInParent<Rigidbody2D>();
        m_springJoint2D = GetComponentInParent<SpringJoint2D>();
    }
    protected void FixedUpdate()
    {    
        moveTime += Time.deltaTime;
        if (InputManager.Instance.LeftMouse && Time.time >= NextFire)
        {             
            NextFire = Time.time + FireRate;   
            if (fire_gate == 1)
            {
                this.TargetStep();
                this.FireGappling();
            }
        }
        if (!InputManager.Instance.LeftMouse)
        {
            this.DespawgrapplingByRelease();
        }
            this.DespawGrapplingByDistance();
            this.DrawGrapplingRope();
}
    protected void DrawGrapplingRope()
    {
    tachtrongluc = tachluc(new Vector2(0, -50f), BulletPos);
    if (Bullet != null)
        {
            DrawRopeWave();
            BulletPos = Bullet.transform.position - transform.parent.position;
            Bullet.transform.position = Vector3.Lerp(Bullet.transform.position,Bullet.transform.position + HuongsungPos, Time.deltaTime * bulletSpeed); 
            if (IsHitted.transform != null)
            {     
                TarGetPos = IsHitted.transform.position + new Vector3(0,-0.5f,0);
                if (Bullet.gameObject.tag == "Hitted")
                {
                    Bullet.transform.position = TarGetPos;
                    if (rope_gate == 1)
                        {
                            rope_gate = 0;
                            if(BulletPos.magnitude < Mindistance) distance = Mindistance;
                            else distance = BulletPos.magnitude + 1f;
                        }
                        DrawRopeNoWave();
                    if (BulletPos.y  > 0 && BulletPos.magnitude >= distance)
                    {
                        m_springJoint2D.enabled = true;
                        m_springJoint2D.connectedAnchor = TarGetPos;
                        MyBody.AddForce(new Vector2(tachtrongluc.x * 40f,0));
                    }
                    else
                    {                      
                        m_springJoint2D.enabled = false;
                    }
                        m_springJoint2D.distance = distance;
                }
            }
        }
        else
        {
            m_springJoint2D.enabled = false;
        }
        if (m_springJoint2D.enabled == true)
        {     
            if(PlayerMoving.Instance.move * tachtrongluc.x < 0 )  
            MyBody.AddForce(new Vector2(-tachtrongluc.x * 50f,0));
        }

    }
    protected void DespawgrapplingByRelease()
    {
        moveTime = 0;
        GrapplingGun_linerenderer.enabled = false;
        GrapplingGun_linerenderer.positionCount = 0;
        m_springJoint2D.enabled = false;
        Destroy(Bullet);
        fire_gate = 1;
        rope_gate = 1;
    }
    protected void DespawGrapplingByDistance()
    {
    if (Bullet !=null && (BulletPos).magnitude > RopeLength)
        {
        GrapplingGun_linerenderer.enabled = false;
        Destroy(Bullet);
        rope_gate = 1;
        }
    }
    protected void FireGappling()
    {
        StartCoroutine(freeze());
        GrapplingGun_linerenderer.enabled = true;
        HuongsungPos = (huongsung.position -transform.parent.position).normalized * 5f;
        Bullet = Instantiate(GrapplingBullet,transform.parent.position,huongsung.rotation);
        fire_gate = 0;
    }
    protected void TargetStep()
    {
    IsHitted = Physics2D.Raycast(transform.position, huongsung.position -transform.parent.position, RopeLength, StepLayer);
    }
    protected void RunTime(float time)
    {
        time += Time.deltaTime;
    }
    private void DrawRopeNoWave()
    {
        GrapplingGun_linerenderer = GetComponent<LineRenderer>();
        GrapplingGun_linerenderer.positionCount = 2;
        GrapplingGun_linerenderer.SetPosition(0,transform.position);
        GrapplingGun_linerenderer.SetPosition(1,Bullet.transform.position);
    }
    private void DrawRopeWave()
    {
        GrapplingGun_linerenderer = GetComponent<LineRenderer>();
        GrapplingGun_linerenderer.positionCount = 100;
        if (Bullet != null)
        {
            for (int i = 0; i < 100; i++)
            {
                float delta = (float)i / 99;
                Vector2 offset = Vector2.Perpendicular(HuongsungPos).normalized * AnimationRopeWave.Evaluate(delta) * 10;
                Vector2 h = Vector2.Lerp(huongsung.transform.position, Bullet.transform.position, delta) + offset;
                Vector2 k = Vector2.Lerp(huongsung.transform.position, h, AnimationRopestraight.Evaluate(moveTime) *bulletSpeed);
                GrapplingGun_linerenderer.SetPosition(i, k);
            }
        }
    }
    IEnumerator freeze()
    {
        MyBody.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.5f);
    }
    Vector2  tachluc(Vector2 bitach, Vector2 huong1)
    {
        Vector2 h = new Vector2(Mathf.Sqrt(Vector2.SqrMagnitude(bitach)) * huong1.x / Mathf.Sqrt(Vector2.SqrMagnitude(huong1)), Mathf.Sqrt(Vector2.SqrMagnitude(bitach)) * huong1.y / Mathf.Sqrt(Vector2.SqrMagnitude(huong1)));
        Vector2 p;
        float xtt, ytt;
        float Alpha = (Vector2.Angle(-huong1,bitach)* Mathf.PI)/180;
        float Beta = (Vector2.Angle(huong1, new Vector2(0, huong1.y)) *Mathf.PI)/180;
        if (Alpha>Beta - 0.1f&& Alpha<Beta+0.1f)
        {
            xtt = Mathf.Sqrt(Mathf.Abs((Vector2.SqrMagnitude(bitach)))) * Mathf.Sin(Alpha) * Mathf.Sin(Mathf.PI / 2 - Beta);
            ytt = -1*Mathf.Sqrt(Mathf.Abs((Vector2.SqrMagnitude(bitach)))) * Mathf.Sin(Alpha) * Mathf.Cos(Mathf.PI/ 2 - Beta);
        }
        else
       {
            xtt = -1*Mathf.Sqrt(Mathf.Abs((Vector2.SqrMagnitude(bitach)))) * Mathf.Sin(Alpha) * Mathf.Sin(Mathf.PI/ 2 - Beta);
            ytt = Mathf.Sqrt(Mathf.Abs((Vector2.SqrMagnitude(bitach)))) * Mathf.Sin(Alpha) * Mathf.Cos(Mathf.PI / 2 - Beta);
        }
        if (BulletPos.y >= 0)
        {
            if (h.x - bitach.x >= 0)
            {
                p = new Vector2(xtt, ytt);
            }
            else p = new Vector2(-xtt,ytt);
        }
        else
        {
            if (h.x - bitach.x >= 0)
            {
                p = new Vector2(-xtt, -ytt);
            }
            else p = new Vector2(xtt, -ytt);
        }
       float   alpha =Alpha;
       float  beta = Beta;
        return p;
    }

}