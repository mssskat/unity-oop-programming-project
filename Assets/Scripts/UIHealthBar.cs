using UnityEngine;
using UnityEngine.UI;

// POLYMORPHISM: UIHealthBar implements IHealthPresenter interface by overriding
// UpdateHealth method
public class UIHealthBar : MonoBehaviour, IHealthPresenter
{
    [SerializeField] private Image m_BarImage;

    private void Awake()
    {
        m_BarImage = GetComponent<Transform>().Find("Health Bar").GetComponent<Image>();
    }

    public void UpdateHealth(uint health, uint maxHealth)
    {
        m_BarImage.fillAmount = (float)health/maxHealth;
    }
}
