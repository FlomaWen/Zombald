using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public GameObject heartPrefab;
    public Transform heartsContainer;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    
    private float _currentHealth = 6;
    private int _maxHearts = 3;

    private void Start()
    {
        UpdateHeartsUI();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0) _currentHealth = 0;
        UpdateHeartsUI();
    }
    
    public void AddHeartContainer()
    {
        if (_maxHearts < 13)
        {
            _maxHearts++;
            _currentHealth += 2;
            UpdateHeartsUI();
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = heartsContainer.childCount; i < _maxHearts; i++)
        {
            Instantiate(heartPrefab, heartsContainer);
        }
        
        for (int i = 0; i < _maxHearts; i++)
        {
            var heart = heartsContainer.GetChild(i).gameObject;
            Image heartImage = heart.GetComponent<Image>();
            
            if (i * 2 + 1 == _currentHealth)
            {
                heartImage.sprite = halfHeart;
                heart.SetActive(true); 
            }
            else if (i * 2 < _currentHealth)
            {
                heartImage.sprite = fullHeart;
                heart.SetActive(true);
            }
            else
            {
                heartImage.sprite = emptyHeart;
                heart.SetActive(true); 
            }
        }
    }
}
