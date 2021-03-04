using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class main : MonoBehaviour
{
    // Start is called before the first frame update
    string folderPath = Directory.GetCurrentDirectory() + @"\customRockets";
    void Start()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
