using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui : MonoBehaviour
{

    public Slider R,G,B;
    public Image preview;
    public InputField scale;

    public Transform holder;

    public Material placeholder;
    // Start is called before the first frame update
    void Start()
    {
        scale.text = "1";
        R.value = 255;
    }

    // Update is called once per frame
    void Update()
    {
        Color32 color = new Color32((byte) (byte) R.value, (byte) G.value, (byte) B.value, (byte) 255);
        preview.color = color;

        float s = float.Parse(scale.text);
        holder.localScale = new Vector3(s,s,s);

        placeholder.color = color;
    }

    public Color32 getColor() {
        return new Color32((byte) (byte) R.value, (byte) G.value, (byte) B.value, (byte) 255);
    }

    public float getScale() {
        return float.Parse(scale.text);
    }
}
