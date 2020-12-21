using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinaBolas : MonoBehaviour
{
    public Transform cesto;
    public Text chovemDinas, fugir;
    public Image text2, text3;
    public AudioSource itsRaining;

    private void Start()
    {
        chovemDinas.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
        text3.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            Cursor.visible = false;
            chovemDinas.gameObject.SetActive(true);
            text2.gameObject.SetActive(true);
            text3.gameObject.SetActive(true);
            fugir.gameObject.SetActive(true);
            itsRaining.Play();
            Destroy(cesto.gameObject);
        }
    }
}
