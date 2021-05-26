using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

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

    float m_dashTime;
    float m_timer = 0.0f;
    float m_invincibleFrame = 0.0f;
    float x = 0.0f;

    int m_hitPoint = 0;

    bool m_death = false;
    bool m_damage = false;
    bool m_direction = true;
    bool m_thrust = false;
    bool m_dash = false;
    bool m_megajump = false;
    bool m_bump = false;



    Rigidbody m_rigidbody;
    SpriteRenderer m_spriteRenderer;
    Animator m_animator;
    CapsuleCollider2D m_collider;
    PolygonCollider2D m_polyCollider;
    inputManager m_inputManager;
    HealthManager  m_healthManager;
    
    void Awake()
    {
        m_inputManager = GameObject.Find("inputManager").GetComponent<inputManager>();

        m_rigidbody = GetComponent<Rigidbody>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_collider = GetComponent<CapsuleCollider2D>();
        m_polyCollider = GetComponent<PolygonCollider2D>();
        m_healthManager = GetComponent<HealthManager>();
    }


    void Update()
    {
        // Raycast Jump
        /*RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, layerMask);

        if (hit)
        {
            m_isGrounded = true;
        }
        else
        {
            m_isGrounded = false;
        }*/

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

            if (m_inputManager.walkLeft && m_inputManager.walkRight || m_inputManager.walkUp && m_inputManager.walkDown)
            {
                m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }

            else if (m_inputManager.walkLeft || m_inputManager.walkRight || m_inputManager.walkUp || m_inputManager.walkDown)
            {
                //Left
                if (m_inputManager.walkLeft)
                {
                    m_direction = false;
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    m_rigidbody.velocity = new Vector3(-speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                }

                //Right
                if (m_inputManager.walkRight)
                {
                    m_direction = true;
                    transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
                    m_rigidbody.velocity = new Vector3(speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                }

                //Up
                if (m_inputManager.walkUp)
                {
                    m_direction = false;
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, -speed * Time.fixedDeltaTime);
                }

                //Down
                if (m_inputManager.walkDown)
                {
                    m_direction = true;
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, speed * Time.fixedDeltaTime);
                }
            }
            else
            {
                m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, 0.0f);    
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

            //Dash
            if (m_dash && m_dashTime <= 0.0f && !m_direction)
            {
                m_dash = false;
                m_dashTime = startDashTime;
                m_rigidbody.velocity = new Vector2(-dashSpeed * Time.fixedDeltaTime, m_rigidbody.velocity.y);
                
            }

            if (m_dash && m_dashTime <= 0.0f && m_direction)
            {
                m_dash = false;
                m_dashTime = startDashTime;
                m_rigidbody.velocity = new Vector2(dashSpeed * Time.fixedDeltaTime, m_rigidbody.velocity.y);
                
            }
            else
            {
                m_dashTime -= Time.deltaTime;
            }

            //Thrust
            if (m_thrust)
            {
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, thrust);
                m_thrust = false;
            }

            //Bump
            if (m_bump)
            {
                m_rigidbody.velocity = new Vector2(-bumpX, bumpY);
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
            if (m_rigidbody.velocity.y == 0.0f && !m_damage)
            {

                if (m_rigidbody.velocity.x == 0.0f && !m_damage)
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

                if (m_rigidbody.velocity.y > 260.0f && !m_damage)
                {
                    m_animator.Play("MegaJump");
                    m_megajump = true;

                }

                else if (m_rigidbody.velocity.y > 0.0f && !m_megajump && !m_damage)
                {
                    m_animator.Play("Jump");
                }
                
                else if (m_rigidbody.velocity.y < 0.0f && !m_damage)
                {
                    m_animator.Play("Fall");
                    m_megajump = false;
                }
            }
        }
        else
        {
            m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, 0.0f);

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
        if (collision.gameObject.tag == "Enemy")
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
