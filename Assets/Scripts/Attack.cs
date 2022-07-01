using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    
    // ateþ etme mekaniði için bizim 2 temel bilgiye ihtiyacýmýz var.
    [SerializeField] private GameObject ammo; // 1) mermi



     private Transform fireTransform; // 2) merminin nerede oluþacaðý.  
     private int maxMagazine = 8; // mermi sayýsý 
     private int currentMagazine = 0;
     private float fireRate = 0.5f;  // atýþ hýzý
     private AudioClip clipToPlay;
     private AudioSource audioSource;



    [SerializeField] private GameObject[] weapons;
    
    [SerializeField] private bool isPlayer = false;  // bir scripti baþka bir oyun objesinde de kullanýyorsak onu ayýrmanýn en kolay yolu bool deðiþken oluþturmaktýr. bu þekilde inspectordan kullanmak istediðimiz objeye tik koyarýz.

    public int GetAmmo { get { return currentMagazine; } 
        set { currentMagazine = value; 
            if (currentMagazine > maxMagazine) currentMagazine = maxMagazine; } }    // bu kýsým property

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
    }   // bu kýsým property

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
            currentFireRate -= Time.deltaTime; //atýþ hýzýmýzýn sýnýrlandýrýlmasý için bu satýrdaki iþlem geriye teker teker sayacak. bu sayede mouse her týkladýðýmýzda deðil de fireRate deðiþkenine atadýðýkýz süre aralýðýnda ateþ edebileceðiz.
        }
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (isPlayer && !gameManager.GetLevelFinish)  // eðer ki oyun bitmerdiyse.
        {
            if (Input.GetMouseButtonDown(0)) // mouse sol týk.
            {
                if (currentFireRate <= 0 && currentMagazine > 0)
                {
                    Fire();

                }
            }
            /*if (Input.inputString == "a")
            {

            }*/
            switch (Input.inputString)  // yukarýdaki if ile buradaki switch metodu ayný anlama geliyor. tek fark, switch cabit deðerler alýr. ve daha pratik.
            {                           // buradaki inputString anahtar sözcük de string olarak inputlar verebileceðimiz anlamýna geliyor.
                case "1":
                    weapons[1].gameObject.GetComponent<Weapon>().GetWeaponCurrentMagazine = currentMagazine;  // 1 tuþuna bastýðýmýzda 2 deki silahýmýzýn kurþunu tekrar fullenmesin, o andaki kurþunu neyse o þekilde kalsýn.
                    weapons[0].gameObject.SetActive(true);
                    weapons[1].gameObject.SetActive(false);
                    break;
                case "2":
                    weapons[0].gameObject.GetComponent<Weapon>().GetWeaponCurrentMagazine = currentMagazine; // 2 tuþuna bastýðýmýzda 1 deki silahýmýzýn kurþunu tekrar fullenmesin, o andaki kurþunu neyse o þekilde kalsýn.
                    weapons[0].gameObject.SetActive(false);
                    weapons[1].gameObject.SetActive(true);
                    break;
                default:
                    print("Not valid key.");
                    break;

                    // burada çok silahýmýz varsa for döngüsü de yapýlabilir.

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
        audioSource.PlayOneShot(clipToPlay);  // Inspector kýsmýndan eklediðimiz audioSource componentinin içine gir, hazýr olan ses klibini ekle ve bunu oynat. 
        currentMagazine--;
        currentFireRate = fireRate;
        GameObject bulletClone = Instantiate(ammo, fireTransform.position, Quaternion.Euler(0f, 0f, targetRotation)); // Instantiate edilen mermimize bir deðiþken atýyoruz. 
        bulletClone.GetComponent<Bullet>().owner = gameObject; //  þuanda bu scriptin takýlý olduðu oyun objesini, Bullet adý scriptteki owner slotuna yerleþtirmiþ olduk. owner public olduðu için eriþebildik. Artýk mermiyi kimin ateþ ettiðini biliyoruz.
    }
  

}
