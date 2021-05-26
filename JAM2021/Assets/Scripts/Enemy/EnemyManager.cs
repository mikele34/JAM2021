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
    public Transform waypointGroup;
    int m_random = 0;
    int m_randomController = 0;

    float m_walkTimer = 2.0f;
    float m_attackTimer = 5.0f;

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
                    m_walkTimer = 3.0f;
                }

                break;

            //Skip
            case EnemyManager.State.Skip:

                m_animator.Play("Skip");

                /*float distance = Vector3.Distance(transform.position, waypointGroup.GetChild(m_random).position);

                if (distance <= 1f)
                {
                    m_random = Random.Range(0, waypointGroup.childCount - 1);
                }

                m_agent.SetDestination(waypointGroup.GetChild(m_random).position);*/

                m_walkTimer -= Time.deltaTime;

                if (m_walkTimer <= 0.0f && m_animator.GetCurrentAnimatorStateInfo(0).IsName("Skip") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {;
                    m_state = EnemyManager.State.Idle;
                    m_walkTimer = 3.0f;
                }

                break;

            //Run
            case EnemyManager.State.Run:

                m_animator.Play("Run");

                break;

            //Attack
            case EnemyManager.State.Attack:

                int m_random = Random.Range(0, 1);

                m_attackTimer -= Time.deltaTime;

                if (m_random == 0 && m_attackTimer <= 0.0f)
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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_state = EnemyManager.State.Run;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_state = EnemyManager.State.Idle;
        }
    }
}
