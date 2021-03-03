using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class loader : MonoBehaviour
{
    string path;
    TextAsset file;
    public Text status;
    // Start is called before the first frame update
    void Start()
    {
        status.text = "Checking Things...";
        path = PlayerPrefs.GetString("path");
        if (!System.IO.File.Exists(path))
        {
            status.text = "Cant find file!";
            Application.Quit();
        }
        StartCoroutine(Pre(path));
    }

    // Update is called once per frame
    private IEnumerator Pre(string path)
    {
        yield return new WaitForSeconds(0.5f);
        status.text = "PreProccessing Data...";
        string contents = File.ReadAllText(path);
        file = new TextAsset(contents);
        
        string[,] array = CSVReader.SplitCsvGrid(file.text);
        float val1 = float.Parse(array[0, 1]);
        float val2 = float.Parse(array[0, 2]);
        float dt = val2 - val1;
        PlayerPrefs.SetFloat("dt", dt);
        PlayerPrefs.Save();

        float oldHeight = 0;
        Vector3 apogee;
        for(int x = 1; x < array.GetLength(1)-2; x++) {

            float height = float.Parse(array[1, x]);
            if(height < oldHeight) {
                apogee = new Vector3(float.Parse(array[2, x]), height, float.Parse(array[3, x]));

            }
            oldHeight = height;
        }

        yield return new WaitForSeconds(0.5f);
        status.text = "Loading Assets, Scene";
        SceneManager.LoadScene("SampleScene");
    }
}
