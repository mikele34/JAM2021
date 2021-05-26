using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameObject bullet;
    public GameObject Sbullet;

    [Header("Movement")]
    public float speed = 300.0f;
    public float jumpForce = 300.0f;
    public float dashSpeed = 350.0f;
    public float startDashTime = 0.0f;
    
    [Header("PhysicsMovement")]
    public float thrust = 400.0f;
    public float bumpY = 100.0f;
    public float bumpX = 100.0f;
    
    [Header("Shooting")]
    public float shotRatio = 0.2f;
    public float SshotRatio = 2.0f;

    float m_dashTime;
    float m_timer = 0.0f;
    float m_shootTimer = 0.0f;
    float m_SshootTimer = 0.0f;
    float m_invincibleFrame = 0.0f;
    float x = 0.0f;

    int layerMask = 1 << 6;
    int m_hitPoint = 0;

    bool m_death = false;
    bool m_damage = false;
    bool m_direction = true;
    bool m_thrust = false;
    bool m_jump = false;
    bool m_dash = false;
    bool m_megajump = false;
    bool m_bump = false;
    bool m_isGrounded = false;
    bool m_Cshoot = false;


    Rigidbody2D m_rigidbody2D;
    SpriteRenderer m_spriteRenderer;
    Animator m_animator;
    CapsuleCollider2D m_collider;
    PolygonCollider2D m_polyCollider;
    inputManager m_inputManager;
    HealthManager  m_healthManager;
    
    void Awake()
    {
        m_inputManager = GameObject.Find("inputManager").GetComponent<inputManager>();

        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<CapsuleCollider2D>();
        m_polyCollider = GetComponent<PolygonCollider2D>();
        m_healthManager = GetComponent<HealthManager>();
    }


    void Update()
    {
        m_shootTimer += Time.deltaTime;

        //Shoot

        if(m_inputManager.shoot && m_shootTimer >= shotRatio && !m_Cshoot)
        {
            float m_spawnPoint_X = 0.0f;
            float m_spawnPoint_Y = 0.0f;
            
            if (m_direction)
            {

                if (Keyboard.current.sKey.isPressed)
                {
                    m_spawnPoint_X = 34.0f;
                    m_spawnPoint_Y = 28.0f;
                }
                else
                {
                    m_spawnPoint_X = 34.0f;
                    m_spawnPoint_Y = 40.0f;
                }
            }
            else
            {

                if (Keyboard.current.sKey.isPressed)
                {
                    m_spawnPoint_X = -34.0f;
                    m_spawnPoint_Y = 28.0f;
                }
                else
                {
                    m_spawnPoint_X = -34.0f;
                    m_spawnPoint_Y = 40.0f;
                }
            }            
                GameObject tmp_bullet = Instantiate(bullet, transform.position + new Vector3(m_spawnPoint_X, m_spawnPoint_Y, 0.0f), Quaternion.identity);
                tmp_bullet.GetComponent<BulletManager>().shoot(m_direction);
                m_shootTimer = 0.0f;
        }



        //Sshoot
        if (m_inputManager.Sshoot && m_Cshoot)
        {
            float m_spawnPoint_X = 0.0f;
            float m_spawnPoint_Y = 0.0f;

            if (m_direction)
            {

                if (Keyboard.current.sKey.isPressed)
                {
                    m_spawnPoint_X = 40.0f;
                    m_spawnPoint_Y = 28.0f;
                }
                else
                {
                    m_spawnPoint_X = 40.0f;
                    m_spawnPoint_Y = 40.0f;
                }
            }
            else
            {

                if (Keyboard.current.sKey.isPressed)
                {
                    m_spawnPoint_X = -40.0f;
                    m_spawnPoint_Y = 28.0f;
                }
                else
                {
                    m_spawnPoint_X = -40.0f;
                    m_spawnPoint_Y = 40.0f;
                }
            }           
        }

        // Raycast Jump

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, layerMask);

        if (hit)
        {
            m_isGrounded = true;
        }
        else
        {
            m_isGrounded = false;
        }

        //Dash

        if(m_inputManager.dash && !Keyboard.current.sKey.isPressed)
        {
            m_dash = true; 
        } 
    }



    void FixedUpdate()
    {
        if (!m_death)
        {

            if (m_inputManager.walkLeft && m_inputManager.walkRight)
            {
                m_rigidbody2D.velocity = new Vector2(0.0f, m_rigidbody2D.velocity.y);
            }

            else if (m_inputManager.walkLeft || m_inputManager.walkRight)
            {

                if (m_inputManager.walkLeft)
                {
                    m_direction = false;
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    m_rigidbody2D.velocity = new Vector2(-speed * Time.fixedDeltaTime, m_rigidbody2D.velocity.y);
                }

                if (m_inputManager.walkRight)
                {
                    m_direction = true;
                    transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
                    m_rigidbody2D.velocity = new Vector2(speed * Time.fixedDeltaTime, m_rigidbody2D.velocity.y);
                }
            }
            else
            {            
                    m_rigidbody2D.velocity = new Vector2(0.0f, m_rigidbody2D.velocity.y);    
            }
           
            // Pixel Perfect

            if (m_direction) // Right
            {
                x = Mathf.Ceil(transform.position.x);
            }
            else // Left
            {
                x = Mathf.Floor(transform.position.x);
            }
             transform.position = new Vector3(x, transform.position.y, transform.position.z);
           
            // Jump

            if (m_jump)
            {
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, jumpForce);
                m_jump = false;
            }

            //Dash

            if (m_dash && m_dashTime <= 0.0f && !m_direction)
            {
                m_dash = false;
                m_dashTime = startDashTime;
                m_rigidbody2D.velocity = new Vector2(-dashSpeed * Time.fixedDeltaTime, m_rigidbody2D.velocity.y);
                
            }

            if (m_dash && m_dashTime <= 0.0f && m_direction)
            {
                m_dash = false;
                m_dashTime = startDashTime;
                m_rigidbody2D.velocity = new Vector2(dashSpeed * Time.fixedDeltaTime, m_rigidbody2D.velocity.y);
                
            }
            else
            {
                m_dashTime -= Time.deltaTime;
            }

            //Thrust

            if (m_thrust)
            {
                m_rigidbody2D.velocity = new Vector2(m_rigidbody2D.velocity.x, thrust);
                m_thrust = false;
            }

            //Bump

            if (m_bump)
            {
                m_rigidbody2D.velocity = new Vector2(-bumpX, bumpY);
                m_bump = false;
            }

            //Damage

            if (m_damage)
            {               
                m_invincibleFrame += Time.deltaTime;

                if (m_invincibleFrame >= 1.0f)
                {
                    m_damage = false;
                    m_polyCollider.enabled = true;
                    m_invincibleFrame = 0.0f;
                }         
            }
            


            // Animations

            if (m_rigidbody2D.velocity.y == 0.0f && !m_damage)
            {

                if (m_rigidbody2D.velocity.x == 0.0f && !m_damage)
                {

                    if (m_inputManager.crouch)
                    {
                        m_animator.Play("Crouch");
                    }
                    else
                    {
                        m_animator.Play("Idle");
                    }

                }
                else
                {
                    m_animator.Play("Skip");
                }
            }
            else
            {

                if (m_rigidbody2D.velocity.y > 260.0f && !m_damage)
                {
                    m_animator.Play("MegaJump");
                    m_megajump = true;

                }

                else if (m_rigidbody2D.velocity.y > 0.0f && !m_megajump && !m_damage)
                {
                    m_animator.Play("Jump");
                }
                
                else if (m_rigidbody2D.velocity.y < 0.0f && !m_damage)
                {
                    m_animator.Play("Fall");
                    m_megajump = false;
                }
            }
        }
        else
        {
            m_rigidbody2D.velocity = new Vector2(0.0f, m_rigidbody2D.velocity.y);

            //Respawn
            m_timer += Time.deltaTime;
            
            if (m_timer >= 0.9f)
            {
                SceneManager.LoadScene("SampleScene");
                m_polyCollider.enabled = true;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * 1.0f);
    }

    //Death
    public void death()
    {
        m_polyCollider.enabled = false;
        m_animator.Play("Death");
        m_death = true;        
    }

    //Damage

    public void damage()
    {
        m_healthManager.Health--;
        m_animator.Play("Damage");
        m_damage = true;
        m_polyCollider.enabled = false;
    }

    //Megajump
    public void Thrust()
    {
        m_thrust = true;
    }

    //Bump
    public void Bump()
    {
        m_bump = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy")
        {
            m_hitPoint++;

            if( m_hitPoint < m_healthManager.numOfHearts)
            {
                damage();                              
            }

            if (m_hitPoint >= m_healthManager.numOfHearts)
            {
                death();
            }
        }
    }
}
