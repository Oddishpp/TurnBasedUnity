using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   public GameObject TelaFimCombate;

    public GameObject WinText;
    public GameObject LoseText;
    public int cena;

    public void EncerraLuta()
    {
       SceneManager.LoadScene(cena);
    }

    public void CloseGame()
    {
        Application.Quit(); 
    }

}
