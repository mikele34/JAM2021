using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    enum State
    {             
        Move,
        Attack,
        Hit,
        Death
    }

    PlayerManager.State m_state = PlayerManager.State.Move;


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


    int m_hitPoint = 0; 


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
        switch (m_state)
        {
            //Move
            case PlayerManager.State.Move:

                //Moviment
                if (m_inputManager.walkLeft && m_inputManager.walkRight || m_inputManager.walkUp && m_inputManager.walkDown)
                {
                    m_animator.Play("Idle");
                    m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                }
                else if (m_inputManager.walkLeft || m_inputManager.walkRight || m_inputManager.walkUp || m_inputManager.walkDown)
                {
                    if (m_inputManager.run)
                    {
                        m_animator.Play("Run");
                        speed = runspeed;

                        //Right
                        if (m_inputManager.walkRight)
                        {
                            transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                            m_rigidbody.velocity = new Vector3(speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                        }

                        //Left
                        if (m_inputManager.walkLeft)
                        {
                            transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
                            m_rigidbody.velocity = new Vector3(-speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                        }

                        //Up
                        if (m_inputManager.walkUp)
                        {
                            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                            m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, speed * Time.fixedDeltaTime);
                        }

                        //Down
                        if (m_inputManager.walkDown)
                        {
                            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                            m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, -speed * Time.fixedDeltaTime);
                        }
                    }
                    else
                    {
                        m_animator.Play("Skip");
                        speed = walkspeed;

                        //Right
                        if (m_inputManager.walkRight)
                        {
                            transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
                            m_rigidbody.velocity = new Vector3(speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                        }

                        //Left
                        if (m_inputManager.walkLeft)
                        {
                            transform.eulerAngles = new Vector3(0.0f, 270.0f, 0.0f);
                            m_rigidbody.velocity = new Vector3(-speed * Time.fixedDeltaTime, m_rigidbody.velocity.y, 0.0f);
                        }

                        //Up
                        if (m_inputManager.walkUp)
                        {
                            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                            m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, speed * Time.fixedDeltaTime);
                        }

                        //Down
                        if (m_inputManager.walkDown)
                        {
                            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                            m_rigidbody.velocity = new Vector3(0.0f, m_rigidbody.velocity.y, -speed * Time.fixedDeltaTime);
                        }
                    }
                }
                else
                {
                    m_animator.Play("Idle");
                    m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                }

                //Attack
                if (m_inputManager.attack)
                {
                    m_animator.Play("Attack");
                    m_state = PlayerManager.State.Attack;
                }

                break;

           
            //Attack
            case PlayerManager.State.Attack:

                m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

                if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    m_state = PlayerManager.State.Move;
                }

                break;

            //Hit
            case PlayerManager.State.Hit:                 

                if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    m_state = PlayerManager.State.Move;
                }

                break;

            //Death
            case PlayerManager.State.Death:

                if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    SceneManager.LoadScene("SampleScene");
                }

                break;
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            m_hitPoint+=10;

            if (m_hitPoint < m_healthManager.numOfHearts)
            {
                m_animator.Play("Hit");

                healtBar.SetHealth(m_healthManager.Health);
                m_healthManager.Health -= 10;

                m_state = PlayerManager.State.Hit;
            }
            else if (m_hitPoint >= m_healthManager.numOfHearts)
            {
                m_animator.Play("Death");
                m_state = PlayerManager.State.Death;
            }
        }
    }
}
