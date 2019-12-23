using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bait : MonoBehaviour
{
  // Update is called once per frame
  void OnTriggerEnter(Collider collider)
  {
    if (collider.gameObject.tag == "Player")
    {
      gameObject.GetComponentInParent<GameManagement>().score++;
      Destroy(gameObject);
    }
  }
}
