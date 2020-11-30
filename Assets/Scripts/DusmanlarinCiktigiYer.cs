using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanlarinCiktigiYer : MonoBehaviour {

    public GameObject dusmanPrefabi;
    public float genislik;
    public float yukseklik;
    public float yaratmayiGeciktirmeSüresi= 1f;

    private float hiz= 5f;
    private bool sagaHaraket = true;
    private float xmax;
    private float xmin;

	// Use this for initialization
	void Start () {
        float objeIleKameraninZsininFarki = transform.position.z - Camera.main.transform.position.z;
        Vector3 kameraninSolTarafi = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, objeIleKameraninZsininFarki));
        Vector3 kameraninSagTarafi = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, objeIleKameraninZsininFarki));
        xmax = kameraninSagTarafi.x;
        xmin = kameraninSolTarafi.x;
        dusmanlarınTekTekYaratılması();
        
	}
	
    void dusmanlarınYaratılması()
    {
        foreach (Transform cocuk in transform)
        {
            GameObject dusman = Instantiate(dusmanPrefabi, cocuk.transform.position, Quaternion.identity) as GameObject;
            dusman.transform.parent = cocuk;
        }
    }

    void dusmanlarınTekTekYaratılması()
    {
        Transform uygunPozisyon = sonrakiUygunPozisyon();
        if (uygunPozisyon)
        {
            GameObject dusman = Instantiate(dusmanPrefabi, uygunPozisyon.transform.position, Quaternion.identity) as GameObject;
            dusman.transform.parent = uygunPozisyon;
        }

        if (sonrakiUygunPozisyon())
        {
            Invoke("dusmanlarınTekTekYaratılması",yaratmayiGeciktirmeSüresi);
        }
    }


    public void OnDrawGizmos()
    {
        
        Gizmos.DrawWireCube(transform.position,new Vector3(genislik,yukseklik)); 
    }
    
	// Update is called once per frame
	void Update () {

        if (sagaHaraket)
        {
           //transform.position += new Vector3(hiz*Time.deltaTime,0)
            transform.position += Vector3.right * Time.deltaTime * hiz;
        }
        else
        {
            transform.position += Vector3.left * hiz * Time.deltaTime;
        }

        float sagSinir = transform.position.x + genislik/2;
        float solSinir = transform.position.x - genislik/2;

        if (sagSinir > xmax)
        {
            sagaHaraket = false;
            
        }
        else if (solSinir < xmin)
        {
            sagaHaraket = true;
            
        }

        if (ButunDusmanlarOlduMu())
        {
            dusmanlarınTekTekYaratılması();
        }

    }

    Transform sonrakiUygunPozisyon()
    {
        foreach (Transform CocuklarınPozisyonu in transform)
        {
            if (CocuklarınPozisyonu.childCount == 0)
            {
                return CocuklarınPozisyonu;
            }
        }
        return null;
    }

    bool ButunDusmanlarOlduMu()
    {
        foreach(Transform CocuklarınPozisyonu in transform)
        {
            if (CocuklarınPozisyonu.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

}
