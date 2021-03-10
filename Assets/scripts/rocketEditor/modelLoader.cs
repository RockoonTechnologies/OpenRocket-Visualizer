using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.UI;

public class modelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    string path = Directory.GetCurrentDirectory() + @"\customRockets";

    public InputField file;
    public Text status;

    public GameObject panel;
    ui UI;

    public string dName;
    void Start()
    {
        status.text = "";
        UI = GetComponent<ui>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load() {
        string filePath = file.text;
        bool exists = checkExists(filePath);
        print(path + @"\" + filePath);
        if(!exists) {
            status.text = "Could not find file :(";
            return;
        }
        dName = filePath;
        panel.SetActive(false);
        Transform holder = GameObject.Find("holder").transform;
        GameObject rocket = new GameObject("rocket");
        rocket.transform.parent = holder;

        Mesh rocketMesh = new Mesh();
        ObjImporter newMesh = new ObjImporter();
        rocketMesh = newMesh.ImportFile(path + @"\" + filePath);

        MeshRenderer renderer = rocket.AddComponent<MeshRenderer>();
        renderer.material = UI.placeholder;
        MeshFilter filter = rocket.AddComponent<MeshFilter>();
        filter.mesh = rocketMesh;

        MeshCollider col = rocket.AddComponent<MeshCollider>();
        col.convex = true;

        rocket.AddComponent<draggy>();
        GetComponent<ui>().scale.text = ".1";

        GetComponent<main>().obj = rocket;
    }

    bool checkExists(string subPath) {
        if (!File.Exists(path + @"\" + subPath))
        {
           print(path + @"\" + subPath);
           return false;
        }
        return true;
    }
}
