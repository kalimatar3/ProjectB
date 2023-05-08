using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood: MonoBehaviour
{
    private static Flood instance;
    public static Flood Instance {get => instance;}
    public bool flooding;
    public float speed;
    private void Awake()
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
    private void Start()
    {
        speed = 5f ;
    }
    private void FixedUpdate()
    {
        this.FloodRunning();
    }



    private void FloodRunning(){

        if (flooding)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position + new Vector3(-100,0,0), new Vector3(200,0,0));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     if(collision.tag == "Player" || collision.CompareTag("Stunning"))
     {
        PlayerDeadScript.Instance.DF = true;
     }
    }
}
