using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mapbox.Unity.Map;
using Mapbox.Map;
using Mapbox.Utils;

public class demoMap : MonoBehaviour {
 
	
	public AbstractMap map;


	void Start () {
		
		//map = GameObject.Find("Map").GetComponent<Mapbox.Unity.Map.AbstractMap>();
		print(map);

		float lat = PlayerPrefs.GetFloat("lat");
		float longi = PlayerPrefs.GetFloat("long"); 

		
		Vector2d coords = new Vector2d(lat, longi);
		map.UpdateMap(coords, 15f);

		
	}
}