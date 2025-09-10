using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager
{
    private Queue<Personagem> fila = new();

    public void Inicializar(List<Personagem>pers)
    {
        var vivosOrdenados = pers
            .Where(p => p.estaVivo)
            .OrderByDescending(p => p.statusBase.velocidade);

        fila = new Queue<Personagem>(vivosOrdenados);
    }

    public Personagem ProximoTurno()
    {
        if (fila.Count == 0)
        {
            return null;
        }
        var prox = fila.Dequeue();

        prox.estaDefendendo = false;

        return prox;
    }

    public bool TemTurnosRestantes() => fila.Count > 0;

    public bool JogoAcabou(List<Personagem> personagens)
    {
        bool temAliado = personagens.Any(p => p.estaVivo && p.time == TimePersonagem.Aliado);
        bool temInimigo = personagens.Any(p => p.estaVivo && p.time == TimePersonagem.Inimigo);

        if (!temAliado || !temInimigo)
        {
            return true;
        }
        return false;
        
    }
    public bool  isVitoria(List<Personagem> personagens)
    {
        bool temAliado = personagens.Any(p => p.estaVivo && p.time == TimePersonagem.Aliado);
        bool temInimigo = personagens.Any(p => p.estaVivo && p.time == TimePersonagem.Inimigo);

        if (temAliado)
        {
            return true;
        }
        else { return false; }
    }
}
