using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class BattleManager : MonoBehaviour
{
    public TextMeshProUGUI TextBox;
    public List<Personagem> personagens; //LISTA DE TODOS ENVOLVIDOS NO COMBATE
    public List<Personagem> aliados; //LISTA DE TODOS ALIADOS NO COMBATE
    public List<Personagem> inimigos; //LISTA DE TODOS INIMIGOS NO COMBATE

    public IconTurnoAtual TurnoAtualIcon;

    //DEFINI플O DE TURNO
    private TurnManager turnos = new(); //GERENCIADOR DE FILA
    private Personagem personagemAtual; //PERSONAGEM ATIVO

    //ESCOLHA DE A합ES 
    public GameObject painelAcoes; //MENU DE A합ES
    public TargetUIManager uiAlvo; //MENU DE ALVOS
    private TipoAcao acaoTemporaria; //A플O ESCOLHIDA

    private List<AcaoCombate> filaDeAcoes = new(); //TODAS A합ES ESCOLHIDAS EM ORDEM DE VELOCIDADE
    public CombatExecutor executor; // EXECUTA AS A합ES
    public SceneController SceneController;
    bool vitoria;




    private void Start()
    {
        personagens = EncontrarTodosOsPersonagens();
        aliados = personagens.Where(p => p.time == TimePersonagem.Aliado).ToList();
        inimigos = personagens.Where(p => p.time == TimePersonagem.Inimigo).ToList();
        turnos.Inicializar(personagens);
        SelecionarProximoTurno();
    }

    List<Personagem> EncontrarTodosOsPersonagens()
    {
        return FindObjectsOfType<Personagem>().ToList();
    }

    void SelecionarProximoTurno()
    {
        if (!turnos.TemTurnosRestantes())
        {
            StartCoroutine(executor.ExecutarAcoes(filaDeAcoes, AoFinalizarRodada));
            return;
        }
        personagemAtual = turnos.ProximoTurno();

        if (personagemAtual.time == TimePersonagem.Inimigo)
        {
            StartCoroutine(EscolherAcaoInimigo(personagemAtual));
        }
        else

        {
            TurnoAtualIcon.PlayerAtual = personagemAtual;
            TurnoAtualIcon.gameObject.SetActive(true);  
            painelAcoes.SetActive(true);
            TextBox.SetText("TURNO ATUAL: " + personagemAtual.statusBase.nome);
        }
    }

    IEnumerator EscolherAcaoInimigo(Personagem inimigo)
    {
        yield return null;

        TipoAcao tipo = TipoAcao.Atacar;

        if(executor.habilidades.TryGetValue(tipo, out var habilidade))
        {
            var alvos = FiltrarAlvos(inimigo, habilidade.TipoDeAlvo);

            if (alvos.Count > 0)
            {
                var alvoEscolhido = alvos[UnityEngine.Random.Range(0, alvos.Count)];

                filaDeAcoes.Add(new AcaoCombate(inimigo, alvoEscolhido, tipo));


            }
        }
        SelecionarProximoTurno();

    }
    public void SelecionarAcao(string tipo)
    {
        TipoAcao tipoEscolhido = (TipoAcao)Enum.Parse(typeof(TipoAcao), tipo);
        acaoTemporaria = tipoEscolhido;
        painelAcoes.SetActive(false);

        if (executor.habilidades.TryGetValue(tipoEscolhido, out var habilidade))
        {
            TipoAlvo tipoDeAlvo = habilidade.TipoDeAlvo;
            var alvosValidos = FiltrarAlvos(personagemAtual,tipoDeAlvo);

            uiAlvo.painelAlvos.SetActive(true);
            uiAlvo.MostrarAlvos(alvosValidos, SelecionarAlvo);
        }
        TextBox.SetText("A플O ESCOLHIDA: " + tipo);
    }
    private List<Personagem> FiltrarAlvos(Personagem origem, TipoAlvo tipoAlvo)
    {
        if (tipoAlvo == TipoAlvo.Inimigo)
        {
            return personagens.Where(p => p != origem && p.estaVivo && p.time != origem.time).ToList();
        }
        if (tipoAlvo == TipoAlvo.Aliado)
        {
            return personagens.Where(p => p.estaVivo && p.time == origem.time).ToList();
        }

        return personagens.Where(p => p.estaVivo).ToList();
    }



    void SelecionarAlvo(Personagem alvo)
    {
        uiAlvo.Esconder();
        filaDeAcoes.Add(new AcaoCombate(personagemAtual, alvo, acaoTemporaria));
        TurnoAtualIcon.gameObject.SetActive(false);
        SelecionarProximoTurno();

        TextBox.SetText("ALVO SELECIONADO: " + alvo.statusBase.nome);
    }

    void AoFinalizarRodada()
    {
        if (turnos.JogoAcabou(personagens))
        {
            TextBox.SetText("Fim da batalha");
            SceneController.TelaFimCombate.SetActive(true);
            if (turnos.isVitoria(personagens))
            {
                SceneController.WinText.SetActive(true);
            }
            else
            {
               SceneController.LoseText.SetActive(true);
            }

                return;
        }

        filaDeAcoes.Clear();
        turnos.Inicializar(personagens);
        SelecionarProximoTurno();
    }

}


