using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    const int X = 50, Y = 30;
    const int SIZE = 25;
    GameObject[,] pixels = new GameObject[X,Y];
    public Sprite pixelSprite;
   
    void Start()
    {
        // Texture2D tex = Resources.Load<Texture2D>("white");
        for (int i = 0; i < X; i++)
        for (int j = 0; j < Y; j++)
        {
            pixels[i, j] = new GameObject("pixel[" + i + "," + j + "]");

            RectTransform trans = pixels[i, j].AddComponent<RectTransform>();
            trans.transform.SetParent(this.transform);
            trans.localScale = Vector3.one;
            trans.anchoredPosition = new Vector2(
                SIZE * (i - (X/2)),
                SIZE * (j - (Y/2))
            );
            trans.sizeDelta = new Vector2(SIZE, SIZE);

            Image image = pixels[i, j].AddComponent<Image>();
            image.sprite = pixelSprite;
            pixels[i, j].transform.SetParent(this.transform);

            pixels[i,j].SetActive(false);
        }

    }

    int generation = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        int new_size = 2;
        int new_x = RandomHandler.NewInt(new_size, X - new_size);
        int new_y = RandomHandler.NewInt(new_size, Y - new_size);
        for (var ii = -new_size; ii <= new_size; ii++)
        for (var jj = -new_size; jj <= new_size; jj++)
        {
            pixels[new_x + ii, new_y + jj].SetActive(true);
        }

        
        generation++;
        if (generation < 2000) {
            var nextGeneration = new bool[X, Y];

            // Loop through every cell 
            for (var row = 1; row < X - 1; row++)
            for (var column = 1; column < Y - 1; column++)
            {
                var neighbor_pixels = 0;
                for (var i = -1; i <= 1; i++)
                for (var j = -1; j <= 1; j++)
                {
                    neighbor_pixels += pixels[row + i, column + j].activeSelf ? 1 : 0;
                }

                var current_pixel = pixels[row, column].activeSelf;
                
                // The cell needs to be subtracted 
                // from its neighbors as it was  
                // counted before 
                neighbor_pixels -= current_pixel ? 1 : 0;

                // Implementing the Rules of Life 
                if (current_pixel == true && neighbor_pixels < 2)
                {
                    nextGeneration[row,column] = false;
                }
                else if (current_pixel == true && neighbor_pixels > 3)
                {
                    // nextGeneration[row, column] = false;
                }
                else if (current_pixel == false && neighbor_pixels == 3)
                {
                    nextGeneration[row,column] = true;
                }
                else
                {
                    nextGeneration[row, column] = current_pixel;
                }
            }
            //Update pixels
            for (var row = 1; row < X - 1; row++)
            for (var column = 1; column < Y - 1; column++)
            {
                pixels[row, column].SetActive(nextGeneration[row, column]);
            }
        }
    }
}