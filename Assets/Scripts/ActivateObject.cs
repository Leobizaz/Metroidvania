using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public GameObject obj;
    public bool oneTime = true;
    bool once;

    public bool onEnable = false;
    public float onEnableDelay = 0;

    private void OnEnable()
    {
        if (onEnable)
        {
            Invoke("Activate", onEnableDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onEnable) return;
        if (collision.tag == "Player" && !once)
        {
            Activate();
            if (oneTime)
            {
                once = true;
                Destroy(gameObject, 0.1f);
            }
        }
    }

    public void Activate()
    {
        obj.SetActive(true);
    }
}
