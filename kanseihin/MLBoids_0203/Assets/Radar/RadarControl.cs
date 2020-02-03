using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RadarControl : MonoBehaviour
{
  public bool enemyswitch = true;
  public float bias = 1;
  public float distThresh = 10;
  public Transform PlayerTrans;
  public Camera maincam;
  public GameObject enemyIcon;
  public GameObject baitIcon;
  public GameObject playerIcon;
  private List<GameObject> enemyIcons;
  private List<GameObject> baitIcons;
  private GameObject playerIcons;

  List<Transform> neighborsCheck(string tag)
  {
    List<Transform> list = new List<Transform>();
    //var prodThresh = Mathf.Cos(360 * Mathf.Deg2Rad);

    foreach (var other in GameObject.FindGameObjectsWithTag(tag))
    {
      if (other == this) continue;

      var to = other.transform.position - PlayerTrans.position;
      to.y = 0;
      var dist = to.magnitude;
      if (dist < distThresh)
      {
        //var dir = to.normalized;
        //var fwd = (PlayerTrans.rotation * Vector3.forward).normalized;
        //var prod = Vector3.Dot(fwd, dir);
        //if (prod > prodThresh)
        //{
          list.Add(other.GetComponent<Transform>());
        //}
      }
    }
    return list;
  }

  List<Vector3> posTransfer(List<Transform> list)
  {
    List<Vector3> posList = new List<Vector3>();
    var myangle = maincam.transform.eulerAngles.y * Mathf.Deg2Rad;
    foreach(var member in list)
    {
      posList.Add(new Vector3((member.position.x - PlayerTrans.position.x) / distThresh * Mathf.Cos(myangle) - (member.position.z - PlayerTrans.position.z) / distThresh * Mathf.Sin(myangle),
                              (member.position.x - PlayerTrans.position.x) / distThresh * Mathf.Sin(myangle) + (member.position.z - PlayerTrans.position.z) / distThresh * Mathf.Cos(myangle),
                               0));
      //posList.Add(new Vector3(member.localPosition.x / distThresh * Mathf.Cos(myangle), member.localPosition.z / distThresh * Mathf.Sin(myangle), 0));
    }
    return posList;
  }

  List<GameObject> iconMaker(List<Vector3> list, GameObject icon)
  {
    List<GameObject> icons = new List<GameObject>();
    foreach(var member in list)
    {
      GameObject newicon = Instantiate(icon, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
      newicon.transform.SetParent(transform);
      newicon.transform.localPosition = member * bias;
      newicon.transform.localScale = new Vector3(1, 1, 1) * bias;
      icons.Add(newicon);
    }
    return icons;
  }

  void iconRemainer(List<Vector3> list, GameObject icon, List<GameObject> newlist)
  {
    foreach (var member in list)
    {
      if (list.IndexOf(member) >= newlist.Count)
      {
        GameObject newicon = Instantiate(icon, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        newicon.transform.SetParent(transform);
        newicon.transform.localPosition = member * bias;
        newicon.transform.localScale = new Vector3(1, 1, 1) * bias;
        newlist.Add(newicon);
      }
      else
      {
        newlist[list.IndexOf(member)].transform.localPosition = member * bias;
        newlist[list.IndexOf(member)].transform.SetAsLastSibling();
      }
    }
    while (newlist.Count > list.Count)
    {
      GameObject rm = newlist[newlist.Count - 1];
      newlist.Remove(rm);
      Destroy(rm);
    }
  }
  void allDestroyer(List<GameObject> objs)
  {
    foreach (var obj in objs) Destroy(obj);
  }

  // Start is called before the first frame update
  void Start()
  {
    if (ReferenceEquals(PlayerTrans, null)) PlayerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    if (ReferenceEquals(maincam, null)) maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    if(enemyswitch)enemyIcons = iconMaker(posTransfer(neighborsCheck("boids")), enemyIcon);
    baitIcons = iconMaker(posTransfer(neighborsCheck("bait")), baitIcon);
    playerIcons = Instantiate(playerIcon, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, maincam.transform.eulerAngles.y - PlayerTrans.eulerAngles.y));
    playerIcons.transform.SetParent(this.transform);
    playerIcons.transform.localPosition = new Vector3(0, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    if (enemyswitch) iconRemainer(posTransfer(neighborsCheck("boids")), enemyIcon, enemyIcons);
    iconRemainer(posTransfer(neighborsCheck("bait")), baitIcon, baitIcons);
    playerIcons.transform.rotation = Quaternion.Euler(0, 0, maincam.transform.eulerAngles.y - PlayerTrans.eulerAngles.y);
    playerIcons.transform.localPosition = new Vector3(0, 0, 0);
    playerIcons.transform.localScale = new Vector3(1, 1, 1) * bias;
    playerIcons.transform.SetAsLastSibling();
  }
}
