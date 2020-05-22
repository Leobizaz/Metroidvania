using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class GUIPointer : MonoBehaviour
{

    //public Texture2D tex;    // Texture to be rotated
    public Transform target; // GameObject to point to
    //public Vector2 pivot;    // Where to place the center of the texture
    float angle;
    Vector3 dir;
    private Rect rect;
    Transform parent;
    public AnimationCurve curve;

    private void OnEnable()
    {
        parent = this.transform.parent.transform;
        parent.localScale = new Vector3(1, 0, 1);
        parent.DOScale(new Vector3(1, 1, 1), 1).SetEase(curve);
    }
    void Update()
    {

    }
    private void OnGUI()
    {
        Vector3 targetPosLocal = Camera.main.transform.InverseTransformPoint(target.position);
        angle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
