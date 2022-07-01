using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 25;
    public GameObject owner; // burada public bir owner deðiþkeni oluþturduk. ama bu deðiþkenin atamasýný yaomadýk.
    
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime; //bu scriptin takýlý olduðu oyun objesi local deðiþkene göre y ekseninde belirlediðimiz hýza göre transformu deðiþiyor.
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<Target>()== false)  // eðer kurþunumuzun isabet ettiði oyun objesinin içinde 'Target' scripti yoksa kurþunumuzu yok et. (duvar vb.)
        {
            Destroy(gameObject);
        }
    }
}
