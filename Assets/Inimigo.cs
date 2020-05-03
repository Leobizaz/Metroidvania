using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float speed = 3;
    public float jumpForce = 50;
    bool playerDetected;
    Rigidbody2D rb;
    GameObject player;
    public GameObject checkBuraco;
    public GameObject checkGrounded;
    public GameObject checkWall;
    public GameObject checkWall2;
    public GameObject sprite;
    public LayerMask whatIsGround;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isTouchingWall;
    float jumpCooldown;
    float distance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !playerDetected)
        {
            playerDetected = true;
            player = collision.gameObject;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if (Physics2D.OverlapCircle(checkGrounded.transform.position, 0.5f, whatIsGround)) isGrounded = true; else isGrounded = false; // checa se o inimigo está no chão

        if (Physics2D.OverlapCircle(checkWall.transform.position, 0.5f, whatIsGround) || Physics2D.OverlapCircle(checkWall2.transform.position, 0.5f, whatIsGround)) isTouchingWall = true; else isTouchingWall = false; // checa se o inimigo está colidindo com parede

        if (playerDetected)
        {
            //Olha para o jogador
            var heading = player.transform.position - this.transform.position;
            distance = heading.magnitude;
            Vector2 direction = heading / distance;

            if (direction.normalized.x > 0)
            {
                sprite.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.normalized.x < 0)
            {
                sprite.transform.localScale = new Vector3(-1, 1, 1);
            }
            //Segue o jogador
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);

            if((player.transform.position.y - this.transform.position.y) > 1 && jumpCooldown <= 0 && isGrounded && isTouchingWall) //checa se o jogador está numa posição elevada e se o inimigo pode pular
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCooldown = 4f;
            }

            if(jumpCooldown > 0)
            {
                jumpCooldown -= Time.deltaTime;
            }


        }
        else
        {
            //Anda pra direita dps esquerda
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(checkBuraco.transform.position.x, transform.position.y), speed / 1.5f * Time.deltaTime);

            if(!Physics2D.OverlapCircle(checkBuraco.transform.position, 0.5f, whatIsGround)) //checa se há um buraco
            {
                Flip(); //muda a direção de andar
            }

        }
    }

    void Flip()
    {
        sprite.transform.localScale = new Vector3(-sprite.transform.localScale.x, sprite.transform.localScale.y, sprite.transform.localScale.z);
    }


}
