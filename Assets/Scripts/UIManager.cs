using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyHealthBarPrefab;

    private List<GameObject> m_EnemyHealthBars;

    public IHealthPresenter GetPlayerHealthBar()
    {
        return transform.Find("Player Health Bar").GetComponent<UIHealthBar>();
    }

    public IHealthPresenter AppendHealthBar(string name)
    {
        GameObject healthBar = Instantiate(m_EnemyHealthBarPrefab, transform);
        Transform healthBarTransform = healthBar.GetComponent<Transform>();
        healthBarTransform.SetParent(transform);
        healthBarTransform.Find("Enemy Name").GetComponent<Text>().text = name;
        m_EnemyHealthBars.Add(healthBar);

        for(int i = 0; i < m_EnemyHealthBars.Count - 1; ++i)
        {
            m_EnemyHealthBars[i].GetComponent<Transform>().position += Vector3.up * 50;
        }

        return healthBar.GetComponent<UIHealthBar>();
    }

    public void RemoveAllHealthBars()
    {
        foreach(GameObject healthBar in m_EnemyHealthBars)
        {
            Destroy(healthBar);
        }
        m_EnemyHealthBars.Clear();
    }

    // Start is called before the first frame update
    private void Awake()
    {
        m_EnemyHealthBars = new List<GameObject>();
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
