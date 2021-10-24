using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ABSTRACTION: UIManager contains helper methods for interacting with UI Canvas
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyHealthBarPrefab;

    private GameObject m_CentralTitleText;
    private List<GameObject> m_EnemyHealthBars;

    public void DisplayTitleText(string text)
    {
        m_CentralTitleText.GetComponent<Text>().text = text;
        EnableTitleText();
    }

    public void EnableTitleText()
    {
        m_CentralTitleText.SetActive(true);
    }

    public void DisableTitleText()
    {
        m_CentralTitleText.SetActive(false);
    }

    public void DisplayTitleText(string text, float displayTime)
    {
        m_CentralTitleText.GetComponent<Text>().text = text;
        StartCoroutine(DisplayText(displayTime));
    }

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

    private IEnumerator DisplayText(float time)
    {
        EnableTitleText();
        yield return new WaitForSeconds(time);
        DisableTitleText();
    }

    private void Awake()
    {
        m_EnemyHealthBars = new List<GameObject>();
        m_CentralTitleText = transform.Find("Central Text").gameObject;
    }
}
