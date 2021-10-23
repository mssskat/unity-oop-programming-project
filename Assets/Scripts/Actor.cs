using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private uint m_Health;
    [SerializeField] private uint m_MaxHealth = 100;
    [SerializeField] protected GameObject m_ProjectilePrefab;

    protected IHealthPresenter healthPresenter;

    public void AcceptDamage(uint amount)
    {
        m_Health = (m_Health > amount) ? m_Health - amount : 0;

        if(healthPresenter != null)
        {
            healthPresenter.UpdateHealth(m_Health, m_MaxHealth);
        }

        if(m_Health == 0)
        {
            Die();
            Destroy(gameObject);
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Object " + gameObject.name + " has been destroyed");
    }

    protected void ProjectileAttack(Vector3 relativePosition, Vector3 direction)
    {
        Vector3 projectileStartPosition = transform.position + relativePosition;
        Quaternion projectileRotation = Quaternion.FromToRotation(Vector3.right, direction);
        GameObject projectile = Instantiate(m_ProjectilePrefab, projectileStartPosition, projectileRotation);
        projectile.GetComponent<Projectile>().SetParent(gameObject);
    }
}
