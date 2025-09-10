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
            botao.gameObject.SetActive(false); // ZERA A QUANTIDADE DE BOT�ES A MOSTRA
        //DEFINI��O DE QUANTIDADE DE BOT�ES
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

            //PERSONALIZA��O BOTAO (SPRITE ALVO)
            Image imagem = botao.GetComponent<Image>();
            SpriteRenderer spriteRendererAlvo = alvo.gameObject.GetComponent<SpriteRenderer>();

            if (spriteRendererAlvo != null)
            {
                imagem.sprite = spriteRendererAlvo.sprite;
                imagem.preserveAspect = true;   
            }
            Vector2 tamanhoSprite = alvo.GetComponent<SpriteRenderer>().bounds.size * 110f; // multiplicador ajust�vel
            botao.GetComponent<RectTransform>().sizeDelta = tamanhoSprite;
            //===FIM=====================================================================================================

            //POSI��O NOc MUNDO
            Vector3 posMundo = alvo.transform.position; //Posi��o do Personagem no mundo
            Vector3 posTela = Camera.main.WorldToScreenPoint(posMundo); // Posi��o do Personagem na Tela

            Vector2 posLocal; //Local onde o bot�o ir� Spawnar
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
