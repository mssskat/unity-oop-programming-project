using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_Speed = 5;
    [SerializeField] private uint m_Damage = 1;

    private Transform[] m_IgnoredObjects;
    private bool m_IsEnemyOwned;

    public void SetParent(GameObject parent)
    {
        m_IsEnemyOwned = parent.CompareTag("Enemy");
        m_IgnoredObjects = parent.GetComponentsInChildren<Transform>();
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
