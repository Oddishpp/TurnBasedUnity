using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TargetUIManager : MonoBehaviour
{
    public GameObject painelAlvos;
    public Transform listaAlvosUI;
    public Button botaoAlvoPrefab;
    public Canvas canvas;
  

    private List<Button> poolBotoesAlvo = new();

    public void MostrarAlvos(IEnumerable<Personagem> alvos, Action<Personagem> aoSelecionar)
    {
        foreach (var botao in poolBotoesAlvo)
            botao.gameObject.SetActive(false); // ZERA A QUANTIDADE DE BOTÕES A MOSTRA
        //DEFINIÇÃO DE QUANTIDADE DE BOTÕES
        int i = 0;
        foreach (var alvo in alvos)
        {
            Button botao;
            if (i < poolBotoesAlvo.Count)
                botao = poolBotoesAlvo[i];
            else
            {
                botao = Instantiate(botaoAlvoPrefab, listaAlvosUI);
                poolBotoesAlvo.Add(botao);
            }

            botao.gameObject.SetActive(true);
        //===FIM====================================================

            //PERSONALIZAÇÃO BOTAO (SPRITE ALVO)
            Image imagem = botao.GetComponent<Image>();
            SpriteRenderer spriteRendererAlvo = alvo.gameObject.GetComponent<SpriteRenderer>();

            if (spriteRendererAlvo != null)
            {
                imagem.sprite = spriteRendererAlvo.sprite;
                imagem.preserveAspect = true;   
            }
            Vector2 tamanhoSprite = alvo.GetComponent<SpriteRenderer>().bounds.size * 110f; // multiplicador ajustável
            botao.GetComponent<RectTransform>().sizeDelta = tamanhoSprite;
            //===FIM=====================================================================================================

            //POSIÇÃO NOc MUNDO
            Vector3 posMundo = alvo.transform.position; //Posição do Personagem no mundo
            Vector3 posTela = Camera.main.WorldToScreenPoint(posMundo); // Posição do Personagem na Tela

            Vector2 posLocal; //Local onde o botão irá Spawnar
            RectTransform canvasRect = canvas.GetComponent<RectTransform>(); //Mapa de coordenadas do Canvas (PainelAlvos)
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, posTela, null, out posLocal); // 
            botao.GetComponent<RectTransform>().anchoredPosition = posLocal;
            //===FIM========================================================================================================


            botao.onClick.RemoveAllListeners();
            botao.onClick.AddListener(() => aoSelecionar(alvo));
                

            i++;
        }

        
    }

    public void Esconder() => painelAlvos.SetActive(false);
}
