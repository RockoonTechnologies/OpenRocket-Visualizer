using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookCam : MonoBehaviour
{

    public Transform target;
    public bool special;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        if(!special)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -transform.eulerAngles.y,transform.eulerAngles.z);
        
        
    }
}
