using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HabilidadeAtaque : IHabilidade
{

    public TipoAlvo TipoDeAlvo => TipoAlvo.Inimigo;
    public void Executar(Personagem atacante, Personagem alvo, TextMeshProUGUI texto)
    {
        if (alvo.estaVivo)
        {
            int dano = atacante.statusBase.ataque;
            alvo.TomarDano(dano);
            texto.SetText ($"{atacante.statusBase.nome} atacou {alvo.statusBase.nome} causando {dano} de dano.");
        }
    }
}
