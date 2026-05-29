using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthbarBehaviour : MonoBehaviour
{
    public Slider SLider;
    public Color Low;
    public Color High;
    public Vector3 Offset;

    public void SetHealth(float health, float maxHealth)
    {
        SLider.gameObject.SetActive(health < maxHealth);
        SLider.value = health;
        SLider.maxValue = maxHealth;

        SLider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, SLider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        SLider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}