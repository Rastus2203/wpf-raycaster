using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayCaster
{
    class game
    {
        public player player;

        public int[,] worldMap;


        public void gameLoop()
        {
            loadMap();
            player = new player();


            while(true)
            {

            }
        }


        public void loadMap()
        {
            // Loads the worldmap from a file, currently one baked into the exe.
            // Then converts the string loaded from the file into a 2d array of ints.

            // This seemed really complicated for what it is, definitely a better way to do it.
            // Could probs be done in python in like 2 lines.

            string mapText = Properties.Resources.simpleTestMap;
            string[] mapTextArray1 = mapText.Split('\n');

            int worldMapSizeX = (mapTextArray1[0].Length + 1) / 2;
            int worldMapSizeY = mapTextArray1.Length;

            int[,] mapTextArray2 = new int[worldMapSizeX, worldMapSizeY];
            for (int i=0; i < mapTextArray1.Length; i++)
            {
                string[] temp = mapTextArray1[i].Split(' ');
                int[] intArray = new int[temp.Length];
                for (int j=0; j < temp.Length; j++)
                {
                    intArray[j] = int.Parse(temp[j]);
                }

                for (int k=0; k < intArray.Length; k++)
                {
                    mapTextArray2[i,k] = intArray[k];
                }
            }

            worldMap = mapTextArray2;

        }
    }
}
