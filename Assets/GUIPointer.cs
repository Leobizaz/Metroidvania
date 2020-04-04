using UnityEngine;
using System.Collections;

public class GUIPointer : MonoBehaviour
{

    //public Texture2D tex;    // Texture to be rotated
    public Transform target; // GameObject to point to
    //public Vector2 pivot;    // Where to place the center of the texture
    float angle;
    Vector3 dir;
    private Rect rect;

    void Start()
    {
        //rect = new Rect(pivot.x - tex.width * 0.5f, pivot.y - tex.height * 0.5f, tex.width, tex.height);
    }

    private void OnGUI()
    {
        Vector3 targetPosLocal = Camera.main.transform.InverseTransformPoint(target.position);
        angle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, angle);

        //dir = target.position - transform.position;
        //angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
