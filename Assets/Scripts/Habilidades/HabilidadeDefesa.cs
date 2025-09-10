using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HabilidadeDefesa : IHabilidade
{
    public TipoAlvo TipoDeAlvo => TipoAlvo.Pessoal;
    public void Executar(Personagem atacante, Personagem alvo, TextMeshProUGUI texto)
    {
        alvo.Defender();
        texto.SetText ($"{atacante.statusBase.nome} está defendendo.");

    }

}
