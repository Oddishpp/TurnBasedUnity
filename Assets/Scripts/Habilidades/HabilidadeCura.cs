using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HabilidadeCura : IHabilidade
{

    public TipoAlvo TipoDeAlvo => TipoAlvo.Aliado;
    public void Executar(Personagem atacante, Personagem alvo, TextMeshProUGUI texto)
    {
        int cura = (atacante.statusBase.vidaMaxima / 2);
        alvo.Curar(cura);
        texto.SetText ($"{atacante.statusBase.nome} curou {alvo.statusBase.nome} em {cura} de vida.");
    }

}
