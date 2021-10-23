using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private GameObject m_EnemyPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject player = Instantiate(m_PlayerPrefab, Vector3.zero, m_PlayerPrefab.transform.rotation);
        GameObject enemy = Instantiate(m_EnemyPrefab, Vector3.right * 40, m_EnemyPrefab.transform.rotation);
        GameObject.Find("Main Camera").GetComponent<PlayerCamera>().trackedObject = player.transform;

        enemy.GetComponent<Enemy>().SetTarget(player);
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
