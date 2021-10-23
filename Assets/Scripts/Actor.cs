using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private uint m_Health;

    public void AcceptDamage(uint amount)
    {
        m_Health = (m_Health > amount) ? m_Health - amount : 0;
    }
}
