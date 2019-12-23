using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bait : MonoBehaviour
{
  // Update is called once per frame
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      gameObject.GetComponentInParent<GameManagement>().score++;
      Destroy(gameObject);
    }
  }
}
