using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public GameObject obj;
    public bool oneTime = true;
    bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
