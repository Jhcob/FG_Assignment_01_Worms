using UnityEngine;
using UnityEngine.UI;

public class ActivePlayerHealth : MonoBehaviour
{
    [Header("UI Health")]
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart, emptyHeart;
    [SerializeField] public int numberOfHearts;
    [SerializeField] public int health;
    private void Update()
    {
        PlayerHealth();
        CurrentLife();
    }

    private void PlayerHealth()
    {
        // If implement health pickups
        if (health > numberOfHearts)
        {
            health = numberOfHearts;
        }
        
        // UI - health iteration
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //Die anim
            Debug.Log("you are dead");
        }
    }

    public int CurrentLife()
    {
        return health;
    }
}
