using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiKontrolu : MonoBehaviour {

    public float verdigiZarar = 10f;

    public void carptigindaYokOl()
    {
        Destroy(gameObject);
    }

    public float zararVerme()
    {
        return verdigiZarar;
    }
}
