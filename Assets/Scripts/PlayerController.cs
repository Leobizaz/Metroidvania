using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Variaveis ajustaveis
    public float movementSpeed = 10f;
    public float movement_acceleration = 0.2f;
    public float movement_decceleration = 0.05f;
    public float jumpForce = 2f;
    public float tempoReload = 1.5f; // Tempo em segundos que leva para recarregar
    public float flashCooldown = 2;

    // Variaveis essenciais
    float currentMoveSpeed;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool wantToJump;
    [HideInInspector] public bool onRope;
    public bool unlockedFlash;
    private float invulnerabilityFrame = 0.5f;
    private float hitCooldown;
    private float yVelocity = 0.0f;
    private float jumpCooldown;
    [HideInInspector] public bool isBusy;
    private float storedAccelerationValue;
    private int bulletcount;
    private Vector3 spriteScale;
    private Vector3 mousePos;
    [SerializeField] private float x = 0;
    [SerializeField] private float y = 0;
    private float flashTime;
    private bool died;
    GameObject lastEnemyToHit;

    // Variaveis estaticas
    public static bool facingleft;
    public static bool facingright;
    public static int hits = 3;

    // Referencias privadas
    private PlayerCollision p_collision;
    private Rigidbody2D rb;
    private SoundPlayer soundplayer;
    private AudioSource reload;

    // Referencias publicas
    public GameObject playersprite;
    public Animator playerAnim;
    public GameObject gun;
    public GameObject bullet_normal;
    public GameObject BackLight;
    public AudioClip LampDone;
    public GameObject DMG;
    public GameObject flash;
    public Animator hud_dmg;
    public Animator hud_noFlash;
    public Material red_material;

    private void Start()
    {
        Initialize();
    }



    private void FixedUpdate()
    {

        DeathCheck();

        if (!died)
        {
            GroundCheck();
            MoveCheck();
            ShootMechanic();
            if (!isBusy)
            {
                FlipSprite();
                //MovementSmoothing();
                MovementMechanic();

                if (unlockedFlash)
                {
                    FlashMechanic();

                }

            }
        }

    }

    //Funções principais:
    private void Initialize()
    {
        currentMoveSpeed = movementSpeed;
        spriteScale = playersprite.transform.localScale;
        soundplayer = GetComponent<SoundPlayer>();
        storedAccelerationValue = movement_acceleration;
        p_collision = GetComponent<PlayerCollision>();
        rb = GetComponent<Rigidbody2D>();
        reload = gun.GetComponent<AudioSource>();
    }
    private void MovementMechanic()
    {
        // Essa função controla tudo a respeito da movimentação do jogador

        if (Input.GetAxisRaw("Horizontal") != 0) // Checa se o jogador está apertando para ir pros lados
        {
            isMoving = true; // Diz que o jogador está se movimentando
            x = Mathf.SmoothDamp(x, Input.GetAxis("Horizontal"), ref yVelocity, movement_acceleration); // Registra o valor da movimentação horizontal de acordo com a aceleração
        }
        else
        {
            isMoving = false; // Diz que o jogador não está se movimentando
        }

        Vector2 dir = new Vector2(x, y); // Registra o vetor de movimento de acordo com os valores de input horizontal e vertical


        if (Input.GetAxisRaw("Horizontal") == 0) // Checa se o jogador não está apertando para andar
        {
            x = Mathf.SmoothDamp(x, 0, ref yVelocity, movement_decceleration); // Registra o valor da movimentação horizontal de acordo com a desceleração
                                                                               //animação do jogador parando vai aqui
        }

        rb.velocity = new Vector2(dir.x * currentMoveSpeed, rb.velocity.y); // Movimenta o jogador para os lados de acordo com o vetor 'dir' e a velocidade variavel 


        if (Input.GetButtonDown("Jump") && Input.GetAxis("Vertical") >= 0)
        {
            // Essa função indica que o jogador quer pular
            // Ela serve pra caso o jogador aperte o botão de pular muito cedo enquanto ainda não tocou no chão
            // Basicamente deixa o input de pular mais confortável
            CancelInvoke("ResetWantToJump");
            Invoke("ResetWantToJump", 0.07f);
            wantToJump = true;
        } // Detecção de pulo automático
        if (p_collision.onGround && jumpCooldown <= 0 && wantToJump == true && Input.GetAxis("Vertical") >= 0) // Realiza o pulo automático da função acima
        {
            Jump(); // Execute o pulo
        } // Realiza o pulo automático da função acima

        if (Input.GetButtonDown("Jump") && (p_collision.onGround || p_collision.onGroundCoyote) && jumpCooldown <= 0 && Input.GetAxis("Vertical") >= 0) // Checa se o jogador pode pular
        {
            Jump(); // Executa o pulo
            jumpCooldown = 0.5f; // Tempo que leva para o jogador realizar outro pulo
        } // Pulo tradicional

        if (jumpCooldown > 0)
        {
            jumpCooldown = jumpCooldown - Time.deltaTime;
        } // Reseta o cooldown de pulo


        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerAnim.SetBool("IsSlow", true);
            currentMoveSpeed = movementSpeed / 3;
        }
        else
        {
            playerAnim.SetBool("IsSlow", false);
            currentMoveSpeed = movementSpeed;
        }


    } // Controla a movimentação do jogador
    private void Jump()
    {
        playerAnim.Play("Bob_JumpStill"); // Toca a animação de pulo
        p_collision.onGroundCoyote = false; // Indica que o jogador não está mais no chão
        wantToJump = false; // Indica que o jogador não quer mais pular
        rb.velocity = new Vector2(rb.velocity.x, 0); // Mantem a velocidade horizontal do player
        rb.velocity += Vector2.up * jumpForce; // Joga o player para cima de acordo com a força do pulo
    } // Controla o pulo do jogador
    private void ShootMechanic()
    {
        // Função que controla tudo a respeito das mecânicas de tiro do jogador

        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos -= new Vector3(0.5f, 0.5f, 0.0f) * 1;

        if (Input.GetButtonDown("Fire1") && !isBusy && p_collision.onGround) // Checa se o jogador pode atirar
        {
            if (bulletcount < 2) // Caso o jogador ainda tenha munição
            {
                if (facingright == true)
                {
                    gun.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                } // Caso o jogador esteja olhando para a direita, mantem a rotação da bala
                if (facingleft == true)
                {
                    gun.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                } // Caso o jogador esteja olhando para a esquerda, flipe a rotação da bala
                if (facingright && mousePos.x > 0)
                {
                    GameObject instance = Instantiate(bullet_normal, gun.transform.position, gun.transform.rotation);
                    instance.GetComponent<Gunshot>().speed = -10;
                }
                else if (facingleft && mousePos.x < 0)
                {
                    Instantiate(bullet_normal, gun.transform.position, gun.transform.rotation); // Cria uma bala do tipo NORMAL
                }
                else
                {
                    return;
                }
                bulletcount++; // Indica que o jogador gastou 1 bala
                FreezeMovement(); // Deixa o jogador parado
                isBusy = true; // Indica que o jogador está atirando
                Invoke("StopBeingBusy", 0.5f); // Jogador volta ao normal depois de 'x' segundos
                return;
            } // Caso o jogador atire e tenha munição
            if (bulletcount >= 2)
            {
                CancelInvoke("StopBeingBusy");
                FreezeMovement(); // Congela o movimento do jogador
                reload.Play();
                playerAnim.Play("Bob_Reload");
                GameEvents.current.MakeBigSound(gameObject);
                isBusy = true; // Indica que o jogador está ocupado recarregando
                Invoke("Reload", tempoReload); // Recarrega as balas depois de 'x' segundos
                                               // ou toque sons/animações do player sem munição aqui 
            } // Caso o jogador tenta atirar sem munição

        } // Atirar
        if (Input.GetKeyDown(KeyCode.R) && !isBusy && p_collision.onGround && bulletcount != 0) // Checa se o jogador pode recarregar
        {
            FreezeMovement(); // Congela o movimento do jogador
            playerAnim.Play("Bob_Reload");
            isBusy = true; // Indica que o jogador está ocupado recarregando
            Invoke("Reload", tempoReload); // Recarrega as balas depois de 'x' segundos
        } // Recarregar

        if (Input.GetKeyDown(KeyCode.E) && !isBusy && p_collision.onGround && DestroyBackLight.backLight == false) // Checa se a lampada esta quebrada
        {
            FreezeMovement(); // Congela o movimento do jogador
                              /////playerAnim.Play("Bob_Reload");//animação para conserto da lampada
            isBusy = true; // Indica que o jogador está ocupado 
            Invoke("Lamp", tempoReload); // Conserta a lampada depois de 'x' segundos

        }
    }
    private void Lamp()
    {
        isBusy = false; // Libera o movimento
        DestroyBackLight.backLight = true; // Retorna a variavel estatica
        BackLight.SetActive(true); // Ativa a lampada

    } 
    //Funções secundárias
    private void GroundCheck()
    {
        // Essa função realiza ações caso o jogador esteja ou não com os pés no chão
        if (p_collision.onGround) // Caso esteja no chão
        {
            playerAnim.SetBool("OnGround", true); // Animação
        }
        else                     // Caso não esteja no chão
        {
            playerAnim.SetBool("OnGround", false); // Animação
        }
    } // Checa se o jogador está no chão
    private void MoveCheck()
    {
        // Função que realiza ações caso o jogador esteja em movimento
        if (isMoving) // Caso esteja se movimentando:
        {
            playerAnim.SetBool("IsMoving", true); // Animação
        }
        else         // Caso não esteja se movimentando:
        {
            playerAnim.SetBool("IsMoving", false); // Animação
        }
    } // Checa se o jogador está se movimentando
    public void Reload()
    {
        // == ATENÇÃO ==
        // ESTA FUNÇÃO EXECUTA AÇÕES NO FINAL DO RECARREGAMENTO
        // PARA AS ANIMAÇÕES, SONS E OUTROS RECURSOS QUE SÃO EXECUTADOS ENQUANTO O JOGADOR RECARREGA
        // ACESSE A FUNÇÃO ShootMechanic()

        isBusy = false; // Indica que o jogador não está mais ocupado
        bulletcount = 0; // Reseta a quantidade de balas que o jogador atirou
    } // Controla o que acontece no final do reload
    public void StopBeingBusy()
    {
        isBusy = false; // Indica que o jogador não está mais ocupado
    }
    public void ResetWantToJump()
    {
        wantToJump = false;
    }
    private void FreezeMovement()
    {
        x = 0;
        y = 0;
        isMoving = false;
        rb.velocity = Vector3.zero;
    }
    private void FlipSprite()
    {
        if (!isBusy) // Caso o jogador não esteja ocupado
        {
            if (Input.GetAxis("Horizontal") <= -0.7f && isMoving) // Caso o jogador esteja se movimentando para a esquerda
            {
                FaceLeft();
                return;
            }
            if (Input.GetAxisRaw("Horizontal") >= 0.7f && isMoving) // Caso o jogador esteja se movimentando para a direita
            {
                FaceRight();
                return;
            }

            if (mousePos.x > 0)
            {
                playersprite.transform.localScale = new Vector3(-spriteScale.x, spriteScale.y, spriteScale.z); // Inverte o valor 'x' da escala do sprite
                facingright = true;
                facingleft = false;
                return;
            }
            if (mousePos.x < 0)
            {
                playersprite.transform.localScale = new Vector3(spriteScale.x, spriteScale.y, spriteScale.z); // Mantem o valor positivo do 'x' da escala do sprite
                facingleft = true;
                facingright = false;
                return;
            }



        }
    }

    public void Freio()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }

    public void FaceLeft()
    {
        playersprite.transform.localScale = new Vector3(spriteScale.x, spriteScale.y, spriteScale.z); // Mantem o valor positivo do 'x' da escala do sprite
        facingleft = true;
        facingright = false;
    }
    public void FaceRight()
    {
        playersprite.transform.localScale = new Vector3(-spriteScale.x, spriteScale.y, spriteScale.z); // Inverte o valor 'x' da escala do sprite
        facingright = true;
        facingleft = false;
    }
    private void MovementSmoothing()
    {
        if (!p_collision.onGround)
            movement_acceleration = Mathf.SmoothDamp(movement_acceleration, 0, ref yVelocity, 0.1f);
        else
            movement_acceleration = 0.04f;
    } // Controla a aceleração
    public void UnlockFlash()
    {
        unlockedFlash = true;
    }

    private void FlashMechanic()
    {
        if (flashTime <= 0 && Input.GetKeyDown(KeyCode.F))
        {
            flash.SetActive(true);
            hud_noFlash.Play("hud_icon_noflash");
            flashTime = flashCooldown;
        }
        else
        {
            flashTime -= Time.deltaTime;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "FloorHazard") // Detecta se o player esta preso no inimigo do chão
        {
            FreezeMovement();
            jumpCooldown = 0.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Enemy" && hits > 0)// Detecta se o player tomou dano e não está morto
        {
            if (hitCooldown <= 0)
            {
                GetHit();
                lastEnemyToHit = other.gameObject;
                var magnitude = 400;

                var force = transform.position - other.transform.position;

                force.Normalize();
                GetComponent<Rigidbody2D>().AddForce(-force * magnitude);
            }

        }

        if (other.gameObject.tag == "FloorHazard")
        {
            //Instantiate(DMG, new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), Quaternion.identity);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Damage" && hits > 0)
        {
            if(hitCooldown <= 0)
            {
                GetHit();
                lastEnemyToHit = collision.gameObject.transform.parent.gameObject;
            }
        }
    }

    void ResetHitCooldown()
    {
        hitCooldown = 0;
    }

    public void GetHit()
    {
        hud_dmg.Play("hud_damage"); //animação da hud
        hits -= 1;
        hitCooldown = invulnerabilityFrame;
        Invoke("ResetHitCooldown", invulnerabilityFrame);
        Instantiate(DMG, new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), Quaternion.identity);
        //Som do player tomando dano ta no gameobject que é instanciado acima (bruh)
    }

    void DeathCheck()
    {
        if (hits <= 0 && !died)
        {
            rb.isKinematic = true;
            died = true;
            Freio();
            Debug.Log(lastEnemyToHit.name);
            PaintLastEnemy();
            playerAnim.Play("Death_verme");
        }
    }

    void PaintLastEnemy()
    {
        if(lastEnemyToHit.name == "fotoverme_charging")
        {
            lastEnemyToHit.transform.parent.transform.parent.GetComponent<Fotoverme>().Congela();
            lastEnemyToHit.GetComponentInChildren<SpriteRenderer>().material = red_material;
            lastEnemyToHit.GetComponentInChildren<SpriteRenderer>().gameObject.layer = LayerMask.NameToLayer("DeathSprite");
        }
        if(lastEnemyToHit.name == "carapaça")
        {
            lastEnemyToHit.GetComponentInChildren<SpriteRenderer>().sortingLayerID = 0;
            lastEnemyToHit.GetComponentInChildren<SpriteRenderer>().material = red_material;
            lastEnemyToHit.GetComponentInChildren<SpriteRenderer>().gameObject.layer = LayerMask.NameToLayer("DeathSprite");
        }
    }

}
