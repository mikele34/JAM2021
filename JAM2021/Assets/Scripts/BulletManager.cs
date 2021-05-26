using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    float m_speed = 230.0f;
    float m_timer = 0.0f;
    Animator m_animator;


    void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Shoot
        if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("EsplosionePP") && m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Destroy(gameObject);
        }
        else if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("ProiettilePiccolo"))
        {       
            transform.Translate(m_speed * Time.deltaTime, 0.0f, 0.0f);
            m_timer += Time.deltaTime;
        }
        if  (m_timer >= 1.0f)
        {
            m_animator.Play("EsplosionePP");
        }
    }

    public void shoot(bool direction) // true = right; false = left;
    {
        if (direction) //Right
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }
        else //Left
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, -180.0f);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            m_animator.Play("EsplosionePP");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            m_animator.Play("EsplosionePP");
        }
    }

}

