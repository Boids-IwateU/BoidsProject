<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //モーションを切り替える
        if(Input.GetAxis("Vertical") > 0)
        {
            animator.SetInteger("Vertical", 1);
        }
        else
        {
            animator.SetInteger("Vertical", 0);
        }
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //モーションを切り替える
        if(Input.GetAxis("Vertical") > 0)
        {
            animator.SetInteger("Vertical", 1);
        }
        else
        {
            animator.SetInteger("Vertical", 0);
        }
    }
}
>>>>>>> 47734b0ab5f0c652757489868dad1eb9f700194e
