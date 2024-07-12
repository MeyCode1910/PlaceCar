using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("-------Platform Ayarlarý")]
    public float[] rotationSpeed;

    [Header("-------Araba Ayarlarý")]
    public GameObject[] Arabalar;
    
    public int kacArabaOlsun;
    int AktifIndex = 0;
    int KalanAracSayisiDegeri;

    [Header("-------Canvas Ayarý")]
    public Sprite AracGeldiGorsel;  
    public TextMeshProUGUI KalanAracSayisi;
    public GameObject[] ArabacanvasGörsel;
    public TextMeshProUGUI[] text;
    public GameObject[] Panellerim;
    public GameObject[] TabToButtonlar;


    [Header("--------Platform Ayarý")]
    public GameObject platform1;
    public GameObject platform2;

    [Header("--------Level Ayarý")]
    public int ElmasSayýsi;

    public ParticleSystem CarpmaEfekti;

    public AudioSource[] Sesler;

    public bool YükselecekPlatform = false;

    bool DokunmaKiidi;

    void Start()
    {
        DokunmaKiidi= true;

        VarsayilanDegerleriKontrolEt();

        KalanAracSayisiDegeri = kacArabaOlsun;

       //  KalanAracSayisi.text = KalanAracSayisiDegeri.ToString();


         for (int i = 0; i < kacArabaOlsun; i++)
         {
             ArabacanvasGörsel[i].SetActive(true);
         }
            

    }

    public void YeniArabaGetir()
    {
        
        KalanAracSayisiDegeri--;
        if (AktifIndex<kacArabaOlsun)
        {
            Arabalar[AktifIndex].SetActive(true);
        }
        else
        {
            Kazandin();
        }
        

        ArabacanvasGörsel[AktifIndex - 1].GetComponent<Image>().sprite = AracGeldiGorsel;
      
     //   KalanAracSayisi.text = KalanAracSayisiDegeri.ToString();
    }


    void Update()
    {

        if (Input.touchCount==1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase==TouchPhase.Began)
            {
                if (DokunmaKiidi)
                {
                    Panellerim[0].SetActive(false);
                    Panellerim[3].SetActive(true);
                    DokunmaKiidi=false;
                }
                else
                {
                    Arabalar[AktifIndex].GetComponent<PlayerCar>().ilerleme = true;
                    AktifIndex++;
                }
            }
        }

        platform1.transform.Rotate(new Vector3(0, 0, rotationSpeed[0]), Space.Self);
        if(platform2!=null)
        platform2.transform.Rotate(new Vector3(0, 0, -rotationSpeed[1]), Space.Self);
    }

    public void Kaybettin()
    {
        //Kaybedince elmas toplanmaz
        //PlayerPrefs.SetInt("Elmas", PlayerPrefs.GetInt("Elmas") + ElmasSayýsi);

        text[6].text = PlayerPrefs.GetInt("Elmas").ToString();
        text[7].text = SceneManager.GetActiveScene().name;
        text[8].text = (kacArabaOlsun - KalanAracSayisiDegeri).ToString();
        text[9].text = ElmasSayýsi.ToString();
        Sesler[2].Play();
        Sesler[1].enabled = false;
        

        Panellerim[2].SetActive(true);
        Panellerim[3].SetActive(false);
        Invoke("KaybettinButtonuOrtayaCikart", 2);
       
    }
      
   public void KazandinButtonuOrtayaCikart()//parametre alamaz
    {
        TabToButtonlar[0].SetActive(true);

    }
   public void KaybettinButtonuOrtayaCikart()
    {
        TabToButtonlar[1].SetActive(true);
    }           

    void Kazandin()
    {
        PlayerPrefs.SetInt("Elmas", PlayerPrefs.GetInt("Elmas") + ElmasSayýsi);

        text[2].text = SceneManager.GetActiveScene().name;
        text[3].text = text[6].text = PlayerPrefs.GetInt("Elmas").ToString();
        text[4].text = (kacArabaOlsun - KalanAracSayisiDegeri).ToString();
        text[5].text = ElmasSayýsi.ToString();

        Sesler[2].Play();

        Invoke("KazandinButtonuOrtayaCikart", 2f);
        Panellerim[1].SetActive(true);
        Panellerim[3].SetActive(false);
    }

   //Bellek Yönetimi

     void VarsayilanDegerleriKontrolEt()
    {

        if (!PlayerPrefs.HasKey("Elmas"))
        {
            PlayerPrefs.SetInt("Elmas", 0);
            PlayerPrefs.SetInt("Level", 1);
        }


        text[0].text = PlayerPrefs.GetInt("Elmas").ToString();
        text[1].text = SceneManager.GetActiveScene().name;

    }

   public void izleVeDevamEt()
    {
        //Bu yapýlacak
    }

    public void izleVeDahaFazlaKazan()
    {
        //Bu yapýlacak
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DokunmaKiidi = true;
    }
    public void SonrakiLevel()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }
    public void OyunBaslangic()
    {
        Panellerim[0].SetActive(false);
        Panellerim[3].SetActive(true);
    }

}
