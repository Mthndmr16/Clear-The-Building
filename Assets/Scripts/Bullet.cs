using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 25;
    public GameObject owner; // burada public bir owner de�i�keni olu�turduk. ama bu de�i�kenin atamas�n� yaomad�k.
    
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime; //bu scriptin tak�l� oldu�u oyun objesi local de�i�kene g�re y ekseninde belirledi�imiz h�za g�re transformu de�i�iyor.
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<Target>()== false)  // e�er kur�unumuzun isabet etti�i oyun objesinin i�inde 'Target' scripti yoksa kur�unumuzu yok et. (duvar vb.)
        {
            Destroy(gameObject);
        }
    }
}
