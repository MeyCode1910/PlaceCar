using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public bool ilerleme;

    public Transform parent;

    public GameObject[] wheel;

    public GameManager gameManager;

    public GameObject parcPoint;

    bool BaslangicNoktasi = false;
   

    private void Update()
    {      
        if (!BaslangicNoktasi)
        {
            transform.Translate(7f * Time.deltaTime * transform.forward);
        }
        if (ilerleme)
        {
            transform.Translate(15f * Time.deltaTime * transform.forward);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Parking"))
        {
             ArabaTeknkÝslemi();
            transform.SetParent(parent);
            gameManager.YeniArabaGetir();

            //Araba Çarptýðýnda Sabit Kalýr
           // GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY
                //|RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ;

        }  
        if (collision.gameObject.CompareTag("Car"))
        {
            gameManager.CarpmaEfekti.transform.position = parcPoint.transform.position;
            gameManager.CarpmaEfekti.Play();
            ilerleme = false;
            ArabaTeknkÝslemi();
            gameManager.Sesler[3].Play();
            gameManager.Kaybettin();
        } 
    }
    void ArabaTeknkÝslemi()
    {
        ilerleme = false;
        wheel[0].SetActive(false);
        wheel[1].SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BaslangicNoktasý"))
        {
            BaslangicNoktasi = true;
          
        }
         if (other.CompareTag("Elmas"))
        {
            other.gameObject.SetActive(false);
            gameManager.ElmasSayýsi++;
            gameManager.Sesler[1].Play(); 
        }
        if (other.CompareTag("middlePole"))
        {
            gameManager.CarpmaEfekti.transform.position = parcPoint.transform.position;
            gameManager.CarpmaEfekti.Play();
            ilerleme = false;
            ArabaTeknkÝslemi();

            gameManager.Kaybettin();

        }
        if (other.CompareTag("ÖnParking"))
        {
            other.gameObject.GetComponent<ÖnParking>().Parking.SetActive(true);

        }
    }
}
