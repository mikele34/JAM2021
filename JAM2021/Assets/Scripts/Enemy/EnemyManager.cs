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
        Damage,
        Death
    }

    EnemyManager.State m_state = EnemyManager.State.Idle;

    NavMeshAgent m_agent;
    public Transform[] target;
    public Transform playerTarget;

    float m_walkTimer = 3.0f;
    float m_attackTimer = 5.0f;

    bool m_skip = false;
    bool m_trigger = false;

    int m_child = 0;

    Animator m_animator;

    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();        
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

                skip();

                m_walkTimer -= Time.deltaTime;

                if (m_walkTimer <= 0.0f && m_animator.GetCurrentAnimatorStateInfo(0).IsName("Skip") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    Debug.Log("1");
                    m_child = 0;
                    m_state = EnemyManager.State.Idle;
                    m_walkTimer = 3.5f;
                    m_skip = false;
                }

                break;

            //Run
            case EnemyManager.State.Run:

                m_animator.Play("Run");

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
            case EnemyManager.State.Damage:

                m_animator.Play("Damage");

                break;

            //Death
            case EnemyManager.State.Death:

                m_animator.Play("Death");

                break;
        }


        if (m_skip)
        {
            m_agent.SetDestination(target[m_child].position);

            if (m_agent.remainingDistance <= m_agent.stoppingDistance && m_child < target.Length - 1)
            {
                Debug.Log("2");
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
}
