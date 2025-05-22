using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthImage;
    public Sprite[] healthSprites;
    public int maxHealth = 5;
    private int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
        // UpdateHealthUI(currentHealth);
        healthImage.sprite = healthSprites[5];

    }

    public void UpdateHealthUI(int newHealth)
    {
        int spriteIndex = newHealth;
        healthImage.sprite = healthSprites[spriteIndex];
        healthImage.SetNativeSize(); 
    }
}

