using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerMoving : MyMonoBehaviour
{
    // singleton 
    protected static PlayerMoving instance;
    public static PlayerMoving Instance => instance;

    [Header("Controller variable")]
    [HideInInspector] public float move;
    [HideInInspector] public float AirSpeed,MaxJumpHeight,LandSpeed;
    protected float Speed,JumpHeight;
    protected Rigidbody2D MyBody;
    protected Animator MyAnim;

    [Header("Dash variable")] 
    [SerializeField] protected float DashingCooldown;
    [SerializeField] protected float DashingTime;
    [SerializeField] protected float DashingPower;
    [SerializeField] protected float NextDash;
    protected bool Dashable,IsDashing;

    [Header("Status variable")]
    protected float StunTime;
    [HideInInspector] public bool Grounded,Walled,facing;
    protected override void Awake()
    {
        base.Awake();
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadData();
    }
    protected virtual void LoadData()
    {
        MyAnim = GetComponentInParent<Animator>();
        MyBody = GetComponentInParent<Rigidbody2D>();
        this.MaxJumpHeight = 27;
        this.LandSpeed = 20;
        this.AirSpeed = 10;

        this.DashingCooldown = 0.5f;
        this.DashingTime = 0.2f;
        this.DashingPower = 30;
    }
    protected override void Start()
    {
        base.Start();
        StunTime = 0;
        transform.parent.position = GameMaster.Instance.LastCheckPoint;
        Dashable = false;
        facing = true;
    }
    protected void FixedUpdate()
    {  
        this.AnimationVariable();
        StartCoroutine(Stunning());
        if (gameObject.transform.parent.CompareTag("Stunning"))
        {
            this.Stun();
        }
        if(gameObject.transform.parent.CompareTag("Player"))
        {
            JumpHeight = MaxJumpHeight;
            this.Move();
            this.Jump();
            this.Dash();
        }
    }
    protected void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && Grounded == true)
        {
            StartCoroutine(Jumping());
        } 
    }
    protected void Move()
    {
        if(Grounded)    Speed = LandSpeed;
        else            Speed = AirSpeed;
        MyBody.velocity = new Vector2(move * Speed, MyBody.velocity.y);
    }
    protected void Stun()
    {
        JumpHeight = 0;
        move = 0;
    }
    protected void Dash()
    {
        if (IsDashing)
        {
            MyBody.velocity = new Vector2(transform.parent.localScale.x * DashingPower, 0f);
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Time.time > NextDash)
        {
            NextDash = Time.time + DashingCooldown + DashingTime;
            StartCoroutine(Dashing());
        }
    }
    public void DoStun(float time)
    {
        StunTime = time;
    }
    protected IEnumerator Stunning()
    {
        if(StunTime != 0)
        {
            gameObject.transform.parent.tag = "Stunning";
            yield return new WaitForSeconds(StunTime);
            gameObject.transform.parent.tag = "Player";
            StunTime = 0 ;
        }
    }
    IEnumerator Jumping()
    {
        yield return new WaitForSeconds(0.1f);
        MyBody.velocity = new Vector2(MyBody.velocity.x, JumpHeight);
    }
    IEnumerator Dashing()
    {
        if (Dashable)
        {
            yield return new WaitForSeconds(DashingCooldown);
            Dashable = false;
        }
        else
        {
            Dashable = true;
            MyBody.gravityScale = 0;
            IsDashing = true;
            yield return new WaitForSeconds(DashingTime);
            MyBody.gravityScale = 5f;
            IsDashing = false;
            yield return new WaitForSeconds(DashingCooldown);
            Dashable = false;
        }
    }
    protected void AnimationVariable()
    {
        move = Input.GetAxis("Horizontal");                  
        if (move < 0 && facing) flip();
        if (move > 0 && !facing) flip();

        MyAnim.SetBool("Grounded", Grounded);
        MyAnim.SetBool("Walled", Walled);
        MyAnim.SetFloat("Speed", Mathf.Abs(move));
        MyAnim.SetBool("Dash",IsDashing);
    }
    protected void flip()
    {
        facing = !facing;
        Vector3 PlayerScale = transform.parent.localScale ;
        PlayerScale.x *= -1;
        transform.parent.localScale = PlayerScale;
    }
}
