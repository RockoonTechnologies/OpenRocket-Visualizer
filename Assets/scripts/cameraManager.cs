using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    orbit orb;
    lookCam look;
    Vector3 lookie;

    public TrailRenderer tr;


    // Start is called before the first frame update
    void Start()
    {
        orb = gameObject.GetComponent<orbit>();
        look = gameObject.GetComponent<lookCam>();
        lookie = transform.position;
    }

    bool inTrack = true;
    public void toggle() 
    {
        if(inTrack) {
            inTrack = false;
            orb.enabled = false;
            look.enabled = true;
            transform.position = lookie;
        }
        else {
            inTrack = true;
            look.enabled = false;
            orb.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        tr.widthMultiplier = Vector3.Distance(tr.transform.position, look.transform.position) * .2f;
    }
}
