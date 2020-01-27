﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {   
        timeText.text = "Score : " + GameManagement.score.ToString();

    }
}
