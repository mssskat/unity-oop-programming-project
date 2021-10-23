using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    [SerializeField] private Vector3 m_GunPoint;
    [SerializeField] private float m_CollideAttackForce;

    protected override void PerformIdleAction(GameObject target)
    {
        RotateToTarget(target);
    }

    protected override void PerformRegularAttack(GameObject target)
    {
        Vector3 attackDirection = target.transform.position - transform.position;
        Vector3 attackPosition = transform.TransformVector(m_GunPoint);
        ProjectileAttack(attackPosition, attackDirection);
    }

    protected override void PerformLongDistanceAction(GameObject target)
    {
        CollideAttack(target, m_CollideAttackForce);
    }
}
