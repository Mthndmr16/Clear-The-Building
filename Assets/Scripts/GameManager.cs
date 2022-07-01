using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject levelFinishParent;

    // þimdi de oyun bittiðinde oyuncumuzu durduralým.

    private bool levelFinish = false;   // burada oyunun bitip bitmediðine dair bool bir deðiþken atadýk.

    public bool GetLevelFinish
    {
        get
        {
            return levelFinish;
        }
    }   // burada da yukarýda oluþturduðumuz levelFinish deðiþkenini diðer scriptlerde kullanabilmek için bir PROPERTY oluþturduk.

    private Target playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
    }


    void Update()
    {
        // ilk önce düþmöanlarýmýn sayýsýna ihtiyacým var, ne kadar düþman olduðunu bilmem gerekiyor.

        int enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount <= 0 || playerHealth.getHealth <= 0 )
        {
            levelFinishParent.gameObject.SetActive(true);
            levelFinish = true;
        }
        else
        {
            levelFinishParent.gameObject.SetActive(false);
            levelFinish = false;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0); // ilk sahne 
    }
}
