using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints; // Array bi�imde bir transform de�i�keni olu�turuyoruz(birden �ok de�er alabilmesi i�in array kulland�k) ve gidece�i noktalar� inspector k�sm�nda g�steriyoruz ki bo� bir obje a��p o noktalar� referans alabilelim.
    [SerializeField] private float speed = 5f; // MoveTowards() metodu kullanaca��m�z i�in h�z de�i�keni kullanmam�z gerek.

    [SerializeField] private bool canMoveRight = false; // Nereye gidece�i konusunda bilgiye ihtiyac�m var. Sa� tarafa gitmiyorsa false olarak kal�p sol tarafa gidecektir. sa� tarafa gidiyorsa da de�i�ken true olacakt�r.
    [SerializeField] private float shootRange = 10f; // raycast kullanaca��m�z i�in menzil belirlememiz gerek.
    [SerializeField] private LayerMask shootLayer;  // hedef layer. 
    private Transform aimTransform;  // D��man�m�z�n silah�n�n ucu.
    private Attack attack;  // Attack scriptindeki propertyleri ve metotlar� bu sctiptte kullanmak i�in bu sat�r� yazd�k.
    private bool isReloaded = false;
    private float reloadTime = 5f;


    private void Awake() // bu t�r �nemli de�er atamalar�n� Awake() metodunun i�inde yap�yoruz.
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

    private bool Aim()  // bu metodu bool yapmam�z laz�m ��nk� yapay zekam�za �unu diyece�iz. E�er ni�an al�yorsan yani aim = true ise ate� et diye bir metod uygulamam�z gerek.
    {
        if (aimTransform ==  null)  // bu if k�sm�n� oyun bittikten sonra restarta bast���m�zda aimTransform hakk�nda null reference hatas� verdi�i i�in yapt�k.Program oyun bittikten sonra aimTransforma ula�am�yor
        {
            aimTransform = attack.GetFireTransform;
        }
        bool hit = Physics.Raycast(aimTransform.position, aimTransform.forward, shootRange, shootLayer); // Oyuncumuza atad���m�z 'Player' layer'�na �arparsa (yani bir objenin fiziksel etiketi player ise) buradaki b�t�n sat�r bize TRUE d�necek.
        Debug.DrawRay(aimTransform.position, aimTransform.forward * shootRange, Color.blue); // �stteki sat�r� g�rselle�tirmek i�in de bu sat�rdaki kodu kullan�yoruz.
        return hit;
    }

    private void MoveTowards()
    {
        if (Aim() && attack.GetAmmo > 0)  //yapay zekan�n hem �n�nde ate� edebilece�i bir�ey varsa hem de kur�unu varsa direkt return yap�yor. b�ylece alttaki if'leri g�rmezden geliyor.
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
        if (Vector3.Distance(transform.position,new Vector3(movePoints[0].position.x, transform.position.y, movePoints[0].position.z)) <= 0.1f) // movePoints[0] sol taraftaki nokta. Karakterimiz bu noktaya geldi�inde art�k sola gidecek yer kalmayacak o y�zden sa�a do�ru gidecek.
        {
            canMoveRight = true;
            print("Move Right");
        }
        else if (Vector3.Distance(transform.position, new Vector3(movePoints[1].position.x, transform.position.y, movePoints[1].position.z)) <= 0.1f) // movePoints[1] sa� taraftaki nokta.
        {
            canMoveRight = false;
            print("Move Left");
        }
    }
    private void LookAtTheTarget(Vector3 newTarget)
    {
        Vector3 newLookPoisiton = new Vector3(newTarget.x, transform.position.y, newTarget.z);
        Quaternion targetRotation = Quaternion.LookRotation(newLookPoisiton - transform.position); // hedef pozisyondan kendi pozisyonumuzu ��kart�yoruz b�ylece aradaki fark� elde ediyoruz.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
