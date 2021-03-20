﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mapbox.Unity.Map;
using Mapbox.Map;
using Mapbox.Utils;

public class csvRunner : MonoBehaviour {
 
	
	public Transform rocket;
	//public ParticleSystem flameys;
	public Transform light;
	Rigidbody rb;

	public GameObject loading;
	


	public bool paused;
	public Text pauseText;
	public Text alt;
	public Slider timeline;
	public Slider timeScale;

	orientation ori;

	Coroutine coro;

	float dt;
	string contents;


	public AbstractMap map;

	

	void Start () {
		
		//map = GameObject.Find("Map").GetComponent<Mapbox.Unity.Map.AbstractMap>();
		//print(map);
		//GameObject.Find("ReloadMapCanvas").GetComponent<Mapbox.Examples.ReloadMap>().Awake();
		//GameObject.Find("ReloadMapCanvas").GetComponent<Mapbox.Examples.ReloadMap>().Reload(1);
		gameObject.GetComponent<rocketSpawner>().LOAD();

		

		dt = PlayerPrefs.GetFloat("dt");
		string path = PlayerPrefs.GetString("path");
		contents = File.ReadAllText(path);
		int time = PlayerPrefs.GetInt("time");
		if(time == 1) {
			light.eulerAngles = new Vector3(-90f, 0, 0);
		}


		//float lat = PlayerPrefs.GetFloat("lat");
		//float longi = PlayerPrefs.GetFloat("long"); 
		
		//Vector2d coords = new Vector2d(lat, longi);
		
		//print(coords);
		//map.UpdateMap(coords, 15f);

		//CSVReader.DebugOutputGrid( CSVReader.SplitCsvGrid(csv.text) );
		string[,] array = CSVReader.SplitCsvGrid(contents);
		ori = GetComponent<orientation>();
		ori.dt = dt;
		rb = rocket.gameObject.GetComponent<Rigidbody>();

		timeScale.value = 0.7f;
		
		coro = StartCoroutine(Run(array, 1));
		
	}

	float ftToM(float ft) {
		return ft / 3.281f;
	}

	float degrees(float r) {
		return r * 180/ori.PI;
	}

	public void togglePause() {
		if(paused) {
			paused = false;
			pauseText.text = "Playing";
		}
		else {
			paused = true;
			pauseText.text = "Paused";
		}
	}

	bool iChanged = true;
	public void timeLineChange() {
		if(iChanged) {
			return;
		}
		paused = true;
		pauseText.text = "Paused";

		StopCoroutine(coro);
		string[,] array = CSVReader.SplitCsvGrid(contents);
		
		coro = StartCoroutine(Run(array, (int) timeline.value));
	}

	public void timeChange() {
		Time.timeScale = timeScale.value;
	}

	private IEnumerator Run(string[,] data, int start)
    {
	   rb.isKinematic = true;
	   timeline.maxValue = data.GetLength(1)-5;
	   float oldHeight = 0;
	   //flameys.Play();
       for(int x = start; x < data.GetLength(1)-5; x++) {
		   loading.SetActive(false);
		   while(paused) {
			   yield return null;
			  
		   }
			
		   float time = float.Parse(data[0, x]);
		   float height = float.Parse(data[1, x]);
		   alt.text = "Altitude: " + height.ToString() + "m";
		   float xPos = float.Parse(data[2, x]);
		   float yPos = float.Parse(data[3, x]);

		   float rollRate = degrees(float.Parse(data[4, x]));
		   float pitchRate = degrees(float.Parse(data[5, x]));
		   float yawRate = degrees(float.Parse(data[6, x]));

		   Vector3 oriUpdate = ori.update(yawRate, rollRate, pitchRate);

		   rocket.position = new Vector3(xPos, height, yPos);
		   rocket.eulerAngles = new Vector3(oriUpdate.x, oriUpdate.y, -oriUpdate.z);

		   if(height < oldHeight) {
			   //flameys.Stop();
		   }

		   oldHeight = height;
		   iChanged = true;
		   timeline.value = x;
		   iChanged = false;

		   yield return new WaitForSeconds(dt);
	   }
	   rb.isKinematic = false;
	   while(true) {yield return new WaitForSeconds(0.01f);}
	   
    }
}