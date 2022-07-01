using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField] private GameObject hitFx;
    [SerializeField] private GameObject deadFx;

    [SerializeField] private int maxHealth = 5; // oluþturduðumuz (box,enemy vb.) caný
    [SerializeField] private int currentHealt; // anlýk caný

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



    private void Awake() // bu tür önemli deðer atamalarýný Awake() metodunun içinde yapýyoruz.
    {
        currentHealt = maxHealth;
    }

    private void OnTriggerEnter(Collider other)  // triggerin çalýþmasý için (yani kurþunun kübe etki edebilmesi için) bu metodu çaðýrmamýz gerek.
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>(); //  temas eden objenin içindeki bullet scriptini'GetComponent<''>()' komutu ile buraya çekiyoruz. ve ona bu scriptte kolay kullanabilmek için ayrýca bir bullet deðiþkeni atýyoruz.
        if (bullet) // yukarýdaki iþlemde bullet scriptini buraya çektik. o yüzden tekrar ayný kodu 'other.gameObject.GetComponent<Bullet>()' yazmak yerine direkt burada oluþturduðumuz bullet deðiþkenini atadýk.
        {          
            if (bullet && bullet.owner != gameObject) // bu scriptin takýlý olduðu gameObject , mermiyi ateþleyen kiþinin gameObject'i ile ayný deðilse bu kodu çalýþtýr. bunu yazmaktaki amacýmýz þu : kendi kurþunumuzun bize zarar vermesini istemiyoruz.
            {
                currentHealt -= 1; // kurþun her temas ettiðinde canýmýz 1 azalacak.

                AudioSource.PlayClipAtPoint(clipToPlay, transform.position);

                if (hitFx != null && currentHealt > 0)
                {
                   Instantiate(hitFx, transform.position, Quaternion.identity);                 
                }

                if (currentHealt <= 0) // her temas ettiðinde canýmýzý kontrol edip Die() metodunu kontrol edecek. canýmýz 0 olduðu zaman metod çalýþacak ve 
                {
                    Die();
                }
                Destroy(other.gameObject); // burada da buraya isabet eden gameObject (kurþun) yok edilecek.
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

    // benim öyle birþey yapmam lazým ki , istediðim zaman bana can deðerimi döndürsün istediðim zaman da canýmý artýrsýn. burada da bunu kullanmamýza olanak saðlayan property'ler var.

    public int getHealth // burada kullandýðýmýz þeyin adý property. burada getHealt(int value) görünmez bir (int value) var.
    {
        get
        {
            return currentHealt;
        }
        set
        {
            currentHealt = value; // buradaki value'nin anlamý , set'ten gelen deðer. yani getHealth = 5 dersek bizim valuemiz 5 olur.
            if (currentHealt > maxHealth)
            {
                currentHealt = maxHealth;
            }
        }
    }    
}
