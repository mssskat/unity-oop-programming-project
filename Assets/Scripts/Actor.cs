using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE: This class is a base class for the player and all enemies
public class Actor : MonoBehaviour
{
    [SerializeField] private uint m_Health;
    [SerializeField] private uint m_MaxHealth = 100;
    [SerializeField] protected GameObject m_ProjectilePrefab;

    public IHealthPresenter healthPresenter;
    public delegate void ReportOnEvent(GameObject obj);
    public ReportOnEvent ReportOnDeath;

    // ABSTRACTION: AcceptDamage deals with damage
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
            if(ReportOnDeath != null)
            {
                ReportOnDeath(gameObject);
            }
            Destroy(gameObject);
        }
    }

    // POLYMORPHISM: A virtual method defines specific behaviour when health reduces to zero
    protected virtual void Die()
    {
        Debug.Log("Object " + gameObject.name + " has been destroyed");
    }

    // ABSTRACTION: ProjectileAttack is a helper method used in derived classes
    protected void ProjectileAttack(Vector3 relativePosition, Vector3 direction)
    {
        Vector3 projectileStartPosition = transform.position + relativePosition;
        Quaternion projectileRotation = Quaternion.FromToRotation(Vector3.right, direction);
        GameObject projectile = Instantiate(m_ProjectilePrefab, projectileStartPosition, projectileRotation);
        projectile.GetComponent<Projectile>().parent = gameObject;
    }
}
