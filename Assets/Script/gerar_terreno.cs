using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256; // Largura do terreno
    public int height = 256; // Altura do terreno
    private int seed = 0;

    private void Start()
    {
        seed = Random.Range(0, 10000000); // Gera uma semente aleatória entre 0 e 10000
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
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
    }
}

