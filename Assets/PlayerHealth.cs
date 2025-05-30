using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public float recoveryDelay = 3f;
    public float recoveryRate = 5f;

    public Volume postProcessingVolume;
    private ColorAdjustments colorAdjustments;

    private int currentHealth;
    private float lastDamageTime;

    void Start()
    {
        currentHealth = maxHealth;

        if (postProcessingVolume != null &&
            postProcessingVolume.profile.TryGet(out colorAdjustments))
        {
            Debug.Log("[Start] Color Adjustments found.");
            UpdateExposure();
        }
        else
        {
            Debug.LogWarning("[Start] Color Adjustments NOT found.");
        }
    }

    void Update()
    {
        if (Time.time - lastDamageTime >= recoveryDelay && currentHealth < maxHealth)
        {
            int previous = currentHealth;
            currentHealth += Mathf.RoundToInt(recoveryRate * Time.deltaTime);
            currentHealth = Mathf.Min(currentHealth, maxHealth);

            if (currentHealth != previous)
            {
                UpdateExposure();
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        lastDamageTime = Time.time;
        UpdateExposure();
        Debug.Log($"[Damage] Health: {currentHealth}");
    }

    void UpdateExposure()
    {
        if (colorAdjustments != null)
        {
            float t = 1f - ((float)currentHealth / maxHealth); // چقدر تاریک بشه
            colorAdjustments.postExposure.value = Mathf.Lerp(0f, -2.5f, t); // نور کمتر
            Debug.Log($"[Exposure] Set to {colorAdjustments.postExposure.value}");
        }
    }
}
