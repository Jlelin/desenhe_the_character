using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerarTerreno : MonoBehaviour
{
    public List<Sprite> terrainSprites; // Lista dos sprites de terreno que você importou
    public GameObject terrainTilePrefab; // Prefab do objeto que representará cada tile do terreno
    public float fractionOfScreen = 1.0f; // Fração da tela para o terreno

    private void Start()
    {
        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        // Obtém a câmera principal
        Camera mainCamera = Camera.main;
        float orthographicSize = mainCamera.orthographicSize;
        float cameraHeight = 2.0f * orthographicSize;
        float aspectRatio = (float)Screen.width / Screen.height;
        float cameraWidth = cameraHeight * aspectRatio;

        // Verifica se a câmera principal existe
        if (mainCamera != null)
        {
            // Obtém a largura e altura da câmera e multiplica pela fração desejada
            float screenWidth = cameraWidth * fractionOfScreen;
            float screenHeight = cameraHeight * fractionOfScreen;

            // Calcula o número de tiles na largura e altura
            int tileCountX = 10; // Altere conforme necessário
            int tileCountY = 10; // Altere conforme necessário

            // Calcula o tamanho de cada tile
            float tileSizeX = screenWidth / tileCountX;
            float tileSizeY = screenHeight / tileCountY;

            // Calcula o espaçamento entre os tiles para evitar sobreposição
            float tileSpacingX = tileSizeX * 0.1f; // Ajuste o valor conforme necessário
            float tileSpacingY = tileSizeY * 0.1f; // Ajuste o valor conforme necessário

            // Crie um objeto pai para os tiles
            GameObject parentTile = new GameObject("ParentTile");
            parentTile.transform.position = new Vector3(-1.27f, -0.72f, 0f); // Defina a posição desejada

            // Loop para criar e posicionar os objetos representando os tiles do terreno
            for (int x = 0; x < tileCountX; x++)
            {
                for (int y = 0; y < tileCountY; y++)
                {
                    GameObject terrainTile = Instantiate(terrainTilePrefab, Vector3.zero, Quaternion.identity);

                    // Faça o tile ser filho do objeto pai
                    terrainTile.transform.parent = parentTile.transform;

                    SpriteRenderer spriteRenderer = terrainTile.GetComponent<SpriteRenderer>();

                    // Define o tamanho do tile
                    terrainTile.transform.localScale = new Vector3(tileSizeX, tileSizeY, 1f);

                    // Calcula a posição considerando o espaçamento entre os tiles
                    float adjustedX = (x * (tileSizeX + tileSpacingX)) - (screenWidth * 0.5f) + (tileSizeX * 0.5f);
                    float adjustedY = (y * (tileSizeY + tileSpacingY)) - (screenHeight * 0.5f) + (tileSizeY * 0.5f);
                    terrainTile.transform.position = new Vector3(adjustedX, adjustedY, 0);

                    // Define a sprite aleatória para o tile
                    int randomSpriteIndex = Random.Range(0, terrainSprites.Count);
                    spriteRenderer.sprite = terrainSprites[randomSpriteIndex];
                }
            }
        }
        else
        {
            Debug.LogError("Câmera principal não encontrada!");
        }
    }
}
