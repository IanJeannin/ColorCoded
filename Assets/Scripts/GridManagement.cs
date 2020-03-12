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

    private float size = 1.25f; //Size of the objects
    private bool[,] gridArray = new bool[10, 5];  //Create a 2D array for keeping track of grid units
    private GameObject[,] objectGrid = new GameObject[10, 5];
    private float startingXPos = 0f;
    private float startingYPos = 5f;


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
        float xPos = startingXPos; //xPosition of an object
        float yPos = startingYPos; //yPosition of an object

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
                objectGrid[gridArrayRow, gridArrayColumn] = newObject;
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
                    objectGrid[gridArrayRow, gridArrayColumn] = newObject;
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
                objectGrid[gridArrayRow, gridArrayColumn] = newObject;
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

    public void rowRight(int row) //Moves all units in a row right, until there are no blank spaces on the rightmost column
    {
        bool allEmpty=true;
        bool tempBool = false; //Meant to store the rightmost value when rotating
        GameObject tempObject= null; //Meant to store the rightmost object when rotating

        for (int x = 0; x < numberOfColumns ; x++) //Iterate through all grid units in row
        {
            if (gridArray[row, x] == true) //If one of the units has an image
            {
                allEmpty = false; //All Empty is false
            }
        }

        if (allEmpty == false) //As long as at least one unit in the row has an image...
        {
            for (int i = numberOfColumns - 1; i >= 0; i--) //For each unit in the row
            {
                if(i==numberOfColumns-1) //For the object furthest right
                {
                    objectGrid[row, i].transform.position = new Vector3(startingXPos, startingYPos - (row * size));
                    tempObject = objectGrid[row, i];
                    tempBool = gridArray[row, i];
                    objectGrid[row, i] = objectGrid[row, i - 1];
                    gridArray[row, i] = gridArray[row, i - 1];
                }
                else if(i==0) // For the object furthest left
                {
                    objectGrid[row, i].transform.position = new Vector3((i * size) + size, startingYPos - (row * size));
                    objectGrid[row, i] = tempObject;
                    gridArray[row, i] = tempBool;
                }
                else
                {
                    objectGrid[row, i].transform.position = new Vector3((i * size) + size, startingYPos - (row * size));
                    objectGrid[row, i] = objectGrid[row,i-1];
                    gridArray[row, i] = gridArray[row,i-1];
                }
            }
        }
    }

    public void rowLeft(int row) //Moves all units in a row right, until there are no blank spaces on the rightmost column
    {
        bool allEmpty = true;
        bool tempBool = false; //Meant to store the rightmost value when rotating
        GameObject tempObject = null; //Meant to store the rightmost object when rotating

        for (int x = 0; x < numberOfColumns; x++) //Iterate through all grid units in row
        {
            if (gridArray[row, x] == true) //If one of the units has an image
            {
                allEmpty = false; //All Empty is false
            }
        }

        if (allEmpty == false) //As long as at least one unit in the row has an image...
        {
            for (int i = 0; i <= numberOfColumns - 1; i++) //For each unit in the row
            {
                if (i == 0) //For the object furthest left
                {
                    objectGrid[row, i].transform.position = new Vector3(startingXPos+((numberOfColumns*size)-size), startingYPos - (row * size));
                    tempObject = objectGrid[row, i];
                    tempBool = gridArray[row, i];
                    objectGrid[row, i] = objectGrid[row, i + 1];
                    gridArray[row, i] = gridArray[row, i + 1];
                }
                else if (i == numberOfColumns-1) // For the object furthest right
                {
                    objectGrid[row, i].transform.position = new Vector3((i*size)-size, startingYPos - (row * size));
                    objectGrid[row, i] = tempObject;
                    gridArray[row, i] = tempBool;
                }
                else
                {
                    objectGrid[row, i].transform.position = new Vector3((i * size) - size, startingYPos - (row * size));
                    objectGrid[row, i] = objectGrid[row, i + 1];
                    gridArray[row, i] = gridArray[row, i + 1];
                }
            }
        }
    }
}
