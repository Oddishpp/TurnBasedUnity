using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "statusName", menuName = "RPG/Status Personagem")]
public class CharacterStatus : ScriptableObject
{
    public string nome;
    public int ataque;
    public int velocidade;
    public int defesa;
    public float modificadorDefesa;
    public int vidaMaxima;
}



