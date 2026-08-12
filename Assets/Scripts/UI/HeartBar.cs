using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartBar : MonoBehaviour
{
    [Header("UI Elements")]
    public Image heartPrefab;

    List<Heart> hearts = new List<Heart>();
    private int currentHealth;
    private int maxHealth;
    private PlayerHealth playerHealth;

    void Start()
    {
        if (playerHealth == null)
            playerHealth = FindAnyObjectByType<PlayerHealth>();

        maxHealth = Mathf.Max(1, Mathf.RoundToInt(playerHealth.maxHealth));
        currentHealth = Mathf.RoundToInt(playerHealth.GetCurrentHealth());
        DrawHearts();
    }

    private void OnEnable()
    {
        if (playerHealth == null)
            playerHealth = FindAnyObjectByType<PlayerHealth>();

        PlayerHealth.OnHealthChanged += DrawHearts;
    }

    private void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= DrawHearts;
    }

    public void ClearHeartImages()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        hearts.Clear();
    }

    public void CreateEmptyHeart()
    {
        Image newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
        Heart heartComponent = newHeart.GetComponent<Heart>();
        heartComponent.SetHeartStatus(HeartStatus.Empty);
        hearts.Add(newHeart.GetComponent<Heart>());
    }

    public void DrawHearts()
    {
        float heartRemainder = maxHealth % 2;
        int maxHeartCount = Mathf.FloorToInt(maxHealth / 2f + heartRemainder);
        ClearHeartImages();
        for (int i = 0; i < maxHeartCount; i++)
        {
            CreateEmptyHeart();
        }
        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatus = Mathf.Clamp(playerHealth.GetCurrentHealth() - i * 2, 0, 2);
            hearts[i].SetHeartStatus((HeartStatus)heartStatus);
        }
    }
}
