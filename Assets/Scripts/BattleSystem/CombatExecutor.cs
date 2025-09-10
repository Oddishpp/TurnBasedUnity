using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using TMPro;
using System.Globalization;


public class CombatExecutor : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    private string texto;
    public Dictionary<TipoAcao, IHabilidade> habilidades;

    private void Awake()
    {
        habilidades = new()
        {
            { TipoAcao.Atacar, new HabilidadeAtaque() },
            {TipoAcao.Defender, new HabilidadeDefesa() },
            {TipoAcao.Curar, new HabilidadeCura() }
            // adicione mais aqui...
        };
    }

    public IEnumerator ExecutarAcoes(List<AcaoCombate> acoes,  Action onFim)
    {
        acoes = acoes.OrderByDescending(a => a.atacante.statusBase.velocidade).ToList();

        foreach (var acao in acoes)
        {
            if (!acao.atacante.estaVivo)
                continue;

            if (habilidades.TryGetValue(acao.tipo, out var habilidade))
            {
                habilidade.Executar(acao.atacante, acao.alvo, textBox);
            }

            yield return new WaitForSeconds(1f);
        }

        onFim?.Invoke();
    }
}
