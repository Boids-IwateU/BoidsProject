using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class replace : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall") {
            this.transform.position = new Vector3(Random.Range(-15f, 15f), 0.5f, Random.Range(-15f, 15f));
        }
    }
}
