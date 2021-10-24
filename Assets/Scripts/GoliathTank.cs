using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE: GoliathTank class derives from Enemy base class indicating
// that it is a kind of enemy
public class GoliathTank : Enemy
{
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private float m_BodyRotationSpeed;
    [SerializeField] private List<Transform> m_GunPoints;

    private Transform m_Body;
    private float m_CurrentMoveSpeed;

    private void Awake()
    {
        m_Body = transform.Find("Body");
        m_GunPoints = new List<Transform>();
        m_CurrentMoveSpeed = m_MoveSpeed;

        foreach(Transform gun in m_Body)
        {
            m_GunPoints.Add(gun);
        }
    }

    // POLYMORPHISM: Override PerformIdleAction
    protected override void PerformIdleAction(GameObject target)
    {
        m_Body.Rotate(Vector3.up, Time.deltaTime * m_BodyRotationSpeed);

        Vector3 directionToTarget = target.transform.position - transform.position;
        MoveToDirection(directionToTarget, m_CurrentMoveSpeed);
    }

    // POLYMORPHISM: Override PerformShortDistanceAction
    protected override void PerformShortDistanceAction(GameObject target)
    {
        m_CurrentMoveSpeed = 0;
    }

    // POLYMORPHISM: Override PerformLongDistanceAction
    protected override void PerformLongDistanceAction(GameObject target)
    {
        m_CurrentMoveSpeed = m_MoveSpeed;
    }

    // POLYMORPHISM: Override PerformRegularAttack
    protected override void PerformRegularAttack(GameObject target)
    {
        Vector3 attackDirection = m_Body.right;

        foreach(Transform gunPoint in m_GunPoints)
        {
            Vector3 spawnPosition = 11*gunPoint.localPosition - 2*Vector3.up;
            ProjectileAttack(m_Body.rotation * spawnPosition, gunPoint.up);
        }
    }
}
