using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Health Settings")]
    public bool healthPowerUp = false;
    public int healthAmount = 1;
    [Header("Ammo Settings")]
    public bool ammoPowerUp = false;
    public int ammoAmount = 5;
    [Header("Transform Settings")]
    [SerializeField] private Vector3 turnVector = Vector3.zero;
    [Header("Scale Settings")] // bu k�s�mda cismimizin b�y�y�p k���lmesini istiyoruz. bunu animasyon kullanarak yapabiliriz ama kod ile yapmak performans a��s�ndan daha iyi.
    [SerializeField] private float period = 2f;  // periyot : iki dalga aras�ndaki mesafe.
    [SerializeField] private Vector3 scaleVector; // b�y�kl�k-k���kl�k verilerini de vekt�rler yard�m� ile tutar�z.
    private float scaleFactor; // bu float de�er bizim en sonki sin�s verimizi tutacak.
    private Vector3 startScale;
    [SerializeField] private AudioClip clipToPlay;


    private void Awake() // bu t�r �nemli de�er atamalar�n� Awake() metodunun i�inde yap�yoruz.
    {
        startScale = transform.localScale; // bu sat�rdaki kod bizim ba�lang�� b�y�kl���m�z� tutuyor.
         
    }

    void Start()
    {
       
        if (healthPowerUp == true && ammoPowerUp == true)
        {
            healthPowerUp = false;
            ammoPowerUp = false;
        }
        else if (healthPowerUp == true)
        {
            ammoPowerUp = false;
        }
        else if (ammoPowerUp)
        {
            healthPowerUp = false;
        }
    }
    void Update()
    {
        transform.Rotate(turnVector);
        SinusWave(); // ctrl + . ya bas�p enter'a basarsak h�zl�ca bir metot olu�turacakt�r.
    }

    private void SinusWave()
    {
        if (period <= 0f)
        {
            period = 0.1f;
        }
        float cycle = Time.timeSinceLevelLoad / period; // neden zaman� periyoda b�l�yoruz. ��nk� s�reklilik istiyoruz. yani oyun devam etti�i s�rece bana -1 ila 1 aras�nda de�er d�nmesini istiyoruz. bu y�zden zamana b�l�yoruz. b�ylelikle bize s�rekli bir de�er d�nebilecek.

        const float piX2 = Mathf.PI * 2;

        float sinusWave = Mathf.Sin(cycle * piX2); // buradaki sin�s dalgas� -1 ile 1 aras�nda de�er d�necek. ama biz 0 ile 1 aras�nda d�nd�rmesini istiyoruz ��nk� boyut - olursa objemiz yok olur.

        // objenin - boyuta ula�mas�n� �nlemek i�in
        scaleFactor = sinusWave / 2 + 0.5f;  // burada da sinusWave de�i�kenini 2'ye b�ld�k b�ylece -0.5 ile 0.5 aras�nda de�er verecek. sonra da +0.5 ekledik. art�k 0-1 aras�nda de�er vermi� olacak.

        Vector3 offSet = scaleFactor * scaleVector; // bunu neden yapt�k . �uanda sinus dalgas� bize 0 ile 1 aras�nda de�er veriyor. ama ben bunu 0-2 veya 0-3 aras�nda de�er d�nmesini de isteyebilirim.

        transform.localScale = startScale + offSet;  
    }

    private void OnTriggerEnter(Collider other) // bu metotta da oyuncunun i�ine girip onun ya can�n� ya da kur�ununu art�raca��z.
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(clipToPlay, transform.position); 
            if (healthPowerUp)
            {
                other.gameObject.GetComponent<Target>().getHealth += healthAmount;
            }
            else if (ammoPowerUp)
            {
                other.gameObject.GetComponent<Attack>().GetAmmo += ammoAmount;
            }
            Destroy(gameObject);
        }

    }
}
