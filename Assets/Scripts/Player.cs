using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField] private float m_MoveSpeed = 20;
    [SerializeField] private float m_RotateSpeed = 60;
    [SerializeField] private float m_MinAttackInterval = 1;

    private Rigidbody m_Rigidbody;
    private float m_TimeSinceLatestAttack;

    // Start is called before the first frame update
    private void Awake()
    {
        healthPresenter = GameObject.Find("Health Bar").GetComponent<UIHealthBar>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_TimeSinceLatestAttack = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

        m_Rigidbody.AddForce(transform.right * input.x * Time.deltaTime * m_MoveSpeed * 100);
        transform.Rotate(Vector3.up * input.z * m_RotateSpeed * Time.deltaTime);

        if(Input.GetKey(KeyCode.Space))
        {
            if(Time.time - m_TimeSinceLatestAttack > m_MinAttackInterval)
            {
                ProjectileAttack(transform.TransformVector(new Vector3(4, 1, 0)), transform.right);
                m_TimeSinceLatestAttack = Time.time;
            }
        }
    }

    protected override void Die()
    {
        GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().GameOver();
    }
}
