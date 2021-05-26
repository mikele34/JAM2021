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
    bool trigger = false;

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
                    m_state = EnemyManager.State.Idle;
                    m_walkTimer = 3.0f;
                }

                break;

            //Run
            case EnemyManager.State.Run:

                m_animator.Play("Run");

                if (!trigger)
                {
                    m_state = EnemyManager.State.Idle;
                }

                break;

            //Attack
            case EnemyManager.State.Attack:

                m_animator.Play("Attack");

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
        if (other.tag == "Player")
        {
            trigger = true;
            m_state = EnemyManager.State.Run;
        }
    }
}
