using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TJ_Audio_Visual : MonoBehaviour {

    public GameObject BARSSSSS;
    public float Num_Boxes;
    float f_Val;

    public List<GameObject> AudioVisualizer_List;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < Num_Boxes; i++)
        {
            if (i % 2 == 0)
            {
                GameObject tempobj = GameObject.Instantiate(BARSSSSS, transform);
                tempobj.transform.position = new Vector3(-9, (i * (9/Num_Boxes)) -4, -2);
                tempobj.transform.localScale = Vector3.one;
                AudioVisualizer_List.Add(tempobj);
            }
            else
            {
                GameObject tempobj = GameObject.Instantiate(BARSSSSS, transform);
                tempobj.transform.position = new Vector3(9, (i * (9 / Num_Boxes)) - 4, -2);
                tempobj.transform.localScale = Vector3.one *-1;
                AudioVisualizer_List.Add(tempobj);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        f_Val = 0;

        float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for(int i=0; i<AudioVisualizer_List.Count; i ++)
        {
            for(int j=0; j<10*(Num_Boxes-i); j++)
            {
                f_Val += spectrum[j];
            }
            if (AudioVisualizer_List[i].transform.localScale.x > 0)
                AudioVisualizer_List[i].transform.localScale = new Vector3(f_Val/10.0f, AudioVisualizer_List[i].transform.localScale.y, 0);
            else
                AudioVisualizer_List[i].transform.localScale = new Vector3(-f_Val/10.0f, AudioVisualizer_List[i].transform.localScale.y, 0);
        }

    }
}
