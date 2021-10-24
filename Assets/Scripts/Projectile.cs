using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_Speed = 5;
    [SerializeField] private uint m_Damage = 1;

    private GameObject m_Parent;
    private Transform[] m_IgnoredObjects;
    private bool m_IsEnemyOwned;

    // ENCAPSULATION: Use custom setter to initialise private fields 
    public GameObject parent
    {
        get
        {
            return m_Parent;
        }
        set
        {
            m_Parent = value;
            m_IsEnemyOwned = m_Parent.CompareTag("Enemy");
            m_IgnoredObjects = m_Parent.GetComponentsInChildren<Transform>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.right * m_Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isIgnoredPbject(other.gameObject))
        {
            return;
        }

        if(m_IsEnemyOwned)
        {
            var enemy = other.gameObject.GetComponentInParent<Enemy>();
            if(enemy != null)
            {
                Destroy(gameObject);
                return;
            }
        }


        var actor = other.gameObject.GetComponentInParent<Actor>();
        if(actor != null)
        {
            actor.AcceptDamage(m_Damage);
        }

        Destroy(gameObject);
    }

    // ABSTRACTION: method checks if a projectile should not interact with object
    private bool isIgnoredPbject(Object collidedObject)
    {
        if(m_IgnoredObjects == null)
        {
            return false;
        }

        foreach(Transform children in m_IgnoredObjects)
        {
            if(children != null && children.gameObject == collidedObject)
            {
                return true;
            }
        }
        return false;
    }
}
