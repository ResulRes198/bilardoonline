using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class yonetici : MonoBehaviour
{
    public LineRenderer cizgi;
    public Camera kamera;
    
    public Transform beyaz_top;
    public Rigidbody beyaz_top_guc;

    public Transform cubuk;

    public AudioSource ses_dosyasi;
    public AudioClip temas_sesi;
    public AudioClip sayi_sesi;

    float vurus_hizi = 0.0f;

    Vector3 cubugun_baslangic_koordinati;
    Vector3 beyaz_topun_baslangic_koordinati;

    int oyuncu1_skor = 0;
    int oyuncu2_skor = 0;

    int cizgilitoplar = 0, duztoplar = 0;

    bool oyuncu_degistir = false; // false birinci oyuncu

    public TMPro.TextMeshProUGUI oyuncu_txt;
    public TMPro.TextMeshProUGUI oyuncu_skorlari_txt;
    public TMPro.TextMeshProUGUI kazanan_txt;
    public TMPro.TextMeshProUGUI topsahip_txt;



    void Start()
    {
        beyaz_topun_baslangic_koordinati = beyaz_top.position;
        cubugun_baslangic_koordinati = cubuk.localPosition; // child
    }

    void Update()
    {
        cizgi_ayari();
        fare_kontrol();
        gorunurluk();
    }

    // 1 gelirse düz renkli , 2 gelirse çizgili
     public void topu_belirle(int gelentop)
     {
        oyuncu_degistir = !oyuncu_degistir;
        if(oyuncu_degistir == false && gelentop == 1)
        {
            duztoplar = 1;
            oyuncu1_skor++;
            cizgilitoplar = 2;
            topsahip_txt.text = "Duz Top: Birinci\nCizgili Top: İkinci";
           // oyuncu_degistir = !oyuncu_degistir;
        }
        else if(oyuncu_degistir == false && gelentop == 2)
        {
            duztoplar = 2;
            oyuncu1_skor++;
            cizgilitoplar = 1;
            topsahip_txt.text = "Duz Top: İkinci\nCizgili Top: Birinci";
          //  oyuncu_degistir = !oyuncu_degistir;
        }
        else if (oyuncu_degistir == true && gelentop == 1)
        {
            duztoplar = 2;
            oyuncu2_skor++;
            cizgilitoplar = 1;
            topsahip_txt.text = "Duz Top: İkinci\nCizgili Top: Birinci";
          //  oyuncu_degistir = !oyuncu_degistir;
        }
        else if (oyuncu_degistir == true && gelentop == 2)
        {
            oyuncu2_skor++;
            duztoplar = 1;
            cizgilitoplar = 2;
            topsahip_txt.text = "Duz Top: Birinci\nCizgili Top: İkinci";
            //oyuncu_degistir = !oyuncu_degistir;
        }
    }
    void cizgi_ayari()
    {
        RaycastHit temas;
        Ray isik = kamera.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(isik, out temas))
        {
            Vector3 beyaz_topun_pozisyonu = beyaz_top.position;
            Vector3 farenin_temas_yeri_pozisyonu = new Vector3(temas.point.x, temas.point.y, temas.point.z);

            cizgi.SetPosition(0, beyaz_topun_pozisyonu); // baslangic
            cizgi.SetPosition(1, farenin_temas_yeri_pozisyonu); // bitis

            beyaz_top.LookAt(farenin_temas_yeri_pozisyonu);
        }
    }

    void fare_kontrol()
    {
        if(Input.GetMouseButton(0) && cizgi.gameObject.activeSelf) // basili iken
        {
            if (cubuk.localPosition.z >= -15.0f)
            {
                cubuk.Translate(0, 0, -1.0f * Time.deltaTime);
                vurus_hizi += 35.0f * Time.deltaTime;
            }
        }
        if(Input.GetMouseButtonUp(0) && cizgi.gameObject.activeSelf) // cekince 
        {
            cubuk.localPosition = -cubugun_baslangic_koordinati;
            Invoke("vur", 0.1f);
        }
    }
    
    void vur()
    {
        ses_dosyasi.PlayOneShot(temas_sesi);
        beyaz_top_guc.velocity = beyaz_top.forward * vurus_hizi; // z ye göre

        cizgi.gameObject.SetActive(false); // gorunmez yap
        cubuk.gameObject.SetActive(false);
        vurus_hizi = 0.0f;
        oyuncu_degistir = !oyuncu_degistir; // false ve true arasında git 
    }

    void gorunurluk()
    {
        
        if (beyaz_top_guc.velocity.magnitude <=0.1f && cizgi.gameObject.activeSelf == false)
        {
            cizgi.gameObject.SetActive(true); // topun durmasına yakın geri görünsunler
            cubuk.gameObject.SetActive(true);

            if(oyuncu_degistir == false)
            {
                oyuncu_txt.text = "1. Oyuncu"; // text 
            }
            else
            {
                oyuncu_txt.text = "2. Oyuncu";
            }
        }
    }

    public void skor_arttir(int topturu)
    {
        oyuncu_degistir = !oyuncu_degistir;
        /*
        if (oyuncu_degistir)
        {
            oyuncu2_skor++;
            if(oyuncu2_skor == 8)
            {
                oyunu_bitir("2. Oyuncu Kazandi");
            }
        }
        else
        {
            oyuncu1_skor++;
            if(oyuncu1_skor == 8)
            {
                oyunu_bitir("1. Oyuncu Kazandi");

            }
        }*/
        if(topturu == 1)
        {
            if(oyuncu_degistir == false && duztoplar == 1)
            {
                oyuncu1_skor++;
                oyuncu_skorlari_txt.text = "1. Oyuncu: " + oyuncu1_skor + "\n2. Oyuncu: " + oyuncu2_skor;

                //oyuncu_degistir = !oyuncu_degistir;
            }
            else if( oyuncu_degistir == false && duztoplar ==2)
            {
                oyuncu2_skor++;
                oyuncu_degistir = !oyuncu_degistir;
                oyuncu_skorlari_txt.text = "1. Oyuncu: " + oyuncu1_skor + "\n2. Oyuncu: " + oyuncu2_skor;

            }

        }
        else if(topturu == 2)
        {
            if(oyuncu_degistir == false && cizgilitoplar == 1)
            {
                oyuncu1_skor++;
                oyuncu_skorlari_txt.text = "1. Oyuncu: " + oyuncu1_skor + "\n2. Oyuncu: " + oyuncu2_skor;

                //  oyuncu_degistir = !oyuncu_degistir;
            }
            else if ( oyuncu_degistir == false && cizgilitoplar == 2)
            {
                oyuncu2_skor++;
                oyuncu_skorlari_txt.text = "1. Oyuncu: " + oyuncu1_skor + "\n2. Oyuncu: " + oyuncu2_skor;

                oyuncu_degistir = !oyuncu_degistir;
            }
        }
       /* else if(topturu == 3) // belirli değilen 3. top geldi
        {
            if(oyuncu_degistir == false) // birinci oyuncu attı ise
            {
                duztoplar =1;
                cizgilitoplar = 2;
                oyuncu1_skor++;
                //oyuncu_degistir = !oyuncu_degistir;

            }
            else
            {
                oyuncu2_skor++;
                cizgilitoplar = 1;
                duztoplar = 2;
                //oyuncu_degistir = !oyuncu_degistir;

            }
        }
        else if(topturu == 4) // belirli değilken cizgli top
        {
            if(oyuncu_degistir == false)
            {
                cizgilitoplar = 1;
                oyuncu1_skor++;
                duztoplar = 2;
                //oyuncu_degistir = !oyuncu_degistir;

            }
            else
            {
                cizgilitoplar = 2;
                oyuncu2_skor++;
                duztoplar = 1;
                //oyuncu_degistir = !oyuncu_degistir;

            }
        }*/
        else if(topturu == 8)
        {
            if(oyuncu_degistir == false && oyuncu1_skor == 7) // birinci oyuncuda iken siyah girerse
            {
                oyuncu1_skor++;
                oyunu_bitir("1. Oyuncu Kazandi");
            }
            else if(oyuncu_degistir == false && oyuncu1_skor != 7)
            {
                oyuncu2_skor = 8;
                oyunu_bitir("2. Oyuncu Kazandi");
            }
            else if(oyuncu_degistir == true && oyuncu2_skor == 7)
            {
                oyuncu2_skor++;
                oyunu_bitir("2. Oyuncu Kazandi");
            }
            else if(oyuncu_degistir == true && oyuncu2_skor != 7)
            {
                oyunu_bitir("1. Oyuncu Kazandi");
                oyuncu1_skor = 8;
            }
        }
        //oyuncu_degistir = !oyuncu_degistir;
        oyuncu_skorlari_txt.text = "1. Oyuncu: " + oyuncu1_skor + "\n2. Oyuncu: " + oyuncu2_skor;
    }


    void oyunu_bitir(string kazanan)
    { // parent i aktiv yapınca panel active oluyor
        kazanan_txt.gameObject.transform.parent.gameObject.SetActive(true);
        kazanan_txt.text = kazanan;
        Invoke("tekrar_oyna", 5.0f); // 5sn sonra
    }

    void tekrar_oyna()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void beyaz_topu_resetle()
    {
        beyaz_top_guc.velocity = Vector3.zero;
        beyaz_top.position = beyaz_topun_baslangic_koordinati;
    }
}
