using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject.Find("Main Camera").GetComponent<PlayerCamera>().trackedObject =
            Instantiate(m_PlayerPrefab, Vector3.zero, m_PlayerPrefab.transform.rotation).transform;
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
