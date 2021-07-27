using UnityEngine;
using System.Collections;

/// <summary>
/// Extension methods for the transform object. 
/// </summary>
public static class TransformHelper
{   
    public static int IndexOfChild(this Transform t, Transform target)
    {
        if (t != null && target != null)
        {
            if (t.childCount > 0)
            {
                for (int i = 0; i < t.childCount; i++)
                {
                    if (t.GetChild(i) == target)
                        return i;
                }
            }
        }
        return -1;
    }

    public static Transform FindNearestChild(this Transform t, Vector3 pos)
    {
        Transform nearest = null;
        if (t != null && t.childCount > 0)
        {
            float curDist = 0;
            float dist = 100000;

            Transform tmp = null;
            for (int i = 0; i < t.childCount; i++)
            {
                tmp = t.GetChild(i);
                curDist = Vector3.Distance(tmp.position, pos);
                if (curDist < dist)
                {
                    dist = curDist;
                    nearest = tmp;
                }
            }
        }
        return nearest;
    }

    public static T GetComponentInAnyParent<T>(this Transform t) where T : Component
    {
        if(t.parent == null)
            return null; 

        Transform cur = t.parent; 
        while(cur != null)
        {
            T val = cur.GetComponent(typeof(T)) as T;
            if(val != null)
                return val; 
            cur = cur.parent; 
        }
        return null; 
    }
}