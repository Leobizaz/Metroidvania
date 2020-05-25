using UnityEngine;
using UnityEngine.Experimental.U2D.IK;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class FloorHazard : MonoBehaviour
{
    public Animator spriteAnimator;
    SpriteRenderer sprite;
    private bool PlayerLocked;
    private int count = 13;
    public HingeJoint2D hinge;
    float storedIntensity;
    bool once;
    public Light2D redlight;
    AudioSource audioSource;
    public AudioClip sfx_bite;
    public AudioClip sfx_crunching;
    public AudioClip sfx_crunchend;
    GameObject perna;
    GameObject capturedObject;

    void Start()
    {
        sprite = spriteAnimator.gameObject.GetComponent<SpriteRenderer>();
        perna = GameObject.Find("PERNABOB");
        PlayerLocked = false;
        audioSource = GetComponent<AudioSource>();
        storedIntensity = redlight.intensity;
        redlight.intensity = 0;
    }

    void Update()
    {
        

        if(PlayerLocked == true)
        {
            if (count <= 0)
            {
                UnlockPlayer();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                count -= 1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                count -= 1;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                count -= 4;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player" && !once)
        {
            capturedObject = other.gameObject;
            LockRigidbody();
            GameEvents.current.MakeBigSound(this.gameObject);
            //Invoke("LockRigidbody", 0.5f);
            redlight.intensity = storedIntensity;
            perna.transform.position = this.transform.position;
            perna.transform.GetComponentInParent<LimbSolver2D>().enabled = true;
            once = true;
            spriteAnimator.Play("bite");
            PlayerLocked = true;
            count = 13;
            audioSource.PlayOneShot(sfx_bite);
            audioSource.clip = sfx_crunching;
            audioSource.loop = true;
            audioSource.Play();
            //PlayerController.hits = -1;
        }
    }

    void UnlockPlayer()
    {
        
        audioSource.Stop();
        audioSource.PlayOneShot(sfx_crunchend);
        DOVirtual.Float(storedIntensity, 0, 2, ChangeLight);
        perna.transform.GetComponentInParent<LimbSolver2D>().enabled = false;
        hinge.enabled = false;
        hinge.connectedBody = null;
        spriteAnimator.Play("dead");
        PlayerLocked = false;
        
        Invoke("HideSprite", 2);
    }
    void HideSprite()
    {
        sprite.DOColor(new Color(0, 0, 0, 0), 3);
        Destroy(gameObject, 4);
    }

    void LockRigidbody()
    {
        hinge.connectedBody = capturedObject.GetComponent<Rigidbody2D>();
        hinge.autoConfigureConnectedAnchor = false;
        DOVirtual.Float(hinge.connectedAnchor.x, 0, 2, UpdateHingeX);
        DOVirtual.Float(hinge.connectedAnchor.y, 0, 2, UpdateHingeY);

    }

    void UpdateHingeX(float v)
    {
        hinge.connectedAnchor = new Vector2(v, hinge.connectedAnchor.y);
    }
    void UpdateHingeY(float v)
    {
        hinge.connectedAnchor = new Vector2(hinge.connectedAnchor.x, v);
    }

    void ChangeLight(float v)
    {
        redlight.intensity = v;
    }
}
