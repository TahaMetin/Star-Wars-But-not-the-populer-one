using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UzayGemimizinKontrolu : MonoBehaviour {

    private float hiz = 10f;
    public float inceAyar = 0.7f;
    public GameObject Mermi;
    public float mermininHizi = 10f;
    public float atesEtmeAraligi = 0.5f;
    public float can = 400f;
    float xmin;
    float xmax;

   

    public AudioClip AtesSesi;
    public AudioClip OlumSesi;

    void Start() {
        float uzaklik = transform.position.z - Camera.main.transform.position.z;
        Vector3 SolUc = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, uzaklik));
        Vector3 SagUc = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, uzaklik));
        xmin = SolUc.x + inceAyar;
        xmax = SagUc.x - inceAyar;

        
    }



    void AtesEtme()
    {
        GameObject GemimizinMermisi1 = Instantiate(Mermi, transform.position - new Vector3(0.35f, -0.9f, 0), Quaternion.identity) as GameObject;
        GameObject GemimizinMermisi2 = Instantiate(Mermi, transform.position + new Vector3(0.35f, 0.9f, 0), Quaternion.identity) as GameObject;
        GemimizinMermisi1.GetComponent<Rigidbody2D>().velocity = new Vector3(0, mermininHizi, 0);
        GemimizinMermisi2.GetComponent<Rigidbody2D>().velocity = new Vector3(0, mermininHizi, 0);
        AudioSource.PlayClipAtPoint(AtesSesi, transform.position);
        //2 kurşun olduğu için her kod 2 kurşun için tekrar yazılımştır.
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("AtesEtme", 0.0000001f, atesEtmeAraligi);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("AtesEtme");
        }
        //geminin x eksenindeki değeri -8 ile 8 arasında ise YeniX i atar
        float YeniX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(YeniX, transform.position.y, transform.position.z);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-hiz * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(hiz * Time.deltaTime, 0, 0);
        }
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
            }
        }

        
    }

   

        


}
