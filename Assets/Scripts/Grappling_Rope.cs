using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling_Rope : MonoBehaviour
{
    private LineRenderer GrapplingGun_linerenderer;
    private Vector3 h;
    [SerializeField] private AnimationCurve AnimationRopeNoWave;
    [SerializeField] private AnimationCurve AnimationRopeWave;
    void Start()
    {
        GrapplingGun_linerenderer = GetComponent<LineRenderer>();   
    }
}
