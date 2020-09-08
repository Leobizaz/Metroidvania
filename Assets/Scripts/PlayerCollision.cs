using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ferr;

public class PlayerCollision : MonoBehaviour
{
    public bool onGround;
    public bool onWall;
    public bool onWallLeft;
    public bool onWallRight;

    public bool onGroundCoyote;
    public float coyoteTime;
    private bool coyote;
    private bool respawn;

    public LayerMask groundLayer;
    public LayerMask wallLayer;

    public AnimationSignal_Player animSignal;
    public LayerMask raycastFilter;

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    private void Start()
    {
        animSignal = GetComponentInChildren<AnimationSignal_Player>();
    }

    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        if (onGround) coyote = false;

        if (onGround)
        {
            RaycastHit2D detectGroundType = Physics2D.Raycast(transform.position, Vector2.down * 10, 10, raycastFilter);
            if (detectGroundType.collider != null)
            {
                //Debug.Log(detectGroundType.collider.name);

                if (detectGroundType.collider.sharedMaterial.name == "Carne")
                {
                    animSignal.PassoCarne();
                }
                else if (detectGroundType.collider.sharedMaterial.name == "Pedra")
                {
                    animSignal.PassoPedra();
                }
                else if (detectGroundType.collider.sharedMaterial.name == "Metal")
                {
                    animSignal.PassoMetal();
                }
                else
                {
                    animSignal.PassoPedra();
                }
            }
        }

        if (onGround == false && !coyote)
        {
            Invoke("ResetCoyote", coyoteTime);
            coyote = true;
            onGroundCoyote = true;
        }

        onWallLeft = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, wallLayer);
        onWallRight = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, wallLayer);
        if (onWallLeft || onWallRight)
        {
            onWall = true;
        }
        else onWall = false;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

    void ResetCoyote()
    {
        onGroundCoyote = false;
    }
}
