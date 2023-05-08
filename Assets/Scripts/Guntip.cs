using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guntip : MonoBehaviour
{
    public Vector2 huongsung;
    Vector2 MousePos;
    public GameObject Player;
    void Update()
    {
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        huongsung = new Vector2(MousePos.x - Player.transform.localPosition.x, MousePos.y - Player.transform.localPosition.y);
        transform.up = huongsung;
    }
}
