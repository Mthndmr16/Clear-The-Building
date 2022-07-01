using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody mRigidbody;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 13f;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private Transform[] raycastStartPoints;
    private GameManager gameManager;


    private void Awake()  // bu tür önemli deðer atamalarýný Awake() metodunun içinde yapýyoruz.
    {
        mRigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!gameManager.GetLevelFinish)   // bu if kýsmý, oyun bittiðinde hareket etmememiz için yazýldý. eðer oyun bitmediyse input alacak bittiyse almayacak.
        {
            TakeInput();
            print(OnGroundCheck());
        }     
       
    }
    private void TakeInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGroundCheck())
        {
            mRigidbody.velocity = new Vector3(mRigidbody.velocity.x, Mathf.Clamp((jumpPower * 300) * Time.deltaTime, 0f, 15f), 0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            mRigidbody.velocity = new Vector3(Mathf.Clamp((speed * 300) * Time.deltaTime, 0f, 10f), mRigidbody.velocity.y, 0f);
            //  transform.rotation = Quaternion.Euler(0f, 90f, 0f); Sola doðru keskin dönüþ.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 89.99f, 0f), turnSpeed * Time.deltaTime);


        }
        else if (Input.GetKey(KeyCode.D))
        {
            mRigidbody.velocity = new Vector3(Mathf.Clamp((-speed * 300) * Time.deltaTime, -10f, 0f), mRigidbody.velocity.y, 0f);
            // transform.rotation = Quaternion.Euler(0f, -90f, 0f); Saða doðru keskin dönüþ.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, -89.99f, 0f), turnSpeed * Time.deltaTime);

        }
        else
        {
            mRigidbody.velocity = new Vector3(0f, mRigidbody.velocity.y, 0f);
        }
    }

    private bool OnGroundCheck()
    {
        bool hit = false;

        for (int i = 0; i < raycastStartPoints.Length; i++)
        {
            hit = Physics.Raycast(raycastStartPoints[i].position, -raycastStartPoints[i].transform.up, 0.5f);
            Debug.DrawRay(raycastStartPoints[i].position, -raycastStartPoints[i].transform.up * 0.5f, Color.red);
        }

        if (hit)
        {           
            return true;
        }
        else
        {
            return false;
        }       
    }
}
