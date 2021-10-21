using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
  [SerializeField] private Vector3 m_Offset;

  public Transform trackedObject { get; set; }

  // Start is called before the first frame update
  private void Start()
  {

  }

  // Update is called once per frame
  private void LateUpdate()
  {
      transform.position =
          trackedObject.position + (trackedObject.rotation * m_Offset);
      transform.LookAt(trackedObject);
  }
}
