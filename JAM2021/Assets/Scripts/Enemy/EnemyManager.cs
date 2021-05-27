using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    enum State
    {
        Idle,
        Skip,
        Run,
        Attack,
        Hit,
        Death
    }

    EnemyManager.State m_state = EnemyManager.State.Idle;
    NavMeshAgent m_agent;
    Animator m_animator;
    BoxCollider m_boxCollider;
    CapsuleCollider m_capsuleCollider;

    public Transform[] target;
    public Transform playerTarget;

    public int healt = 3;

    float m_walkTimer = 3.0f;
    float m_attackTimer = 5.0f;

    bool m_skip = false;
    bool m_trigger = false;

    int m_child = 0;


    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
        m_boxCollider = GetComponent<BoxCollider>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();        
    }

    void Update()
    {

        switch (m_state)
        {
            //Idle
            case EnemyManager.State.Idle:

                m_animator.Play("Idle");

                m_walkTimer -= Time.deltaTime;

                if (m_walkTimer <= 0.0f && m_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    m_state = EnemyManager.State.Skip;
                    m_walkTimer = 12.5f;
                }

                break;

            //Skip
            case EnemyManager.State.Skip:

                m_animator.Play("Skip");
                m_agent.acceleration = 8;

                skip();

                m_walkTimer -= Time.deltaTime;

                if (m_walkTimer <= 0.0f && m_animator.GetCurrentAnimatorStateInfo(0).IsName("Skip") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    m_child = 0;
                    m_state = EnemyManager.State.Idle;
                    m_walkTimer = 3.5f;
                    m_skip = false;
                }

                break;

            //Run
            case EnemyManager.State.Run:

                m_animator.Play("Run");

                m_agent.acceleration = 10;

                break;

            //Attack
            case EnemyManager.State.Attack:

                int m_randomAttack = Random.Range(0, 1);

                m_attackTimer -= Time.deltaTime;

                if (m_randomAttack == 0 && m_attackTimer <= 0.0f)
                {
                    m_animator.Play("Attack");
                }
                else if(m_attackTimer <= 0.0f)
                {
                    m_animator.Play("Attack2");
                }                

                break;

            //Damage
            case EnemyManager.State.Hit:

                m_animator.Play("Hit");

                m_agent.SetDestination(transform.position);

                if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    m_state = EnemyManager.State.Run;
                    m_trigger = true;
                }                

                break;

            //Death
            case EnemyManager.State.Death:

                m_animator.Play("Death");
                m_agent.SetDestination(transform.position);
                m_trigger = false;
                m_boxCollider.enabled = false;
                m_capsuleCollider.enabled = false;

                break;
        }


        if (m_skip)
        {
            m_agent.SetDestination(target[m_child].position);

            if (m_agent.remainingDistance <= m_agent.stoppingDistance && m_child < target.Length - 1)
            {
                m_child++;
                m_agent.SetDestination(target[m_child].position);
            }
            else if (m_child == target.Length)
            {
                m_child = 0;
            }
        }

        if (m_trigger)
        {
            m_skip = false;
            m_child = 0;
            m_agent.SetDestination(playerTarget.position);
        }
        else
        {
            if (m_state == EnemyManager.State.Skip)
            {
                m_skip = true;
            }
            else 
            {
                m_agent.SetDestination(transform.position);
                m_state = EnemyManager.State.Idle;
            }
            
        }

        if (healt <= 0)
        {
            m_state = EnemyManager.State.Death;
        }
    }

    void skip()
    {
        m_skip = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_trigger = true;
            m_state = EnemyManager.State.Run;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_trigger = false;            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            healt--;
            m_state = EnemyManager.State.Hit;
        }
    }
}
