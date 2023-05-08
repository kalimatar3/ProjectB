using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
   [SerializeField] protected float AliveTime;
    void Start()
    {
        Destroy(gameObject,AliveTime);
    }
}
