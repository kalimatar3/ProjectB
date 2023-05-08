using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingBullet : MyMonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.tag == "Step")
        {
            gameObject.tag = "Hitted";
        }
    }
        private void OnTriggerStay2D(Collider2D collision)
    {
      if (collision.gameObject.tag == "Step")
        {
            gameObject.tag = "Hitted";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "Step")
        {
            gameObject.tag = "Untagged";
        } 
    }
}
