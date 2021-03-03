using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{

    public Dropdown time;
    public InputField path;
    public InputField longi;
    public InputField lat;
    // Start is called before the first frame update
    void Start()
    {
        try {
            path.text = PlayerPrefs.GetString("path");
            longi.text = PlayerPrefs.GetFloat("long").ToString();
            lat.text = PlayerPrefs.GetFloat("lat").ToString();
        }
        catch {

        }
        
    }

    public void go() {
        PlayerPrefs.SetString("path", path.text);
        PlayerPrefs.SetInt("time", time.value);
        PlayerPrefs.SetFloat("lat", float.Parse(lat.text));
        PlayerPrefs.SetFloat("long", float.Parse(longi.text));
        PlayerPrefs.Save();
        SceneManager.LoadScene("loadMain", LoadSceneMode.Single);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
