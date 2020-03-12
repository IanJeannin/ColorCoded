using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagement : MonoBehaviour
{
    [SerializeField]
    private int numberOfRows;
    [SerializeField]
    private int numberOfColumns;
    [SerializeField]
    private GameObject sampleObject;
    [SerializeField]
    private int numberOfStartingRows;

    float size = 1.25f; //Size of the objects
    private bool[,] gridArray = new bool[10, 5];  //Create a 2D array for keeping track of grid units


    private void Start()
    {
         CreateGrid();
        CheckArray();
    }

    private void CreateGrid()
    {
        int numberOfGridUnits = numberOfRows * numberOfColumns;
        GameObject newObject; //Create Instance of Game Object
        int gridArrayRow = 0; //For selecting row of gridArray
        int gridArrayColumn = 0; //For selecting the column of the gridArray
        int numberOfRowsFilled=0;
        float xPos = 0f; //xPosition of an object
        float yPos = 5f; //yPosition of an object

        for (int i = 0; i < numberOfGridUnits; i++)
        {
            //Creates blank objects in every position
            newObject = Instantiate(sampleObject);
            newObject.transform.position = new Vector3(xPos, yPos, 5); //Sets object to proper position

            if ((i + 1) % numberOfColumns != 0 && numberOfRowsFilled<=numberOfStartingRows) //If square is not in last column and there are still unused chosen images, fill the grid unit with an image
            {
                newObject = Instantiate(sampleObject, transform); //Creates objects to fill grid
                newObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV(); //Gives Square random color
                newObject.transform.position = new Vector2(xPos, yPos); //Sets object to proper position
                //==========================================================================
                xPos += size;
                gridArray[gridArrayRow, gridArrayColumn] = true; //Sets gridArray index as true
                gridArrayColumn++;
            }
            else if ((i + 1) % numberOfColumns == 0)//If Square is in last column
            {
                if (numberOfRowsFilled <= numberOfStartingRows)
                {
                    newObject = Instantiate(sampleObject, transform); //Creates objects to fill grid
                    newObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV(); //Gives Square random color
                    newObject.transform.position = new Vector2(xPos, yPos); //Sets object to proper position
                    gridArray[gridArrayRow, gridArrayColumn] = true; //Sets gridArray index as false
                }
                xPos = 0;
                gridArrayColumn = 0; //Sets gridArrayColumn back to 0
                gridArrayRow++; //Increases the gridArray row by 1
                yPos -= size;
                numberOfRowsFilled++;
            }
            else
            {
                gridArray[gridArrayRow, gridArrayColumn] = false; //Sets gridArray index as false
                if ((i + 1) %numberOfColumns != 0) //If square is not in last column
                {
                    xPos += size; //Moves next image to adjacent space
                    gridArrayColumn++; //Increases gridArray column by 1
                }
                else if ((i + 1) % numberOfColumns == 0)//If Square is in last column
                {
                    yPos -= size; //Moves next image to the next row
                    gridArrayColumn = 0; //Sets gridArrayColumn back to 0
                    gridArrayRow++; //Increases the gridArray row by 1
                }

            }

        }

    }

    private void CheckArray() //Method to check array is functioning properly using Debug.Log
    {
        string arrayString = "";

        for (int b = 0; b < numberOfRows; b++)
        {
            for (int c = 0; c < numberOfColumns; c++)
            {
                arrayString += string.Format("{0}", gridArray[b, c]); //Add each unit in a single row
            }
            arrayString += System.Environment.NewLine; //Start a new line for a new row
        }
        Debug.Log(arrayString);
    }
}
