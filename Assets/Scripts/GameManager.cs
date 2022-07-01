using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject levelFinishParent;

    // �imdi de oyun bitti�inde oyuncumuzu durdural�m.

    private bool levelFinish = false;   // burada oyunun bitip bitmedi�ine dair bool bir de�i�ken atad�k.

    public bool GetLevelFinish
    {
        get
        {
            return levelFinish;
        }
    }   // burada da yukar�da olu�turdu�umuz levelFinish de�i�kenini di�er scriptlerde kullanabilmek i�in bir PROPERTY olu�turduk.

    private Target playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
    }


    void Update()
    {
        // ilk �nce d��m�anlar�m�n say�s�na ihtiyac�m var, ne kadar d��man oldu�unu bilmem gerekiyor.

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
