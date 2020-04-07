using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public GameObject obj;

    public void Activate()
    {
        obj.SetActive(true);
    }
}
