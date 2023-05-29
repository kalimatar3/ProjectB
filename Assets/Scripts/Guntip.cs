using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guntip : MyMonoBehaviour
{
    protected Vector2 huongsung;
    protected Vector2 MousePos;
    protected void FixedUpdate()
    {
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        huongsung = new Vector2(MousePos.x - transform.position.x, MousePos.y - transform.position.y);
        transform.up = huongsung;
    }
}
