using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carpisma : MonoBehaviour
{
    public AudioSource ses_dosyasi;
    public AudioClip temas_sesi;
    public AudioClip sayi_sesi;

    bool renkli;

    yonetici yonet;
    void Start()
    {
        yonet = GameObject.Find("yonetici").GetComponent<yonetici>();
        renkli = false;
        //cizgi = 0;
    }
    // 1 1. oyuncu, 2 2. oyuncu olsun
   
    private void OnCollisionEnter(Collision nesne)
    {
        if(renkli == false)
        {
            if (nesne.gameObject.tag == "delik" && gameObject.tag == "top")
            {
                yonet.topu_belirle(1); // belli değilken düz top atmış olur
                renkli = true;
               // cizgi = 1;
              //  yonet.skor_arttir(5);
                Destroy(gameObject);

            }
            else if (nesne.gameObject.tag == "delik" && gameObject.tag == "cizgili_top")
            {
                yonet.topu_belirle(2); // belli değilken cizgili top atmış olur
                renkli = true;
               // cizgi = 2;
           //     yonet.skor_arttir(6);
                Destroy(gameObject);

            }
            else if (nesne.gameObject.tag == "delik" && gameObject.tag == "Player")
            {
                ses_dosyasi.PlayOneShot(sayi_sesi);
                yonet.beyaz_topu_resetle();

            }
            else if (nesne.gameObject.tag == "delik" && gameObject.tag == "siyah_top")
            {
                ses_dosyasi.PlayOneShot(sayi_sesi);
                yonet.skor_arttir(8);
                Destroy(gameObject);
            }
        }
        if(renkli == true)
        {
            if (nesne.gameObject.tag == "delik" && gameObject.tag == "top")
            {
                yonet.skor_arttir(1); // düz top girdigini belirtelim
                ses_dosyasi.PlayOneShot(sayi_sesi);
                Destroy(gameObject);
            }
            else if (nesne.gameObject.tag == "delik" && gameObject.tag == "cizgili_top")
            {
                yonet.skor_arttir(2); // hangi topun girdigini gonder
                ses_dosyasi.PlayOneShot(sayi_sesi);
                Destroy(gameObject);
            }
            else if (nesne.gameObject.tag == "delik" && gameObject.tag == "Player")
            {
                ses_dosyasi.PlayOneShot(sayi_sesi);
                yonet.beyaz_topu_resetle();
            }
            else if (nesne.gameObject.tag == "delik" && gameObject.tag == "siyah_top")
            {
                ses_dosyasi.PlayOneShot(sayi_sesi);
                yonet.skor_arttir(8);
            }
        }
        
        /*
        if(nesne.gameObject.tag == "top")
        {
            ses_dosyasi.PlayOneShot(temas_sesi);
        }*/

    }
}
