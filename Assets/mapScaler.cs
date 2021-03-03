using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapScaler : MonoBehaviour
{
   
    public float Scale;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localScale = new Vector3(Scale, 1, Scale);
    }
}
