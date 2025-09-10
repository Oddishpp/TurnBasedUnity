using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconTurnoAtual : MonoBehaviour
{
    private Camera cam;
    public Personagem PlayerAtual;
    public Vector3 offset = new Vector3 (0.6f, -1.3f, 0);

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if(PlayerAtual != null)
        {
            TrocaPosicao(PlayerAtual);
        }
        
    }

    public void TrocaPosicao(Personagem personagem)
    {
        Vector3 posMundo = personagem.transform.position + offset;
        Vector3 posTela = cam.WorldToScreenPoint(posMundo);
        transform.position = posTela;
    }
}
