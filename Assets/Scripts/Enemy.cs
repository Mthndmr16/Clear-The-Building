using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints; // Array biçimde bir transform deðiþkeni oluþturuyoruz(birden çok deðer alabilmesi için array kullandýk) ve gideceði noktalarý inspector kýsmýnda gösteriyoruz ki boþ bir obje açýp o noktalarý referans alabilelim.
    [SerializeField] private float speed = 5f; // MoveTowards() metodu kullanacaðýmýz için hýz deðiþkeni kullanmamýz gerek.

    [SerializeField] private bool canMoveRight = false; // Nereye gideceði konusunda bilgiye ihtiyacým var. Sað tarafa gitmiyorsa false olarak kalýp sol tarafa gidecektir. sað tarafa gidiyorsa da deðiþken true olacaktýr.
    [SerializeField] private float shootRange = 10f; // raycast kullanacaðýmýz için menzil belirlememiz gerek.
    [SerializeField] private LayerMask shootLayer;  // hedef layer. 
    private Transform aimTransform;  // Düþmanýmýzýn silahýnýn ucu.
    private Attack attack;  // Attack scriptindeki propertyleri ve metotlarý bu sctiptte kullanmak için bu satýrý yazdýk.
    private bool isReloaded = false;
    private float reloadTime = 5f;


    private void Awake() // bu tür önemli deðer atamalarýný Awake() metodunun içinde yapýyoruz.
    {
        attack = GetComponent<Attack>();
        aimTransform = attack.GetFireTransform;
    }

    void Update()
    {
        EnemyAttack();
        CheckCanMoveRight();
        MoveTowards();
        Aim();

    }
    private void Reload()
    {
        attack.GetAmmo = attack.GetMagazineSize;
        isReloaded = false;
        print("Reloaded");
    }

    private void EnemyAttack()
    {
        if (attack.GetAmmo <= 0 && !isReloaded)
        {
            Invoke(nameof(Reload), reloadTime);
            isReloaded = true;
        }
        if (attack.GetcurrentFireRate <= 0f && attack.GetAmmo > 0 && Aim())
        {
            attack.Fire();
        }
    }

    private bool Aim()  // bu metodu bool yapmamýz lazým çünkü yapay zekamýza þunu diyeceðiz. Eðer niþan alýyorsan yani aim = true ise ateþ et diye bir metod uygulamamýz gerek.
    {
        if (aimTransform ==  null)  // bu if kýsmýný oyun bittikten sonra restarta bastýðýmýzda aimTransform hakkýnda null reference hatasý verdiði için yaptýk.Program oyun bittikten sonra aimTransforma ulaþamýyor
        {
            aimTransform = attack.GetFireTransform;
        }
        bool hit = Physics.Raycast(aimTransform.position, aimTransform.forward, shootRange, shootLayer); // Oyuncumuza atadýðýmýz 'Player' layer'ýna çarparsa (yani bir objenin fiziksel etiketi player ise) buradaki bütün satýr bize TRUE dönecek.
        Debug.DrawRay(aimTransform.position, aimTransform.forward * shootRange, Color.blue); // üstteki satýrý görselleþtirmek için de bu satýrdaki kodu kullanýyoruz.
        return hit;
    }

    private void MoveTowards()
    {
        if (Aim() && attack.GetAmmo > 0)  //yapay zekanýn hem önünde ateþ edebileceði birþey varsa hem de kurþunu varsa direkt return yapýyor. böylece alttaki if'leri görmezden geliyor.
        {
            return;
        }
        if (canMoveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[1].position.x, transform.position.y, movePoints[1].position.z), speed * Time.deltaTime);
            LookAtTheTarget(movePoints[1].position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[0].position.x, transform.position.y, movePoints[0].position.z), speed * Time.deltaTime);
            LookAtTheTarget(movePoints[0].position);
        }
    }
    private void CheckCanMoveRight()
    {
        if (Vector3.Distance(transform.position,new Vector3(movePoints[0].position.x, transform.position.y, movePoints[0].position.z)) <= 0.1f) // movePoints[0] sol taraftaki nokta. Karakterimiz bu noktaya geldiðinde artýk sola gidecek yer kalmayacak o yüzden saða doðru gidecek.
        {
            canMoveRight = true;
            print("Move Right");
        }
        else if (Vector3.Distance(transform.position, new Vector3(movePoints[1].position.x, transform.position.y, movePoints[1].position.z)) <= 0.1f) // movePoints[1] sað taraftaki nokta.
        {
            canMoveRight = false;
            print("Move Left");
        }
    }
    private void LookAtTheTarget(Vector3 newTarget)
    {
        Vector3 newLookPoisiton = new Vector3(newTarget.x, transform.position.y, newTarget.z);
        Quaternion targetRotation = Quaternion.LookRotation(newLookPoisiton - transform.position); // hedef pozisyondan kendi pozisyonumuzu çýkartýyoruz böylece aradaki farký elde ediyoruz.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
