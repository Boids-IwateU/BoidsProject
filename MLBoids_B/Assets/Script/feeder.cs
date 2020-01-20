using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feeder : MonoBehaviour
{
  [SerializeField]
  GameObject baitprefab;

  [SerializeField]
  int amount = 0;

  void AddBaits()
  {
    float lim = gameObject.GetComponent<GameManagement>().FieldSize / 2;
    var go = Instantiate(baitprefab, new Vector3(Random.Range(-lim, lim), -lim+transform.localScale.y/2, Random.Range(-lim, lim)), Random.rotation);
    go.transform.SetParent(this.transform);
    
  }

  // Start is called before the first frame update
  void Start()
  {
    for(int i = 0; i < amount; i++)
    {
      AddBaits();
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
}
