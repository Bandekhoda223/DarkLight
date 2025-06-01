using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public float recoveryDelay = 3f;       // چند ثانیه بعد از آخرین دمیج ریکاوری شروع بشه
    public float recoveryRate = 5f;        // چند واحد جون بر ثانیه پر بشه
    private float recoveryBuffer = 0f;

    [Header("UI Overlay")]
    public Image darkOverlay;              // رفرنس به لایه مشکی UI

    private int currentHealth;
    private float lastDamageTime;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateOverlay();
    }

    void Update()
    {
        TryRecover();
    }

    public void TakeDamage(int amount)
    {
        anim.SetBool("isHurt", true);
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        lastDamageTime = Time.time;

        UpdateOverlay();
        Debug.Log($"[Damage] Health: {currentHealth}");
        if (currentHealth <= 0)
        {

        }
    }

    private void TryRecover()
    {
        if (currentHealth < maxHealth && Time.time - lastDamageTime >= recoveryDelay)
    {
        recoveryBuffer += recoveryRate * Time.deltaTime;

        Debug.Log($"[Recovery] Buffer: {recoveryBuffer} (Rate: {recoveryRate})");
            if (recoveryBuffer >= 1f)
            {

                int healthToAdd = Mathf.FloorToInt(recoveryBuffer);
                recoveryBuffer -= healthToAdd;
                Debug.Log($"[Recovering] Buffer: {recoveryBuffer} (Adding: {healthToAdd})");
                currentHealth += healthToAdd;
                currentHealth = Mathf.Min(currentHealth, maxHealth);
                Debug.Log($"[Recovering] Current Health: {currentHealth} (Max: {maxHealth})");
                UpdateOverlay();
                Debug.Log($"[Recovering] +{healthToAdd} → Health: {currentHealth}");
            }
    }
    }

    private void UpdateOverlay()
    {
        if (darkOverlay != null)
        {
            float t = 1f - ((float)currentHealth / maxHealth); // t = درصد آسیب
            Color c = darkOverlay.color;
            c.a = Mathf.Lerp(0f, 0.6f, t);                     // نور صفحه کم یا زیاد شه
            darkOverlay.color = c;

            Debug.Log($"[Overlay] Alpha set to: {c.a}");
        }
    }
}
