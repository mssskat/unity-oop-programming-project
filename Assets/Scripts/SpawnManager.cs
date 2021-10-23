using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject m_GameOverText;
    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private GameObject m_TankPrefab;
    [SerializeField] private GameObject m_EnhancedTankPrefab;
    [SerializeField] private GameObject m_GoliathTankPrefab;
    [SerializeField] private UIManager m_UIManager;

    private GameObject m_Player;
    private uint m_CreatedEnemies = 0;

    public void GameOver()
    {
        m_GameOverText.SetActive(true);
    }

    private void OnEnemyDied(GameObject enemy)
    {
        if(--m_CreatedEnemies == 0)
        {
            m_UIManager.RemoveAllHealthBars();
        }
    }

    private void CreateEnemy(GameObject prefab, Vector3 position, string name)
    {
        GameObject enemy = Instantiate(prefab, position, prefab.transform.rotation);
        enemy.GetComponent<Enemy>().SetTarget(m_Player);
        enemy.GetComponent<Actor>().ReportOnDeath = OnEnemyDied;
        enemy.GetComponent<Actor>().healthPresenter = m_UIManager.AppendHealthBar(name);
        ++m_CreatedEnemies;
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_Player = Instantiate(m_PlayerPrefab, Vector3.zero, m_PlayerPrefab.transform.rotation);
        m_Player.GetComponent<Actor>().healthPresenter = m_UIManager.GetPlayerHealthBar();
        GameObject.Find("Main Camera").GetComponent<PlayerCamera>().trackedObject = m_Player.transform;

        CreateEnemy(m_TankPrefab, Vector3.right * 40 + Vector3.forward * 20, "tank1");
        CreateEnemy(m_EnhancedTankPrefab, Vector3.right * 40 - Vector3.forward * 20, "tank2");
        CreateEnemy(m_GoliathTankPrefab, Vector3.right * 35, "tank3");
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
