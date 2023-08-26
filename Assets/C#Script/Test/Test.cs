using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public partial class Test : MonoBehaviour
{
    public float length;
    
    // Start is called before the first frame update
    void Start()
    {
      Debug.Log(Quaternion.AngleAxis(90,Vector3.back)*new Vector3(1,0,0));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
