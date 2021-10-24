using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerPrefab;
    [SerializeField] private GameObject m_TankPrefab;
    [SerializeField] private GameObject m_EnhancedTankPrefab;
    [SerializeField] private GameObject m_GoliathTankPrefab;
    [SerializeField] private UIManager m_UIManager;
    [SerializeField] private List<Dictionary<GameObject, uint>> m_Waves;
    [SerializeField] private List<string> m_WaveMessages;
    [SerializeField] private float m_WaveMessageDisplayTime = 3;

    private GameObject m_Player;
    private int m_CreatedEnemies = 0;
    private int m_CurrentWave = 0;
    private bool m_IsGameActive;

    public void GameOver()
    {
        m_IsGameActive = false;
        m_UIManager.DisplayTitleText("Game Over!");
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

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();

        position.y = 0;
        position.x = Random.Range(-45.0f, 45.0f);
        position.z = Random.Range(-45.0f, 45.0f);

        return position;
    }

    private void SpawnWave(Dictionary<GameObject, uint> wave)
    {
        int enemyNumber = 1;
        foreach(var spawn in wave)
        {
            for(int i = 0; i < spawn.Value; ++i)
            {
                Vector3 position = GenerateRandomPosition();
                CreateEnemy(spawn.Key, position, "Tank " + enemyNumber);
                ++enemyNumber;
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_IsGameActive = true;
        m_Player = Instantiate(m_PlayerPrefab, Vector3.zero, m_PlayerPrefab.transform.rotation);
        m_Player.GetComponent<Actor>().healthPresenter = m_UIManager.GetPlayerHealthBar();
        GameObject.Find("Main Camera").GetComponent<PlayerCamera>().trackedObject = m_Player.transform;

        m_UIManager.DisplayTitleText(m_WaveMessages[0], m_WaveMessageDisplayTime);
        SpawnWave(m_Waves[0]);
    }

    // Update is called once per frame
    private void Update()
    {
        if(!m_IsGameActive)
        {
            return;
        }

        if(m_CreatedEnemies == 0)
        {
            ++m_CurrentWave;
            if(m_CurrentWave == m_Waves.Count)
            {
                m_IsGameActive = false;
                m_UIManager.DisplayTitleText("Congratulations! All waves defeated!");
            }
            else
            {
                m_UIManager.DisplayTitleText(m_WaveMessages[m_CurrentWave], m_WaveMessageDisplayTime);
                SpawnWave(m_Waves[m_CurrentWave]);
            }
        }
    }

    private void Awake()
    {
        m_Waves = new List<Dictionary<GameObject, uint>>();
        m_Waves.Add(new Dictionary<GameObject, uint>());
        m_Waves.Add(new Dictionary<GameObject, uint>());
        m_Waves.Add(new Dictionary<GameObject, uint>());
        m_Waves.Add(new Dictionary<GameObject, uint>());

        m_Waves[0].Add(m_TankPrefab, 3);

        m_Waves[1].Add(m_TankPrefab, 2);
        m_Waves[1].Add(m_EnhancedTankPrefab, 2);

        m_Waves[2].Add(m_TankPrefab, 2);
        m_Waves[2].Add(m_EnhancedTankPrefab, 2);
        m_Waves[2].Add(m_GoliathTankPrefab, 1);

        m_Waves[3].Add(m_TankPrefab, 3);
        m_Waves[3].Add(m_EnhancedTankPrefab, 2);
        m_Waves[3].Add(m_GoliathTankPrefab, 2);

        m_WaveMessages = new List<string>();
        m_WaveMessages.Add("Defeat the first Wave!");
        m_WaveMessages.Add("Defeat the second Wave!");
        m_WaveMessages.Add("Defeat the third Wave!");
        m_WaveMessages.Add("Defeat the final Wave!");
    }
}
