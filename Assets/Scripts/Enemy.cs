using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject m_ProjectilePrefab;
    [SerializeField] private Vector3 m_ProjectileSpawnPoint;
    [SerializeField] private float m_CollideAttackForce;

    private GameObject m_Target;

    public void SetTarget(GameObject target)
    {
        m_Target = target;
    }

    // Start is called before the first frame update
    private void Start()
    {
        CollideAttack(m_Target);
        StartCoroutine(DelayedAttack(m_Target, 2));
    }

    // Update is called once per frame
    private void Update()
    {
        RotateToTarget(m_Target);

        if(Vector3.Distance(m_Target.transform.position, transform.position) > 40)
        {
            CollideAttack(m_Target);
        }
    }

    private void RotateToTarget(GameObject target)
    {
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.y = 0;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, targetDirection);
    }

    private void CollideAttack(GameObject target)
    {
        Vector3 attackDirection = (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, attackDirection);
        GetComponent<Rigidbody>().AddForce(attackDirection * m_CollideAttackForce, ForceMode.Impulse);
    }

    private void ProjectileAttack(GameObject target)
    {
        Vector3 attackDirection = target.transform.position - transform.position;
        Vector3 projectileStartPosition = transform.position + transform.TransformVector(m_ProjectileSpawnPoint);
        Quaternion projectileRotation = Quaternion.FromToRotation(Vector3.right, attackDirection);
        Instantiate(m_ProjectilePrefab, projectileStartPosition, projectileRotation);
    }

    private IEnumerator DelayedAttack(GameObject target, float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            ProjectileAttack(target);
        }
    }
}
