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
    [Header("Scale Settings")] // bu kýsýmda cismimizin büyüyüp küçülmesini istiyoruz. bunu animasyon kullanarak yapabiliriz ama kod ile yapmak performans açýsýndan daha iyi.
    [SerializeField] private float period = 2f;  // periyot : iki dalga arasýndaki mesafe.
    [SerializeField] private Vector3 scaleVector; // büyüklük-küçüklük verilerini de vektörler yardýmý ile tutarýz.
    private float scaleFactor; // bu float deðer bizim en sonki sinüs verimizi tutacak.
    private Vector3 startScale;
    [SerializeField] private AudioClip clipToPlay;


    private void Awake() // bu tür önemli deðer atamalarýný Awake() metodunun içinde yapýyoruz.
    {
        startScale = transform.localScale; // bu satýrdaki kod bizim baþlangýç büyüklüðümüzü tutuyor.
         
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
        SinusWave(); // ctrl + . ya basýp enter'a basarsak hýzlýca bir metot oluþturacaktýr.
    }

    private void SinusWave()
    {
        if (period <= 0f)
        {
            period = 0.1f;
        }
        float cycle = Time.timeSinceLevelLoad / period; // neden zamaný periyoda bölüyoruz. çünkü süreklilik istiyoruz. yani oyun devam ettiði sürece bana -1 ila 1 arasýnda deðer dönmesini istiyoruz. bu yüzden zamana bölüyoruz. böylelikle bize sürekli bir deðer dönebilecek.

        const float piX2 = Mathf.PI * 2;

        float sinusWave = Mathf.Sin(cycle * piX2); // buradaki sinüs dalgasý -1 ile 1 arasýnda deðer dönecek. ama biz 0 ile 1 arasýnda döndürmesini istiyoruz çünkü boyut - olursa objemiz yok olur.

        // objenin - boyuta ulaþmasýný önlemek için
        scaleFactor = sinusWave / 2 + 0.5f;  // burada da sinusWave deðiþkenini 2'ye böldük böylece -0.5 ile 0.5 arasýnda deðer verecek. sonra da +0.5 ekledik. artýk 0-1 arasýnda deðer vermiþ olacak.

        Vector3 offSet = scaleFactor * scaleVector; // bunu neden yaptýk . þuanda sinus dalgasý bize 0 ile 1 arasýnda deðer veriyor. ama ben bunu 0-2 veya 0-3 arasýnda deðer dönmesini de isteyebilirim.

        transform.localScale = startScale + offSet;  
    }

    private void OnTriggerEnter(Collider other) // bu metotta da oyuncunun içine girip onun ya canýný ya da kurþununu artýracaðýz.
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
