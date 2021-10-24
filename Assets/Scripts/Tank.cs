using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE: Tank class derives from Enemy base class indicating
// that it is a kind of enemy
public class Tank : Enemy
{
    [SerializeField] private Vector3 m_GunPoint;
    [SerializeField] private float m_CollideAttackForce;

    // POLYMORPHISM: Override PerformIdleAction
    protected override void PerformIdleAction(GameObject target)
    {
        RotateToTarget(target);
    }

    // POLYMORPHISM: Override PerformRegularAttack
    protected override void PerformRegularAttack(GameObject target)
    {
        Vector3 attackDirection = target.transform.position - transform.position;
        Vector3 attackPosition = transform.TransformVector(m_GunPoint);
        ProjectileAttack(attackPosition, attackDirection);
    }

    // POLYMORPHISM: Override PerformLongDistanceAction
    protected override void PerformLongDistanceAction(GameObject target)
    {
        CollideAttack(target, m_CollideAttackForce);
    }
}
