using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed = 20;
    [SerializeField] private float m_RotateSpeed = 60;

    private Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

        //transform.Translate(Vector3.right * input.x * Time.deltaTime * m_MoveSpeed );
        m_Rigidbody.AddForce(transform.right * input.x * Time.deltaTime * m_MoveSpeed * 100);
        transform.Rotate(Vector3.up * input.z * m_RotateSpeed * Time.deltaTime);
    }
}
