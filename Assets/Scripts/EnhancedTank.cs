using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancedTank : Enemy
{
    [SerializeField] private float m_HeadRotationSpeed = 60;
    [SerializeField] private float m_MoveSpeed = 5;
    [SerializeField] private Vector3 m_FrontGunPoint;
    [SerializeField] private Vector3 m_BackGunPoint;

    private Transform m_Head;

    private void Awake()
    {
        m_Head = transform.Find("Head");
    }

    protected override void PerformIdleAction(GameObject target)
    {
        m_Head.Rotate(Vector3.up, Time.deltaTime * m_HeadRotationSpeed);

        Vector3 directionToTarget = target.transform.position - transform.position;
        Vector3 moveDirection = Vector3.Cross(directionToTarget, Vector3.up);
        MoveToDirection(moveDirection, m_MoveSpeed);
    }

    protected override void PerformShortDistanceAction(GameObject target)
    {
        Vector3 moveDirection = -(target.transform.position - transform.position);
        MoveToDirection(moveDirection, m_MoveSpeed);
    }

    protected override void PerformRegularAttack(GameObject target)
    {
        Vector3 attackDirection = m_Head.right;

        ProjectileAttack(m_Head.rotation * m_FrontGunPoint, attackDirection);
        ProjectileAttack(m_Head.rotation * m_BackGunPoint, -attackDirection);
    }
}
