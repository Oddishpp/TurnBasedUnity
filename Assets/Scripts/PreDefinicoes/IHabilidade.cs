using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface IHabilidade {

    TipoAlvo TipoDeAlvo { get;}

    public void Executar(Personagem atacante, Personagem alvo, TextMeshProUGUI texto);
}
