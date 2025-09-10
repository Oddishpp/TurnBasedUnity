using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Personagem : MonoBehaviour
{
    public TimePersonagem time;

    public CharacterStatus statusBase;

    public int vidaAtual;
    public int defesaBase;
    public float bonusDefesa;
    public bool estaDefendendo;
    public bool estaVivo;
    

    public void Awake()
    {
        vidaAtual = statusBase.vidaMaxima;
        defesaBase = statusBase.defesa;
        bonusDefesa = statusBase.modificadorDefesa;
        estaVivo = true;
    }

    public void TomarDano(int dano)
    {

        int danoFinal = Mathf.Max(dano - statusBase.defesa, 0);
        Debug.Log(danoFinal);
        Debug.Log(estaDefendendo);

        if (estaDefendendo)
        {

            danoFinal = Mathf.RoundToInt(danoFinal *(1f - bonusDefesa));
            Debug.Log(danoFinal);
            
        }

        vidaAtual -= danoFinal;


        if (vidaAtual <= 0)
        {
            Debug.Log("Morreu");
            estaVivo = false;
        }

    
    }

    public void Curar(int quantidade)
    {
        vidaAtual += quantidade;
        if (vidaAtual > statusBase.vidaMaxima)
        {
            vidaAtual = statusBase.vidaMaxima;
        } 
    }

    public void Defender()
    {
        estaDefendendo = true;
    }

}