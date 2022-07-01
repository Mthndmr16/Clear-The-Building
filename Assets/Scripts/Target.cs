using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField] private GameObject hitFx;
    [SerializeField] private GameObject deadFx;

    [SerializeField] private int maxHealth = 5; // olu�turdu�umuz (box,enemy vb.) can�
    [SerializeField] private int currentHealt; // anl�k can�

    [SerializeField] private AudioClip clipToPlay;

    public int GetHealth
    {
        get
        {
            return currentHealt;
        }
        set
        {
            currentHealt = value;
            if (currentHealt > maxHealth)
            {
                currentHealt = maxHealth;
            }
        }
    }

    public int GetMaximumHealth
    {
        get
        {
            return maxHealth;
        }   
    }



    private void Awake() // bu t�r �nemli de�er atamalar�n� Awake() metodunun i�inde yap�yoruz.
    {
        currentHealt = maxHealth;
    }

    private void OnTriggerEnter(Collider other)  // triggerin �al��mas� i�in (yani kur�unun k�be etki edebilmesi i�in) bu metodu �a��rmam�z gerek.
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>(); //  temas eden objenin i�indeki bullet scriptini'GetComponent<''>()' komutu ile buraya �ekiyoruz. ve ona bu scriptte kolay kullanabilmek i�in ayr�ca bir bullet de�i�keni at�yoruz.
        if (bullet) // yukar�daki i�lemde bullet scriptini buraya �ektik. o y�zden tekrar ayn� kodu 'other.gameObject.GetComponent<Bullet>()' yazmak yerine direkt burada olu�turdu�umuz bullet de�i�kenini atad�k.
        {          
            if (bullet && bullet.owner != gameObject) // bu scriptin tak�l� oldu�u gameObject , mermiyi ate�leyen ki�inin gameObject'i ile ayn� de�ilse bu kodu �al��t�r. bunu yazmaktaki amac�m�z �u : kendi kur�unumuzun bize zarar vermesini istemiyoruz.
            {
                currentHealt -= 1; // kur�un her temas etti�inde can�m�z 1 azalacak.

                AudioSource.PlayClipAtPoint(clipToPlay, transform.position);

                if (hitFx != null && currentHealt > 0)
                {
                   Instantiate(hitFx, transform.position, Quaternion.identity);                 
                }

                if (currentHealt <= 0) // her temas etti�inde can�m�z� kontrol edip Die() metodunu kontrol edecek. can�m�z 0 oldu�u zaman metod �al��acak ve 
                {
                    Die();
                }
                Destroy(other.gameObject); // burada da buraya isabet eden gameObject (kur�un) yok edilecek.
            }
        }
    }
    private void Die()
    {
        if (deadFx != null)
        {
            Instantiate(deadFx, transform.position, Quaternion.identity);          
        }
        Destroy(gameObject);
    }

    // benim �yle bir�ey yapmam laz�m ki , istedi�im zaman bana can de�erimi d�nd�rs�n istedi�im zaman da can�m� art�rs�n. burada da bunu kullanmam�za olanak sa�layan property'ler var.

    public int getHealth // burada kulland���m�z �eyin ad� property. burada getHealt(int value) g�r�nmez bir (int value) var.
    {
        get
        {
            return currentHealt;
        }
        set
        {
            currentHealt = value; // buradaki value'nin anlam� , set'ten gelen de�er. yani getHealth = 5 dersek bizim valuemiz 5 olur.
            if (currentHealt > maxHealth)
            {
                currentHealt = maxHealth;
            }
        }
    }    
}
