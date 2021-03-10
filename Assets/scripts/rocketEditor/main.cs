using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

[System.Serializable]
public class main : MonoBehaviour
{

    public GameObject obj;
    // Start is called before the first frame update
    string folderPath = Directory.GetCurrentDirectory() + @"\customRockets";
    void Start()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public void Save() 
    {
        properties props = GetComponent<properties>();
        props.modelFile = GetComponent<modelLoader>().dName;
        props.scale = GetComponent<ui>().getScale();
        props.color = GetComponent<ui>().getColor();

        props.offset = obj.transform.position;

        string json = JsonUtility.ToJson(props);
        print(json);

        
        string name = GetComponent<modelLoader>().file.text.Split('.')[0];

        File.WriteAllText(folderPath + @"\" + name + @".json", json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
