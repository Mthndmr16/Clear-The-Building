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

    void Start()  // oyunu bu þekilde baþlattýðýmýzda silahýmýz ateþlenmiyor çünkü start metodu yeterince hýzlý çalýþmýyor. bu yüzden bu satýrdaki eþitleme iþlemlerini yapamýyoruz. þuan oyun baþladýðýnda mermi sayýmýz 0 gözüküyor.
    {            // OnEnable ve Start hemen hemen ayný karede çalýþýyor.
       // currentMagazine = magazine;
    }

    private void Awake()  // ama Awake metodu hepsinden önce , oyun baþladýðý andan itibaren ilk karede sadece 1 kez çalýþýr. genelde deðer atamalarý için kullanýlýr.
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
