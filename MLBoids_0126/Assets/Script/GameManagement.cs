using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
  public int playerhitcount = 0;
  public int score = 0;
  public float FieldSize = 100;

  public Vector3 Field { get; private set; }


  private void Start()
  {
    Field = new Vector3(FieldSize / 2, FieldSize / 2, FieldSize / 2);
  }
}
