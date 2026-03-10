using UnityEngine;

namespace Version_1
{
 public static class UserAlgorithms
 {

  public static bool IsTrumpetHovered()
  {
      return Input.GetKeyDown(KeyCode.T);
  }

  public static bool IsBookClicked(GameObject obj)
  {
      return Input.GetMouseButtonDown(0) && IsPlayerLookingAt(obj);
  }

  public static void PlayTrumpet()
  {
      GameObject trumpet = GameObject.Find("Trumpet");
      AudioSource a = trumpet.GetComponent<AudioSource>();
      a.Play();

      VReqDV.StateAccessor.SetState("Trumpet","played",trumpet,"Version_1");
  }

  public static bool AllBooksDestroyed()
  {
      for(int i=1;i<=4;i++)
      {
          GameObject b = GameObject.Find("Book_"+i);

          if(!VReqDV.StateAccessor.IsState("Book_1","destroyed",b,"Version_1"))
              return false;
      }

      return true;
  }

  public static void DestroyBook(GameObject obj)
  {
      if (obj == null)
          return;

      AudioSource a = obj.GetComponent<AudioSource>();
      if (a != null && a.clip != null)
          AudioSource.PlayClipAtPoint(a.clip, obj.transform.position);

      VReqDV.StateAccessor.SetState(obj.name, "destroyed", obj, "Version_1");
      GameObject.Destroy(obj);
  }

  public static void OpenChest(GameObject obj)
  {
      Animator a = obj.GetComponent<Animator>();
      a.SetTrigger("Open");

      VReqDV.StateAccessor.SetState("Chest","open",obj,"Version_1");
  }

  private static bool IsPlayerLookingAt(GameObject obj)
  {
      if (obj == null)
          return false;

      Camera cam = Camera.main;
      if (cam == null)
          return false;

      Ray ray = new Ray(cam.transform.position, cam.transform.forward);
      return Physics.Raycast(ray, out RaycastHit hit, 3f)
          && hit.collider.gameObject == obj;
  }
 }
}