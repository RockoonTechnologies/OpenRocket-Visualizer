using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{

    public Dropdown time;
    public InputField path;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void go() {
        PlayerPrefs.SetString("path", path.text);
        PlayerPrefs.SetInt("time", time.value);
        PlayerPrefs.Save();
        SceneManager.LoadScene("loadMain", LoadSceneMode.Single);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
