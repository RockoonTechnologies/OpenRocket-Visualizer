using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draggy : MonoBehaviour
{
    orbit cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject.GetComponent<orbit>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.enabled = true;
    }

    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
 
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
 
    }
    
    void OnMouseDrag() {
        cam.enabled = false;
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
 
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = new Vector3(transform.position.x, curPosition.y, transform.position.z);
    }
}
