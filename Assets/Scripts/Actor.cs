using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private uint m_Health;
    [SerializeField] private uint m_MaxHealth = 100;

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
}
