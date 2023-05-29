using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Stunning") )
        {
            PlayerMoving.Instance.Grounded = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Stunning") )
        {
            PlayerMoving.Instance.Grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
  if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Stunning") )
        {
          PlayerMoving.Instance.Grounded = false;
        }
    }
}
