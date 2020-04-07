using UnityEngine;

public class HUDBootAnimation : MonoBehaviour
{
    public Animator anim;

    private void OnEnable()
    {
        anim.enabled = true;
    }
}
