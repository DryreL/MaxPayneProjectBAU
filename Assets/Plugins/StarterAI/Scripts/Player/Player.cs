using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    #region Singleton
    private static Player instance;
    public static Player Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
    }
    #endregion  
}
