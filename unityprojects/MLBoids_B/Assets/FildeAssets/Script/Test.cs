using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, 10);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(10, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, -10);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(-10, 0, 0);
        }
    }

}