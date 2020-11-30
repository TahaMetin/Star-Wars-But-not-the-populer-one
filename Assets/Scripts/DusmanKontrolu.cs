using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DusmanKontrolu : MonoBehaviour {

    public GameObject mermi;
    public float can = 100f;
    public float mermiHizi=8f;
    public float saniyeBasinaMermiAtma = 0.6f;
    public int skorDegeri = 200;
    private SkorKontrolu skorKontrolu;

    public AudioClip AtesSesi;
    public AudioClip OlumSesi;

    private void Start()
    {
        skorKontrolu = GameObject.Find("Skor").GetComponent<SkorKontrolu>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MermiKontrolu carpanMermi = collision.gameObject.GetComponent<MermiKontrolu>();
        if (carpanMermi)
        {
            carpanMermi.carptigindaYokOl();
            can -= carpanMermi.zararVerme();
            if (can <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(OlumSesi, transform.position);
                skorKontrolu.skoruArttır(skorDegeri);
            }
        }
    }


    
	
	// Update is called once per frame
	void Update () {
        float atmaOlasılıgı = Time.deltaTime * saniyeBasinaMermiAtma;
        if (Random.value < atmaOlasılıgı) 
        {
            atesEt();
        }
        
	}

    void atesEt()
    {
        Vector3 baslangicPozisyonu = transform.position + new Vector3(0, -0.8f, 0);
        GameObject dusmanınMermisi = Instantiate(mermi, baslangicPozisyonu, Quaternion.identity) as GameObject;
        dusmanınMermisi.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -mermiHizi);
        AudioSource.PlayClipAtPoint(AtesSesi, transform.position);
    }
}
