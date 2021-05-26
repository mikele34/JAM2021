using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    float m_walkTimer = 2.0f;
    float m_attackTimer = 5.0f;

    Animator m_animator;

    void Awake()
    {
        m_animator = GetComponent<Animator>();
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
                    m_animator.CrossFade("Idle", 0.05f);
                    m_animator.CrossFade("Skip", 0.05f);
                    m_state = EnemyManager.State.Skip;
                    m_walkTimer = 3.0f;
                }

                break;

            //Skip
            case EnemyManager.State.Skip:

                m_animator.Play("Skip");

                m_walkTimer -= Time.deltaTime;

                if (m_walkTimer <= 0.0f && m_animator.GetCurrentAnimatorStateInfo(0).IsName("Skip") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    m_animator.CrossFade("Idle", 0.05f);
                    m_animator.CrossFade("Skip", 0.05f);
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
