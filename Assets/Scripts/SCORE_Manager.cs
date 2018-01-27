using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCORE_Manager : MonoBehaviour {

    public static SCORE_Manager m_instance;

    public float f_HitSpacing;
    public float f_ComboCounter;
    public int i_Life;

    public float f_NoteSpeed;

    public float HighScore;

    private void Awake()
    {
        m_instance = this;
    }
    
    public void SCORE()
    {
        f_ComboCounter++;
        if (f_ComboCounter > HighScore)
            HighScore = f_ComboCounter;
    }

    public void TOTALMISS()
    {
        f_ComboCounter = 0;
    }

}
