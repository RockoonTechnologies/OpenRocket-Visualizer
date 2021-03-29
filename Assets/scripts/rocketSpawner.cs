using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketSpawner : MonoBehaviour
{
    public GameObject shuttle;
    public GameObject dRocket;
    // Start is called before the first frame update
    public void LOAD()
    {
        GameObject model;
        int choice = PlayerPrefs.GetInt("model");
        if (choice == 0) 
            model = dRocket;
        else
            model = shuttle;

        GameObject spawned = Instantiate(model, new Vector3(0, 0, 0), Quaternion.identity);
        gameObject.GetComponent<csvRunner>().rocket = spawned.transform;
        Camera.main.gameObject.GetComponent<orbit>().target = spawned.transform;
        Camera.main.gameObject.GetComponent<cameraManager>().tr = spawned.transform.GetChild(2).gameObject.GetComponent<TrailRenderer>();
        gameObject.GetComponent<csvRunner>().tr = spawned.transform.GetChild(2).gameObject.GetComponent<TrailRenderer>();
        Camera.main.gameObject.GetComponent<lookCam>().target = spawned.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
