using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tamanho_total_tela : MonoBehaviour
{
    void Start()
    {
        int screenWidth = Screen.width; // Largura da tela em pixels
        int screenHeight = Screen.height; // Altura da tela em pixels
        Debug.Log("Resolução da Tela: " + screenWidth + "x" + screenHeight + " pixels");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
