using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UICombatController uiController;

    void Start()
    {
        List<Personagem> personagens = FindObjectsOfType<Personagem>().ToList();
        uiController.CriarBarras(personagens);
    }
}
