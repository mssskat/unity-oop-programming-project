using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private GameObject m_TankPrefab;
    [SerializeField] private GameObject m_EnhancedTankPrefab;
    [SerializeField] private GameObject m_GoliathTankPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject player = Instantiate(m_PlayerPrefab, Vector3.zero, m_PlayerPrefab.transform.rotation);
        GameObject tank1 = Instantiate(m_TankPrefab, Vector3.right * 40 + Vector3.forward * 20, m_TankPrefab.transform.rotation);
        GameObject tank2 = Instantiate(m_EnhancedTankPrefab, Vector3.right * 40 - Vector3.forward * 20, m_EnhancedTankPrefab.transform.rotation);
        GameObject tank3 = Instantiate(m_GoliathTankPrefab, Vector3.right * 35, m_GoliathTankPrefab.transform.rotation);
        GameObject.Find("Main Camera").GetComponent<PlayerCamera>().trackedObject = player.transform;

        tank1.GetComponent<Enemy>().SetTarget(player);
        tank2.GetComponent<Enemy>().SetTarget(player);
        tank3.GetComponent<Enemy>().SetTarget(player);
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
