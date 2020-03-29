using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanternController : MonoBehaviour
{
    public GameObject pivot;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = pivot.transform.position;

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0f, 0f, rot_z - 90);

    }
}
