using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightCam : MonoBehaviour
{
    [SerializeField] protected Camera MainCam;
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") ||collision.gameObject.CompareTag("Stunning"))
        {
            MainCam.gameObject.transform.position = this.gameObject.transform.position + new Vector3(0,0,-1);
            MainCam.orthographicSize = GameMaster.Instance.BossFightCamSize;
        }
    }
    protected void OnTriggerExit2D( Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") ||collision.gameObject.CompareTag("Stunning"))
        {
            MainCam.orthographicSize = GameMaster.Instance.DefaultCamSize;
        }
    }
}
