using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainelAcoesVisual : MonoBehaviour
{
    public GameObject explicacao;

    public void AtivaExplicacao()
    {
        explicacao.SetActive(true);
    }
    public void DesativaExplicacao()
    {
        explicacao.SetActive(false);
    }
}
