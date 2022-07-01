using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    
    // ate� etme mekani�i i�in bizim 2 temel bilgiye ihtiyac�m�z var.
    [SerializeField] private GameObject ammo; // 1) mermi



     private Transform fireTransform; // 2) merminin nerede olu�aca��.  
     private int maxMagazine = 8; // mermi say�s� 
     private int currentMagazine = 0;
     private float fireRate = 0.5f;  // at�� h�z�
     private AudioClip clipToPlay;
     private AudioSource audioSource;



    [SerializeField] private GameObject[] weapons;
    
    [SerializeField] private bool isPlayer = false;  // bir scripti ba�ka bir oyun objesinde de kullan�yorsak onu ay�rman�n en kolay yolu bool de�i�ken olu�turmakt�r. bu �ekilde inspectordan kullanmak istedi�imiz objeye tik koyar�z.

    public int GetAmmo { get { return currentMagazine; } 
        set { currentMagazine = value; 
            if (currentMagazine > maxMagazine) currentMagazine = maxMagazine; } }    // bu k�s�m property

    private float currentFireRate = 0f;

    public float GetFireRate
    {
        get
        {
           return fireRate;
        }
        set
        {
            fireRate = value;
        }
    }

    public float GetcurrentFireRate
    {
        get
        {
            return currentFireRate;
        }
        set
        {
            currentFireRate = value;
        }
    }   // bu k�s�m property

    public int GetMagazineSize
    {
        get
        {
            return maxMagazine;
        }
        set
        {
            maxMagazine = value;
        }
    }

    public Transform GetFireTransform
    {
        get
        {
            return fireTransform;
        }
        set
        {
            fireTransform = value;
        }
    }

    public AudioClip GetClipToPlay
    {
        get
        {
            return clipToPlay;
        }
        set
        {
            clipToPlay = value;
        }
    }

    private GameManager gameManager ;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime; //at�� h�z�m�z�n s�n�rland�r�lmas� i�in bu sat�rdaki i�lem geriye teker teker sayacak. bu sayede mouse her t�klad���m�zda de�il de fireRate de�i�kenine atad���k�z s�re aral���nda ate� edebilece�iz.
        }
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (isPlayer && !gameManager.GetLevelFinish)  // e�er ki oyun bitmerdiyse.
        {
            if (Input.GetMouseButtonDown(0)) // mouse sol t�k.
            {
                if (currentFireRate <= 0 && currentMagazine > 0)
                {
                    Fire();

                }
            }
            /*if (Input.inputString == "a")
            {

            }*/
            switch (Input.inputString)  // yukar�daki if ile buradaki switch metodu ayn� anlama geliyor. tek fark, switch cabit de�erler al�r. ve daha pratik.
            {                           // buradaki inputString anahtar s�zc�k de string olarak inputlar verebilece�imiz anlam�na geliyor.
                case "1":
                    weapons[1].gameObject.GetComponent<Weapon>().GetWeaponCurrentMagazine = currentMagazine;  // 1 tu�una bast���m�zda 2 deki silah�m�z�n kur�unu tekrar fullenmesin, o andaki kur�unu neyse o �ekilde kals�n.
                    weapons[0].gameObject.SetActive(true);
                    weapons[1].gameObject.SetActive(false);
                    break;
                case "2":
                    weapons[0].gameObject.GetComponent<Weapon>().GetWeaponCurrentMagazine = currentMagazine; // 2 tu�una bast���m�zda 1 deki silah�m�z�n kur�unu tekrar fullenmesin, o andaki kur�unu neyse o �ekilde kals�n.
                    weapons[0].gameObject.SetActive(false);
                    weapons[1].gameObject.SetActive(true);
                    break;
                default:
                    print("Not valid key.");
                    break;

                    // burada �ok silah�m�z varsa for d�ng�s� de yap�labilir.

                   /* for (int i = 0; i < weapons.Length; i++)
                    {
                        weapons[i].gameObject.SetActive(true);
                        weapons[i].gameObject.SetActive(false);
                    }*/                  
            }
        }
    }

    public void Fire()
    {
        float difference = 180f - transform.eulerAngles.y;
        float targetRotation = -90f;

        if (difference >= 90f)
        {
            targetRotation = -90f;
        }
        else if (difference < 90f)
        {
            targetRotation = 90f;
        }
        audioSource.PlayOneShot(clipToPlay);  // Inspector k�sm�ndan ekledi�imiz audioSource componentinin i�ine gir, haz�r olan ses klibini ekle ve bunu oynat. 
        currentMagazine--;
        currentFireRate = fireRate;
        GameObject bulletClone = Instantiate(ammo, fireTransform.position, Quaternion.Euler(0f, 0f, targetRotation)); // Instantiate edilen mermimize bir de�i�ken at�yoruz. 
        bulletClone.GetComponent<Bullet>().owner = gameObject; //  �uanda bu scriptin tak�l� oldu�u oyun objesini, Bullet ad� scriptteki owner slotuna yerle�tirmi� olduk. owner public oldu�u i�in eri�ebildik. Art�k mermiyi kimin ate� etti�ini biliyoruz.
    }
  

}
