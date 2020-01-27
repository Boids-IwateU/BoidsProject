using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{

    private Text timeText;
    float timeLeft = 70;
    int  min, sec;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {   
        timeLeft -= Time.deltaTime;
        min = (int)timeLeft/60;  
        sec = (int)timeLeft%60; 
        timeText.text =min.ToString() + "　分　" + sec.ToString() +　"　秒";
        
        if (min == 0 && sec == 0){
            SceneManager.LoadScene(2);
            GameManagement.score = 0;
        } 
    }
}
