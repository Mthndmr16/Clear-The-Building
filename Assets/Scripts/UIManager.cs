using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // image ve text kullanýrken bu kütüphaneyi eklememiz gerek.

public class UIManager : MonoBehaviour
{
    public Image healthFill;
    public Image magazineFill;

    private Attack playerAmmo;
    private Target playerHealth;

    private void Awake()
    {
        playerAmmo = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
        playerHealth = playerAmmo.GetComponent<Target>();
    }

    void Update()
    {
        UpdateHealthFill();
        UpdateMagazineFill();
    }

    private void UpdateMagazineFill()
    {
        magazineFill.fillAmount = (float)playerAmmo.GetAmmo / playerAmmo.GetMagazineSize;  // fillamount 0-1f arasýnda deðer alýr. bizim o anki kurþunumuz / þarjördeki kurþun diyoruz.
    }

    private void UpdateHealthFill()
    {
        healthFill.fillAmount = (float)playerHealth.getHealth / playerHealth.GetMaximumHealth;
    }
}
