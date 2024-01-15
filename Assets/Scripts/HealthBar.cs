using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillbar;
    public TextMeshProUGUI healthText;
    public void SetHealth(int currentValue, int maxValue)
    {
        fillbar.fillAmount = (float)currentValue / maxValue;
        healthText.text = currentValue.ToString() + "/" + maxValue.ToString();
    }
}
