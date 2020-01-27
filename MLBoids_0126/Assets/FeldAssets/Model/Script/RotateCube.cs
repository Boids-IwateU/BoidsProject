
﻿using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0));
    }

}