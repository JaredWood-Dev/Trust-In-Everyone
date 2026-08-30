using System;
using UnityEngine;

public static class GameObjectLocator
{
    //This script aids in locating game-objects

    public static GameObject FindNearestWithTag(GameObject origin, string targetTag)
    {
        GameObject[] nearObjects = GameObject.FindGameObjectsWithTag(targetTag);

        //add the player when searching for allies
        if (targetTag == "Ally")
        {
             GameObject[] objects = new GameObject[nearObjects.Length + 1];

             for (int i = 0; i < nearObjects.Length; i++)
             {
                 objects[i] = nearObjects[i];
             }
             objects[nearObjects.Length] = GameObject.FindGameObjectWithTag("Player");
             
             nearObjects = objects;
        }
       
        
        GameObject nearest = null;
        float minDist = float.MaxValue;
        foreach (GameObject o in nearObjects)
        {
            float dist = (o.transform.position - origin.transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                nearest = o;
                minDist = dist;
            }
        }
        
        return nearest;
    }
}
