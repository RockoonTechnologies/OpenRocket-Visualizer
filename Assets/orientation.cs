using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class orientation : MonoBehaviour
{

    Vector3 angle;
    public float PI = Mathf.PI;
    public float dt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public Vector3 update(float xRate, float yRate, float zRate)
    {
        Vector3 BodyRates = new Vector3(xRate, yRate, zRate);

        float cos_Roll = Mathf.Cos((angle.x/100f)*PI);
	    float sin_Roll = Mathf.Sin((angle.x/100f)*PI);
	    float cos_Pitch = Mathf.Cos((angle.y/100f)*PI);
	    float tan_Pitch = Mathf.Tan((angle.y/100f)*PI);

        float roll_Dot = BodyRates.x + BodyRates.y * sin_Roll * tan_Pitch + BodyRates.z * cos_Roll * tan_Pitch;
	    float pitch_Dot = BodyRates.y * cos_Roll - BodyRates.z * sin_Roll;
	    float yaw_Dot = BodyRates.y * sin_Roll / cos_Pitch + BodyRates.z * cos_Roll / cos_Pitch;

        angle.x += roll_Dot * (dt) * 2;
	    angle.y += pitch_Dot * (dt) * 2;
	    angle.z += yaw_Dot * (dt) * 2;
    
	    return angle;
    }
}
