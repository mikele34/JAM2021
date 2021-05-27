using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public BarHealthManager healtBar;

    [Header("Movement")]
    public float walkspeed = 300.0f;
    public float runspeed = 300.0f;
    public float dashSpeed = 350.0f;
    
    
    [Header("PhysicsMovement")]
    public float thrust = 400.0f;
    public float bumpY = 100.0f;
    public float bumpX = 100.0f;
    
    [Header("Shooting")]
    public float shotRatio = 0.2f;

    float speed = 300.0f;
    float m_timer = 0.0f;
    float m_invincibleFrame = 0.0f;

    int m_hitPoint = 0; 

    bool m_death = false;
    bool m_damage = false;    
    bool m_thrust = false;
    bool m_bump = false;



    Rigidbody m_rigidbody;
    Animator m_animator;
    inputManager m_inputManager;
    HealthManager  m_healthManager;
    

    void Awake()
    {
        m_inputManager = GameObject.Find("inputManager").GetComponent<inputManager>();

        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_healthManager = GetComponent<HealthManager>();
        healtBar.SetMaxHealth(m_healthManager.numOfHearts);
    }


    void Update()
    {
        //Attack
        if (m_inputManager.attack)
        {
            //m_animator.Play("Attack");
            Debug.Log("Attack");
        }
    }



    void FixedUpdate()
    {
        if (!m_death)
        {

            if (m_inputManager.walkLeft && m_inputManager.walkRight || m_inputManager.walkUp && m_inputManager.walkDown)
            {
                m_animator.Play("Idle");
                m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            else if (m_inputManager.walkLeft || m_inputManager.walkRight || m_inputManager.walkUp || m_inputManager.walkDown)
            {
                //Right
                if (m_inputManager.walkRight)
                {
                    if (m_inputManager.run)
                    {
                        m_animator.Play("Run");
                        speed = runspeed;
                    }
                    else
                    {
                        m_animator.Play("Skip");
                        speed = walkspeed;
                    }
                    transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                    m_rigidbody.velocity = new Vector3(speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                }

                //Left
                if (m_inputManager.walkLeft)
                {
                    if (m_inputManager.run)
                    {
                        m_animator.Play("Run");
                        speed = runspeed;
                    }
                    else
                    {
                        m_animator.Play("Skip");
                        speed = walkspeed;
                    }
                    transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
                    m_rigidbody.velocity = new Vector3(-speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                }                

                //Up
                if (m_inputManager.walkUp)
                {
                    if (m_inputManager.run)
                    {
                        m_animator.Play("Run");
                        speed = runspeed;
                    }
                    else
                    {
                        m_animator.Play("Skip");
                        speed = walkspeed;
                    }
                    transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, speed * Time.fixedDeltaTime);
                }

                //Down
                if (m_inputManager.walkDown)
                {
                    if (m_inputManager.run)
                    {
                        m_animator.Play("Run");
                        speed = runspeed;
                    }
                    else
                    {
                        m_animator.Play("Skip");
                        speed = walkspeed;
                    }
                    transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                    m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, -speed * Time.fixedDeltaTime);
                }
            }
            else
            {
                m_animator.Play("Idle");
                m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, 0.0f);    
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
                    m_invincibleFrame = 0.0f;
                }         
            }
        }
        else
        {
            m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, 0.0f);

            //Respawn
            m_timer += Time.deltaTime;
            
            if (m_timer >= 20.9f)
            {
                SceneManager.LoadScene("SampleScene");
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
        m_animator.Play("Death");
        m_death = true;        
    }

    //Damage
    public void damage()
    {
        healtBar.SetHealth(m_healthManager.Health);
        m_healthManager.Health--;
        //m_animator.Play("Damage");
        m_damage = true;
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
