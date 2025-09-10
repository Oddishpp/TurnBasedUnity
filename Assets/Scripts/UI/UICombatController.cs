using System.Collections.Generic;
using UnityEngine;

public class UICombatController : MonoBehaviour
{
    public GameObject healthBarPrefab;
    public GameObject TurnoAtualIcon;
    public Transform canvasUI;

    private Dictionary<Personagem, HealthBar> barrasDeVida = new();

    public void CriarBarras(List<Personagem> personagens)
    {
        foreach (var p in personagens)
        {
            GameObject barraGO = Instantiate(healthBarPrefab, canvasUI);
            HealthBar barra = barraGO.GetComponent<HealthBar>();
            barra.Configurar(p);
            barrasDeVida[p] = barra;
        }
    }

}