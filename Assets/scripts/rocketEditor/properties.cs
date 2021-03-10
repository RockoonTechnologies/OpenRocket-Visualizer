using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class properties : MonoBehaviour
{
    public float Version = 0.1f;
    
    public string modelFile = "";
    public Vector3 offset;
    public float scale;

    public Color32 color;
}
