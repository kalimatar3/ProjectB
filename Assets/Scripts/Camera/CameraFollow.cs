using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MyMonoBehaviour
{
    protected Camera MainCam;
    protected bool DefaultFollow;
    [SerializeField] protected float smothing;
    [SerializeField] Vector3 cameraX;
    [SerializeField] protected float MaxDisCam;
    protected override void Start()
    {
        DefaultFollow = false;
        MainCam = GetComponent<Camera>();
        this.MainCam.orthographicSize = GameMaster.Instance.DefaultCamSize;
    }
   void FixedUpdate()
    { 
        this.DefaultCamPotation(smothing);
        this.CamerafollowtheFlood();
        this.CamerafollowGroundedPlayer();
        this.CamerafollowPLayer();
    }
    private void CamerafollowPLayer()
    {
        cameraX = new Vector3(PlayerMoving.Instance.transform.position.x, transform.position.y, -1);
        transform.position = cameraX;
        if (PlayerMoving.Instance.transform.position.y - transform.position.y < -10f)
        {
            DefaultFollow = true;
        }
        if (PlayerMoving.Instance.transform.position.y - transform.position.y  > 10f)
        {
            DefaultFollow = true;
        }
    }
    private void CamerafollowtheFlood()
    {
         if (Flood.Instance != null)
        {
            if (transform.position.y -  Flood.Instance.transform.position.y < 5)
            {
                transform.position = Flood.Instance.transform.position + new Vector3(0, 5, 0);
                cameraX = new Vector3(PlayerMoving.Instance.transform.position.x, transform.localPosition.y, -1);
            }
        }
    }
    private void CamerafollowGroundedPlayer ()
    {
        if(PlayerMoving.Instance.transform.position.y - transform.position.y > 0 )
        {
            if(PlayerMoving.Instance.Grounded == true)
            {
                DefaultFollow = true;
            }
        }
    }
    private void DefaultCamPotation(float smothing)
    {   
        if(transform.position.y > PlayerMoving.Instance.transform.position.y + MaxDisCam -0.5f && transform.position.y <= PlayerMoving.Instance.transform.position.y + MaxDisCam + 0.5f)
        {
            DefaultFollow = false;
        }
        if(DefaultFollow)
        {
        transform.position = Vector3.Lerp(transform.position, PlayerMoving.Instance.transform.position + new Vector3 (0,MaxDisCam,0) , smothing * Time.deltaTime);
        }
    }
    }
