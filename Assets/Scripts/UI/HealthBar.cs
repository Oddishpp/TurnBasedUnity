    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private Personagem personagem;

    private Camera cam;
    private Vector3 offset = new Vector3(0,-1.5f,0);

    public void Configurar(Personagem p)
    {
        personagem = p;
        slider.maxValue = personagem.statusBase.vidaMaxima;
        slider.value = personagem.vidaAtual;
        cam = Camera.main;

        

    }

    private void Update()
    {
        if (personagem != null)
        {

            slider.value = personagem.vidaAtual;
            Vector3 posMundo = personagem.transform.position + offset;
            Vector3 posTela = cam.WorldToScreenPoint(posMundo);
            transform.position = posTela;
        }

    }
}
