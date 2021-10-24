using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE: Enemy class derives from Actor base class and is a base class
// for all enemy classes
public class Enemy : Actor
{
    [SerializeField] private float m_DistanceTreshhold = 40;
    [SerializeField] private float m_AttackDelay = 2;

    private GameObject m_Target;

    public void SetTarget(GameObject target)
    {
        m_Target = target;
    }

    // Start is called before the first frame update
    private void Start()
    {
        if(m_Target != null)
        {
            StartCoroutine(DelayedAttack(m_Target, m_AttackDelay));
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(m_Target != null)
        {
            PerformIdleAction(m_Target);

            float distanceToTarget = Vector3.Distance(m_Target.transform.position, transform.position);

            if(distanceToTarget < m_DistanceTreshhold)
            {
                PerformShortDistanceAction(m_Target);
            }
            else
            {
                PerformLongDistanceAction(m_Target);
            }
        }
    }

    // POLYMORPHISM: A virtual method defines action performed in every Update
    protected virtual void PerformIdleAction(GameObject target) {}

    // POLYMORPHISM: A virtual method defines action performed when target is at short distance
    protected virtual void PerformShortDistanceAction(GameObject target) {}

    // POLYMORPHISM: A virtual method defines action performed when target is at long distance
    protected virtual void PerformLongDistanceAction(GameObject target) {}

    // POLYMORPHISM: A virtual method defines enemy attack
    protected virtual void PerformRegularAttack(GameObject target) {}

    protected void MoveToDirection(Vector3 direction, float speed)
    {
        transform.Translate(speed * direction.normalized * Time.deltaTime);
    }

    protected void RotateToTarget(GameObject target)
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.y = 0;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, targetDirection);
    }

    protected void CollideAttack(GameObject target, float force)
    {
        Vector3 attackDirection = (target.transform.position - transform.position);
        transform.rotation = Quaternion.FromToRotation(Vector3.right, attackDirection);
        GetComponent<Rigidbody>().AddForce(attackDirection.normalized * force);
    }

    private IEnumerator DelayedAttack(GameObject target, float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            if(target != null)
            {
                PerformRegularAttack(target);
            }
        }
    }
}
