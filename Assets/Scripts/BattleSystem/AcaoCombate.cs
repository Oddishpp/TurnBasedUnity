using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcaoCombate
{
    public Personagem atacante;
    public Personagem alvo;
    public TipoAcao tipo;


    public AcaoCombate(Personagem atacante, Personagem alvo, TipoAcao tipo)
    {
        this.atacante = atacante;
        this.alvo = alvo;
        this.tipo = tipo;
    }

}
