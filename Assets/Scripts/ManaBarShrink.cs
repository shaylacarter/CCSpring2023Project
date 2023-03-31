using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarShrink : MonoBehaviour
{

    private const float DAMAGED_HEALTH_SHRINK_TIMER_MAX = 1f;

    private Image barImage;
    private Image damagedBarImage;
    private float damagedHealthShrinkTimer;
    private HealthSystem healthSystem;

    private void Awake() {
        barImage = transform.Find("HealthBarFill").GetComponent<Image>();
        damagedBarImage = transform.Find("HealthBarDamage").GetComponent<Image>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);
        Damage(100);
        SetHealth(0);
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        damagedBarImage.fillAmount = barImage.fillAmount;
    }

    private void Update()  {
        damagedHealthShrinkTimer -= Time.deltaTime;
        if (damagedHealthShrinkTimer < 0 && barImage.fillAmount < damagedBarImage.fillAmount) {
            float shrinkSpeed = 1f;
            damagedBarImage.fillAmount -= shrinkSpeed * Time.deltaTime;
        }

        if (damagedBarImage.fillAmount < barImage.fillAmount) {
            damagedBarImage.fillAmount = barImage.fillAmount;
        }
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e) {
        SetHealth(healthSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = barImage.fillAmount;
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        damagedHealthShrinkTimer = DAMAGED_HEALTH_SHRINK_TIMER_MAX;
        SetHealth(healthSystem.GetHealthNormalized());
    }

    private void SetHealth(float healthNormalized) {
        barImage.fillAmount = healthNormalized;
    }

    public void Damage(int amount) {
        healthSystem.Damage(amount);
    }

    public void Heal(int amount) {
        healthSystem.Heal(amount);
    }
}
