using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private int seed = 0;

    private void Start()
    {
        seed = Random.Range(0, 10000000); // Gera uma semente aleatória entre 0 e o valor escolhido
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        // Obtém a altura e largura da tela em pixels
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        float aspectRatio = screenWidth / screenHeight;

        float percentOfScreenWidth = 1f; // Define o terreno como 80% da largura da tela
        int width = Mathf.RoundToInt(screenWidth * percentOfScreenWidth);
        int height = Mathf.RoundToInt(width / aspectRatio); // Mantendo a proporção

        // Cria um novo objeto de textura para o terreno
        Texture2D terrainTexture = new Texture2D(width, height);

        // Percorre cada pixel da textura e define sua cor baseada no ruído
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * seed;
                float yCoord = (float)y / height * seed;
                float sample = Mathf.PerlinNoise(xCoord, yCoord); // Gera ruído Perlin

                terrainTexture.SetPixel(x, y, new Color(sample, sample, sample));
            }
        }

        // Aplica os pixels à textura
        terrainTexture.Apply();

        // Exibe a textura como um Sprite na cena
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(terrainTexture, new Rect(0, 0, width, height), Vector2.zero);

        // Obtém o tamanho atual do sprite na cena
        float spriteWidth = spriteRenderer.bounds.size.x;

        // Calcula a escala necessária para ocupar valor% da largura da tela
        float targetSpriteWidth = screenWidth * percentOfScreenWidth;
        float targetScaleX = targetSpriteWidth / spriteWidth;
        float targetScaleY = targetScaleX; // Mantém a proporção

        // Define a escala do objeto para ocupar 80% da largura da tela
        transform.localScale = new Vector3(targetScaleX, targetScaleY, 1f);
    }
}
