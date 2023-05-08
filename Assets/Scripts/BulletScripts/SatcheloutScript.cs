using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatcheloutScript : MonoBehaviour
{
    public GameObject ExplodePrefab;
    public float fieldofImpact;
    [SerializeField] protected float force;
    public LayerMask LayertoHit;
    protected LayerMask StepLayer;
    protected Vector2 Distance;
    protected Rigidbody2D thisBody;
    protected void Start()
    {
      thisBody = GetComponent<Rigidbody2D>();
        StepLayer = LayerMask.GetMask("Step");
    }
    protected void FixedUpdate()
    {
        if (InputManager.Instance.RightMouse)
        {        
             this.Explode();
             Destroy(gameObject);
        }
    }
    protected void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofImpact, LayertoHit);
        if(objects != null)   Instantiate(ExplodePrefab,transform.position,transform.rotation);
        foreach (Collider2D obj in objects)
        {   
            Distance = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Vector3.Normalize(Distance).x * force,Vector3.Normalize(Distance).y * (force+100f))); 
        }
    }
    protected void DestroyStep()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofImpact/10, StepLayer);
        foreach (Collider2D obj in objects)
        {  
            Destroy(obj.transform.parent.gameObject);
        }
    }
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldofImpact);
    }
   protected void OnCollisionEnter2D(Collision2D other)
   {
    if(other.gameObject.CompareTag("Step"))
    {
//        thisBody.velocity = new Vector3(0,0,0);
    }
   }
}
