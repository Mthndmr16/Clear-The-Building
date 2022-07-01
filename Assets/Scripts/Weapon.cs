using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Attack attack;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private float fireRate;
    [SerializeField] private int magazine;
    [SerializeField] private AudioClip clip;
    private int currentMagazine;

    public int GetWeaponCurrentMagazine
    {
        get
        {
            return currentMagazine;
        }
        set
        {
            currentMagazine = value;
        }
    }

    void Start()  // oyunu bu �ekilde ba�latt���m�zda silah�m�z ate�lenmiyor ��nk� start metodu yeterince h�zl� �al��m�yor. bu y�zden bu sat�rdaki e�itleme i�lemlerini yapam�yoruz. �uan oyun ba�lad���nda mermi say�m�z 0 g�z�k�yor.
    {            // OnEnable ve Start hemen hemen ayn� karede �al���yor.
       // currentMagazine = magazine;
    }

    private void Awake()  // ama Awake metodu hepsinden �nce , oyun ba�lad��� andan itibaren ilk karede sadece 1 kez �al���r. genelde de�er atamalar� i�in kullan�l�r.
    {
        currentMagazine = magazine;
    }
    private void OnEnable()
    {
        if (attack != null)
        {
            attack.GetFireTransform = fireTransform;
            attack.GetFireRate = fireRate;
            attack.GetMagazineSize = magazine;
            attack.GetAmmo = currentMagazine;
            attack.GetClipToPlay = clip;
        }
    }
}
