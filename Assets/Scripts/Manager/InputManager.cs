using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MyMonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;
    public bool LeftMouse,RightMouse;
    public bool LeftMouseDown,RightMouseDown;
    protected override void Awake()
    {
        base.Awake();
        if(InputManager.instance != null)
        {
            Debug.LogError(" Onlly 1 InputManager allow to exits");
            Destroy(this);
        }
        else InputManager.instance = this;
    }
    protected void  Update()
    {
        if(Input.GetMouseButton(0))
        {
            LeftMouse = true;
        }
        else LeftMouse = false;
        if(Input.GetMouseButton(1))
        {
            RightMouse= true;
        }
        else RightMouse = false;
        if(Input.GetMouseButtonDown(0))
        {
            LeftMouseDown = true;
        }
        else LeftMouseDown  = false;
        if(Input.GetMouseButtonDown(1))
        {
        RightMouseDown = true;
        }
        else RightMouseDown = false;
    }
}
