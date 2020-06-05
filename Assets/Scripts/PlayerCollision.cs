using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;


    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        if (onGround) coyote = false;

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
