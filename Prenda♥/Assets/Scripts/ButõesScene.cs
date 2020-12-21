using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButõesScene : MonoBehaviour
{
    public void MudarCena()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
