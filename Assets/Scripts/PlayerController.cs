using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class PlayerController : MonoBehaviour
{
    public static bool xitadasso;
    public GameObject cheatIndicator;
    public GameObject flashObject;

    // Variaveis ajustaveis
    public float movementSpeed = 10f;
    public float movement_acceleration = 0.2f;
    public float movement_decceleration = 0.05f;
    public float jumpForce = 2f;
    public float tempoReload = 1.5f; // Tempo em segundos que leva para recarregar
    public float flashCooldown = 2;
    public bool unlockedBeacon;
    public bool unlockedFlash;
    public bool unlockedJetpack;
    public bool unlockedKnife;

    // Variaveis essenciais
    float currentMoveSpeed;
    bool isSlow;
    bool reloading;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool wantToJump;
    [HideInInspector] public bool onRope;
    private float invulnerabilityFrame = 0.5f;
    private float hitCooldown;
    private float knifeCooldown;
    private int knifeAnimIndex;
    private float yVelocity = 0.0f;
    private float jumpCooldown;
    [HideInInspector] public bool isBusy;
    private float storedAccelerationValue;
    private int bulletcount;
    private Vector3 spriteScale;
    private Vector3 mousePos;
    [SerializeField] private float x = 0;
    [SerializeField] private float y = 0;
    float chargedShotCooldown;
    private float flashTime;
    private bool died;
    bool wasCharging;
    bool isCharging;
    bool playerPaused;
    bool shotReady;
    GameObject lastEnemyToHit;
    public float boostJumpValue;
    float boostIndex;
    bool boostLocked;
    bool chargingJump;
    bool jetpackCharged;
    // Variaveis estaticas
    public static bool facingleft;
    public static bool facingright;
    public static int hits = 2;
    public static bool OnMovement = true;

    // Referencias privadas
    private Animator jetpackAnim;
    private EnergyCollector energy;
    private PlayerCollision p_collision;
    private Rigidbody2D rb;
    private SoundPlayer soundplayer;
    private AudioSource reload;

    // Referencias publicas
    public GameObject jetpacksprite;
    public GameObject jetpackgauge;
    public ParticleSystem[] jetpack_FX;
    public AudioClip[] jetpack_SFX;
    public Light2D jetpackFX_light;
    public AudioClip[] knifesound;
    public AudioSource audioknife;
    public LayerMask layerRaycast;
    public GameObject playersprite;
    public ParticleSystem FX_tiroCarregado;
    public GameObject FX_chargedLight;
    public ParticleSystem FX_fizzle;
    public Animator playerAnim;
    public GameObject gun;
    public GameObject bullet_normal;
    public GameObject BackLight;
    public AudioClip LampDone;
    public GameObject DMG;
    public GameObject flash;
    public ParticleSystem feedbackShieldFX;
    public Animator hud_dmg;
    public Animator hud_shieldDmg;
    public Animator hud_noFlash;
    public Material red_material;
    public GameObject ReloadIcon;


    void Awake()
    {
       
        //LoadPlayer();
    }

    private void Start()
    {
        if (FlashAnimation.flashUnlocked)
        {
            flashObject.SetActive(true);
            unlockedFlash = true;
        }
        if (xitadasso) cheatIndicator.SetActive(true);
        GameEvents.current.onUnPausePlayer += UnPausePlayer;
        GameEvents.current.onPausePlayer += PausePlayer;
        hits = 2;
        Initialize();
    }

    public void PausePlayer()
    {
        playerPaused = true;
    }

    public void UnPausePlayer()
    {
        playerPaused = false;
    }

    private void Update()
    {
        if (!chargingJump)
        {
            jetpackAnim.speed = Mathf.MoveTowards(jetpackAnim.speed, 0, 1 * Time.deltaTime);
            jetpackgauge.transform.localScale = new Vector3(1, Mathf.MoveTowards(jetpackgauge.transform.localScale.y, 0, 2 * Time.deltaTime), 1);
            jetpackFX_light.intensity = Mathf.MoveTowards(jetpackFX_light.intensity, 0, 2 * Time.deltaTime);
        }

        if (jetpackgauge.transform.localScale.y < 1)
            jetpackgauge.GetComponent<SpriteRenderer>().material.SetColor("_MainColor", Color.cyan);

        if (jetpackgauge.transform.localScale.y > 1)
            jetpackgauge.GetComponent<SpriteRenderer>().material.SetColor("_MainColor", Color.yellow);

        if (jetpackgauge.transform.localScale.y > 1.7)
            jetpackgauge.GetComponent<SpriteRenderer>().material.SetColor("_MainColor", Color.red);


        if (Input.GetKeyDown(KeyCode.F1))
        {
            Xiter();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            unlockedFlash = true;
            flashObject.SetActive(true);
        }

        if (!died)
        {
            if (!playerPaused)
            {
                //ShootMechanic();
                ChargedShootMechanic();
                MecanicaRecarregar();
                if (!isBusy)
                {
                    if (!isCharging) // se o jogador nao estiver carregando o tiro
                    {
                        if (!unlockedJetpack)
                            JumpMechanic();
                        else BoostedJump();
                    }
                }

                if (unlockedFlash)
                {
                    FlashMechanic();

                }
                if (unlockedKnife)
                {
                    KnifeMechanic();
                }

            }
        }



    }

    private void FixedUpdate()
    {

        if (isBusy)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }

        DeathCheck();

        if (!died)
        {
            if (!playerPaused)
            {
                GroundCheck();
                MoveCheck();
                if (!isBusy)
                {
                    FlipSprite();
                    //MovementSmoothing();
                    MovementMechanic();
                }
            }
        }

    }

    //Funções principais:
    private void Initialize()
    {
        currentMoveSpeed = movementSpeed;
        energy = GetComponentInChildren<EnergyCollector>();
        spriteScale = playersprite.transform.localScale;
        soundplayer = GetComponent<SoundPlayer>();
        storedAccelerationValue = movement_acceleration;
        storedAccelerationValue = movement_acceleration;
        p_collision = GetComponent<PlayerCollision>();
        rb = GetComponent<Rigidbody2D>();
        reload = gun.GetComponent<AudioSource>();
        jetpackAnim = jetpacksprite.GetComponent<Animator>();

        if (GameLoad.playerHasDiedOnce)
        {
            hits = 3;
        }
        else
        {
            hits = 2;
        }

    }

    void Xiter()
    {
        xitadasso = !xitadasso;
        if (xitadasso) cheatIndicator.SetActive(true); else cheatIndicator.SetActive(false);
    }

    private void DoubleJump()
    {
        //será
    }

    private void BoostedJump()
    {

        if (Input.GetButtonDown("Jump") && (p_collision.onGround || p_collision.onGroundCoyote) && jumpCooldown <= 0 && Input.GetAxis("Vertical") >= 0)
        {
            boostLocked = true;
            boostIndex = 0;
            boostJumpValue = 0;

            if (!isSlow)
            {
                Jump(1);
                jumpCooldown = 0.5f;
            }
        }

        if (Input.GetButton("Jump") && (p_collision.onGround || p_collision.onGroundCoyote) && jumpCooldown <= 0 && Input.GetAxis("Vertical") >= 0 && boostLocked)
        {
            //jogador está carregando o pulo
            if(!chargingJump)
                soundplayer.Play(jetpack_SFX[0]); //som de carregando
            chargingJump = true;
            boostIndex += Time.deltaTime;
            boostJumpValue = Mathf.Abs(1 * Mathf.Sin(0.5f * boostIndex));

            if (boostJumpValue > 0.5f && !jetpackCharged)
            {
                jetpackCharged = true;
                jetpack_FX[3].Play();
            }

            jetpackAnim.speed = Mathf.Lerp(0, 1, boostJumpValue);
            jetpackgauge.transform.localScale = new Vector3(1, Mathf.MoveTowards(jetpackgauge.transform.localScale.y, Mathf.Lerp(0, 2, boostJumpValue), 1 * Time.deltaTime), 1);
        }

        if (Input.GetButtonUp("Jump") && (p_collision.onGround || p_collision.onGroundCoyote) && jumpCooldown <= 0 && Input.GetAxis("Vertical") >= 0)
        {
            //jogador solta o botão de carregar o pulo
            jetpackCharged = false;
            StopJetpackCharged();
            CancelInvoke("StopJetpackFlame");
            Invoke("StopJetpackFlame", boostJumpValue);
            boostLocked = false;
            chargingJump = false;
            if (boostJumpValue < 0.2f)
            {
                //pulo cancelado
                return;
            }
            else
            {
                //jogador pula
                soundplayer.Stop();
                soundplayer.PlayOneShot(jetpack_SFX[1]);//som de boost
                jetpackFX_light.intensity = Mathf.Lerp(1, 4, boostJumpValue);
                jetpack_FX[0].Play(); //chama
                jetpack_FX[1].Play(); //fumaça
                
                if(boostJumpValue > 0.7f)
                    jetpack_FX[2].Play(); //burst

                Jump(1 + (boostJumpValue * 1.3f));
                jumpCooldown = 0.5f;

                GetComponent<JumpModifier>().rocketFalling = true;
                Invoke("StopJump", 5);

            }

            boostJumpValue = 0;

        }

        if (jumpCooldown > 0)
        {
            jumpCooldown = jumpCooldown - Time.deltaTime;
        } // Reseta o cooldown de pulo

    }

    void StopJump()
    {
        GetComponent<JumpModifier>().rocketFalling = false;
    }

    void StopJetpackCharged()
    {
        jetpack_FX[3].Stop();
    }

    void StopJetpackFlame()
    {
        jetpack_FX[0].Stop();
        jetpack_FX[1].Stop();
    }

    void KnifeMechanic()
    {
        if (knifeCooldown > 0) knifeCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.V) && knifeCooldown <= 0)
        {
            CancelInvoke("ResetKnifeAnimation");
            if (knifeAnimIndex == 0)
            {
                audioknife.clip = knifesound[0];
                audioknife.Play();
                playerAnim.Play("Bob_knife1");
                knifeAnimIndex++;
            }
            else
            {
                audioknife.clip = knifesound[1];
                audioknife.Play();
                playerAnim.Play("Bob_knife2");
                knifeAnimIndex = 0;
            }
            knifeCooldown = 0.5f;
            Invoke("ResetKnifeAnimation", 2f);
        }
    }

    void ResetKnifeAnimation()
    {
        knifeAnimIndex = 0;
    }



    private void JumpMechanic()
    {   


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
            Jump(1); // Execute o pulo
        } // Realiza o pulo automático da função acima

        if (Input.GetButtonDown("Jump") && (p_collision.onGround || p_collision.onGroundCoyote) && jumpCooldown <= 0 && Input.GetAxis("Vertical") >= 0) // Checa se o jogador pode pular
        {
            Jump(1); // Executa o pulo
            jumpCooldown = 0.5f; // Tempo que leva para o jogador realizar outro pulo
        } // Pulo tradicional




        if (jumpCooldown > 0)
        {
            jumpCooldown = jumpCooldown - Time.deltaTime;
        } // Reseta o cooldown de pulo
    }

    private void MovementMechanic()
    {
        // Essa função controla tudo a respeito da movimentação do jogador

        if (Input.GetAxisRaw("Horizontal") != 0) // Checa se o jogador está apertando para ir pros lados
        {
            isMoving = true; // Diz que o jogador está tentando se movimentar

            if (!p_collision.onWall)
            {
                x = Mathf.SmoothDamp(x, Input.GetAxis("Horizontal"), ref yVelocity, movement_acceleration); // Registra o valor da movimentação horizontal de acordo com a aceleração
            }
            else
            {
                if (p_collision.onWallLeft)
                {
                    if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        x = Mathf.SmoothDamp(x, Input.GetAxis("Horizontal"), ref yVelocity, movement_acceleration);
                    }
                    else
                    {
                        x = 0;
                    }
                }
                else if (p_collision.onWallRight)
                {
                    if (Input.GetAxisRaw("Horizontal") < 0)
                    {
                        x = Mathf.SmoothDamp(x, Input.GetAxis("Horizontal"), ref yVelocity, movement_acceleration);
                    }
                    else
                    {
                        x = 0;
                    }
                }
            }
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

        if(Input.GetAxisRaw("Horizontal") != 0)
            rb.velocity = new Vector2(dir.x * currentMoveSpeed, rb.velocity.y); // Movimenta o jogador para os lados de acordo com o vetor 'dir' e a velocidade variavel 


        if ((Input.GetKey(KeyCode.LeftShift) && p_collision.onGround) || reloading || isCharging)
        {
            playerAnim.SetBool("IsSlow", true);
            currentMoveSpeed = movementSpeed / 3;
            isSlow = true;
        }
        else
        {
            playerAnim.SetBool("IsSlow", false);
            currentMoveSpeed = movementSpeed;
            isSlow = false;
        }

        if (OnMovement == false)
        {
            FreezeMovement();
        }
        //else
            //isMoving = true;

    } // Controla a movimentação do jogador
    private void Jump(float boost)
    {
        boostLocked = false;
        if (!isCharging)
        {
            playerAnim.Play("Bob_JumpStill"); // Toca a animação de pulo
            p_collision.onGroundCoyote = false; // Indica que o jogador não está mais no chão
            wantToJump = false; // Indica que o jogador não quer mais pular
            rb.velocity = new Vector2(rb.velocity.x, 0); // Mantem a velocidade horizontal do player
            rb.velocity += Vector2.up * jumpForce * boost; // Joga o player para cima de acordo com a força do pulo
        }
    } // Controla o pulo do jogador
    private void ShootMechanic()
    {
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos -= new Vector3(0.5f, 0.5f, 0.0f) * 1;

        // Função que controla tudo a respeito das mecânicas de tiro do jogador

        if (Input.GetButtonDown("Fire1") && !isBusy && p_collision.onGround && !reloading) // Checa se o jogador pode atirar
        {
            Debug.Log("Bruh");
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
                    Debug.Log("Bruh2");
                }
                else if (facingleft && mousePos.x < 0)
                {
                    Debug.Log("Bruh2");
                    Instantiate(bullet_normal, gun.transform.position, gun.transform.rotation); // Cria uma bala do tipo NORMAL
                }
                else
                {
                    //return;
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
                //FreezeMovement(); // Congela o movimento do jogador
                reloading = true;
                
                ReloadIcon.SetActive(true);
                //playerAnim.Play("Bob_Reload");
                GameEvents.current.MakeBigSound(gameObject);
                //isBusy = true; // Indica que o jogador está ocupado recarregando
                Reload(); // Recarrega as balas depois de 'x' segundos
                                               // ou toque sons/animações do player sem munição aqui 
            } // Caso o jogador tenta atirar sem munição

        } // Atirar old

        /*
        if (Input.GetKeyDown(KeyCode.E) && !isBusy && p_collision.onGround && DestroyBackLight.backLight == false) // Checa se a lampada esta quebrada
        {
            FreezeMovement(); // Congela o movimento do jogador
                              /////playerAnim.Play("Bob_Reload");//animação para conserto da lampada
            isBusy = true; // Indica que o jogador está ocupado 
            Invoke("Lamp", tempoReload); // Conserta a lampada depois de 'x' segundos

        }
        */
    }
    private void ChargedShootMechanic()
    {

        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos -= new Vector3(0.5f, 0.5f, 0.0f) * 1;

        if (chargedShotCooldown > 0) chargedShotCooldown -= Time.deltaTime;


        if (Input.GetButton("Fire1") && !isBusy && p_collision.onGround && !isCharging && chargedShotCooldown <= 0)
        {
            isCharging = true;
            wasCharging = true;
            FX_chargedLight.SetActive(true);
            chargedShotCooldown = 1;
            if(bulletcount >= 2)
            {
                FX_fizzle.Play();
                return;
            }
            FX_tiroCarregado.gameObject.SetActive(true);
            FX_tiroCarregado.Play();
            Invoke("ShotLoaded", 1f);


        }
        if (Input.GetButtonUp("Fire1") && wasCharging)
        {
            if (!isBusy || !p_collision.onGround)
            {
                CancelInvoke("ShotLoaded");
                FX_chargedLight.SetActive(false);
                isCharging = false;
                wasCharging = false;
                FX_tiroCarregado.Stop();
                FX_tiroCarregado.gameObject.SetActive(false);
                if (shotReady)
                {
                    shotReady = false;
                    chargedShotCooldown = 1;
                    ShootNormal();
                    //bulletcount++;
                }
            }
            else
            {
                FX_fizzle.Play();
            }
        }
    }

    void MecanicaRecarregar()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isBusy && p_collision.onGround && bulletcount != 0) // Checa se o jogador pode recarregar
        {
            Reload();
        } // Recarregar
    }

    public void Reload()
    {
        //FreezeMovement(); // Congela o movimento do jogador
        reloading = true;
        playerAnim.Play("Bob_Reload");
        reload.Play();//som
        ReloadIcon.SetActive(true);
        //isBusy = true; // Indica que o jogador está ocupado recarregando
        Invoke("ReloadFinish", tempoReload); // Recarrega as balas depois de 'x' segundos
    }

    void ShootNormal()
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
    }

    public void HeroLanding()
    {
        Freio();
        playerAnim.Play("Bob_herolanding");
        isBusy = true;
    }

    public void PutJetpack()
    {
        jetpacksprite.SetActive(true);
        unlockedJetpack = true;
    }
    void ShotLoaded()
    {
        shotReady = true;
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
    public void ReloadFinish()
    {
        // == ATENÇÃO ==
        // ESTA FUNÇÃO EXECUTA AÇÕES NO FINAL DO RECARREGAMENTO
        // PARA AS ANIMAÇÕES, SONS E OUTROS RECURSOS QUE SÃO EXECUTADOS ENQUANTO O JOGADOR RECARREGA
        // ACESSE A FUNÇÃO ShootMechanic()
        reloading = false;
        isBusy = false; // Indica que o jogador não está mais ocupado
        bulletcount = 0; // Reseta a quantidade de balas que o jogador atirou
        ReloadIcon.SetActive(false);// Desliga o ícone de reload
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

            if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 0;
                Ray ray;
                ray = Camera.main.ScreenPointToRay(mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 99991, layerRaycast, QueryTriggerInteraction.Collide))
                {
                    if(hit.point.x - transform.position.x > 0)
                    {
                        //right
                        playersprite.transform.localScale = new Vector3(-spriteScale.x, spriteScale.y, spriteScale.z); // Inverte o valor 'x' da escala do sprite
                        facingright = true;
                        facingleft = false;
                        return;
                    }
                    if (hit.point.x - transform.position.x < 0)
                    {
                        //left
                        playersprite.transform.localScale = new Vector3(spriteScale.x, spriteScale.y, spriteScale.z); // Mantem o valor positivo do 'x' da escala do sprite
                        facingleft = true;
                        facingright = false;
                        return;
                    }

                }



                /* OLD
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
                */
            }


        }
    }

    public void Freio()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }

    public void InteractPause(float time)
    {
        CancelInvoke("BackToNormal");
        FreezeMovement();
        isBusy = true;
        Invoke("BackToNormal", time);

    }
    void BackToNormal()
    {
        isBusy = false;
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
       /* if (other.gameObject.tag == "SavePoint" && Input.GetKeyDown(KeyCode.S))
            SavePlayer();*/
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

        if (collision.gameObject.tag == "Damage" && hits > 0)
        {
            if(hitCooldown <= 0)
            {
                GetHit();
                lastEnemyToHit = collision.gameObject.transform.parent.gameObject;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SavePoint" && Input.GetKeyDown(KeyCode.S))
        {
            SavePlayer();
            Debug.Log("Saved");
        }
    }

    void ResetHitCooldown()
    {
        hitCooldown = 0;
        playerAnim.SetBool("Damage", false);
    }

    public void GetHit()
    {
        if (!xitadasso && hitCooldown == 0)
        {
            if (energy.shieldcharge >= 20)
            {
                hitCooldown = invulnerabilityFrame;
                Invoke("ResetHitCooldown", invulnerabilityFrame);
                feedbackShieldFX.Play();
                hud_shieldDmg.Play("hud_damage");
                energy.GetHit(20);
            }
            else
            {
                hud_dmg.Play("hud_damage"); //animação da hud
                playerAnim.SetBool("Damage", true);
                hits -= 1;
                hitCooldown = invulnerabilityFrame;
                Invoke("ResetHitCooldown", invulnerabilityFrame);
                Instantiate(DMG, new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), Quaternion.identity);
            }
            //Som do player tomando dano ta no gameobject que é instanciado acima (bruh)
        }
    }

    public void GetHitSpecific(GameObject hit)
    {
        if (!xitadasso && hitCooldown == 0)
        {
            if (hit != null)
            {
                lastEnemyToHit = hit;
            }

            if (energy.shieldcharge >= 20)
            {
                hitCooldown = invulnerabilityFrame;
                Invoke("ResetHitCooldown", invulnerabilityFrame);
                feedbackShieldFX.Play();
                hud_shieldDmg.Play("hud_damage");
                energy.GetHit(20);
            }
            else
            {
                hud_dmg.Play("hud_damage"); //animação da hud
                playerAnim.SetBool("Damage", true);
                hits -= 1;
                hitCooldown = invulnerabilityFrame;
                Invoke("ResetHitCooldown", invulnerabilityFrame);
                Instantiate(DMG, new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), Quaternion.identity);
            }
        }
    }

    void DeathCheck()
    {
        if (hits <= 0 && !died)
        {
            rb.isKinematic = true;
            died = true;
            GameLoad.playerHasDiedOnce = true;
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
        if(lastEnemyToHit.name == "ENEMY_Charger")
        {
            lastEnemyToHit.GetComponentInChildren<Charger>().Congela();
            foreach (SpriteRenderer rend in lastEnemyToHit.GetComponentsInChildren<SpriteRenderer>()) 
            {
                rend.sortingLayerID = 0;
                rend.material = red_material;
                rend.gameObject.layer = LayerMask.NameToLayer("DeathSprite");
            }
        }
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        SaveRoom data = SaveSystem.LoadPlayer();

        hits = data.health;
        unlockedBeacon = data.beacon;
        unlockedFlash = data.flash;
        unlockedJetpack = data.jetpack;
        unlockedKnife = data.knife;

        ReatorOnEnable.ReatorOn = data.reator;
        ExecuteFlashCutscene.FlashLog = data.flash;

        GameController.currentScrap = data.scrap;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        OnEnableJo.TriggerJoshOn = data.Josh;
        OnEnableMing.TriggerMingOn = data.Ming;

        if (data.Ming == true)
            LogUnlocker.current.UnlockLog(2);
        if (data.Josh == true)
            LogUnlocker.current.UnlockLog(1);

    }
}
