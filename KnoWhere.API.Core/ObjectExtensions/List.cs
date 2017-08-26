using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace KnoWhere.API.Core.ObjectExtensions
{
    public static class List
    {
        public static List<T> Shuffle<T>(this List<T> inputList)
        {
            List<T> randomList = new List<T>();
            Random r = new Random();
            while (inputList.Count > 0)
            {
                //Choose a random object in the list
                int randomIndex = r.Next(0, inputList.Count);
                //add it to the new, random list
                randomList.Add(inputList[randomIndex]);
                //remove to avoid duplicates
                inputList.RemoveAt(randomIndex);
            }
            return randomList; //return the new random list
        }
    }
}