using UnityEngine;

public static class GameObjectLocator
{
    //This script aids in locating game-objects

    public static GameObject FindNearestWithTag(GameObject origin, string targetTag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject nearest = null;
        float minDist = float.MaxValue;
        foreach (GameObject o in objects)
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
