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
             ArabaTeknk�slemi();
            transform.SetParent(parent);
            gameManager.YeniArabaGetir();

            //Araba �arpt���nda Sabit Kal�r
           // GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY
                //|RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ;

        }  
        if (collision.gameObject.CompareTag("Car"))
        {
            gameManager.CarpmaEfekti.transform.position = parcPoint.transform.position;
            gameManager.CarpmaEfekti.Play();
            ilerleme = false;
            ArabaTeknk�slemi();
            gameManager.Sesler[3].Play();
            gameManager.Kaybettin();
        } 
    }
    void ArabaTeknk�slemi()
    {
        ilerleme = false;
        wheel[0].SetActive(false);
        wheel[1].SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BaslangicNoktas�"))
        {
            BaslangicNoktasi = true;
          
        }
         if (other.CompareTag("Elmas"))
        {
            other.gameObject.SetActive(false);
            gameManager.ElmasSay�si++;
            gameManager.Sesler[1].Play(); 
        }
        if (other.CompareTag("middlePole"))
        {
            gameManager.CarpmaEfekti.transform.position = parcPoint.transform.position;
            gameManager.CarpmaEfekti.Play();
            ilerleme = false;
            ArabaTeknk�slemi();

            gameManager.Kaybettin();

        }
        if (other.CompareTag("�nParking"))
        {
            other.gameObject.GetComponent<�nParking>().Parking.SetActive(true);

        }
    }
}
