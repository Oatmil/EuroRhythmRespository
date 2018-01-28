using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TJ_Audio_Visual : MonoBehaviour {

    public GameObject BG;
    public float Modifier;
    Color BG_Color;
    
	// Use this for initialization
	void Start () {
        BG_Color = Color.white;
        if (BG.GetComponent<SpriteRenderer>() != null)
            BG_Color = BG.GetComponent<SpriteRenderer>().color;
    }


    // Update is called once per frame
    void Update () {

        float[] spectrum = new float[128];
        BG_Color.a = 0;
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

        BG_Color.a += spectrum[5] + spectrum[6] + spectrum[7] + spectrum[8] + spectrum[9] + spectrum[10] + spectrum[11] + spectrum[12] + spectrum[20] + spectrum[21] + spectrum[22] + spectrum[23] + spectrum[24] + spectrum[25];

        BG_Color.a = BG_Color.a * Modifier;
        if (BG_Color.a > 1)
            BG_Color.a = 1;
        if (BG.GetComponent<CanvasRenderer>() != null)
            BG.GetComponent<CanvasRenderer>().SetColor(BG_Color);
        else
            BG.GetComponent<SpriteRenderer>().color = BG_Color;

    }
}
