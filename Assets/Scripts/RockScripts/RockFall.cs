 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : MonoBehaviour
{
    [SerializeField] protected GameObject Animation;
    protected Vector3 lucsetno;
    protected LayerMask PlayerLayer ;
    protected LayerMask StepLayer;
    [SerializeField] protected bool Thundered,ThunderUpComing;
    [SerializeField] protected float Force;

    protected void Start()
    {
    ThunderUpComing = true;
    StepLayer = LayerMask.GetMask("Step");
    PlayerLayer = LayerMask.GetMask("Player");
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
                     Collider2D[] StepThunderZone1 = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - 1, transform.position.y +RockManager.Instance.TakeDamezone ), new Vector2(transform.position.x + 1, transform.position.y + RockManager.Instance.TakeDamezone + 3), PlayerLayer);
                     Collider2D[] StepThunderZone = Physics2D.OverlapCircleAll(transform.position, RockManager.Instance.TakeDamezone, PlayerLayer);
                     foreach (Collider2D Player in StepThunderZone)
                     {
                        if(Player != null)
                        {
                        PlayerController.Instance.DoStun(0.5f);
                         lucsetno = (Player.transform.position - transform.position);
                         Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Vector3.Normalize(lucsetno).x * 1.4f, Vector3.Normalize(lucsetno).y + 0.5f)*Force, ForceMode2D.Impulse);
                        }
                    }
                     foreach (Collider2D Player in StepThunderZone1)
                     {
                        if(Player != null)
                        {
                        PlayerController.Instance.DoStun(0.5f);
                        lucsetno = (Player.transform.position - transform.position);
                        Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Vector3.Normalize(lucsetno).x * 1.4f, Vector3.Normalize(lucsetno).y + 0.5f)*Force, ForceMode2D.Impulse);
                        }
                     }
                     Instantiate(Animation,transform.position,transform.rotation);
                     Destroy(gameObject);
    }
}
