using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFlood : MonoBehaviour
{
    public int flood_gate;
    BoxCollider2D box;
    [SerializeField] protected Transform start,end;
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        flood_gate = 1;
    }
    private void Update()
    {
        this.FloodComming();
    }
    private void FloodComming(){      
            if (Flood.Instance.transform.position.y >= end.position.y - 0.1f && Flood.Instance.transform.position.y >= end.position.y + 0.1f)
            {
                Flood.Instance.flooding = false;
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (flood_gate == 1)
        {
            if (collision.tag == "Player")
            {
                flood_gate = 0;
                if (start.position.y - Flood.Instance.transform.position.y > 15)
                {
                    Flood.Instance.transform.position = start.position + new Vector3(0,- 15, 0);
                }
            }
        }
        if (flood_gate == 0)
        {
            Flood.Instance.flooding = true;
        }
    }
}
