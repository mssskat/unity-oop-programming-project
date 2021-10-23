using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float m_Speed = 5;
    [SerializeField] private uint m_Damage = 1;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.right * m_Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var actor = other.gameObject.GetComponentInParent<Actor>();
        if(actor != null)
        {
            actor.AcceptDamage(m_Damage);
        }

        Destroy(gameObject);
    }
}
