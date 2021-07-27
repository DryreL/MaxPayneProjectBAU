using UnityEngine;
using System.Collections;

/// <summary>
/// Root level object of patrol that allows PatrolBehavor to globally access and find the closes patrol route to the actor.
/// </summary>
public class PatrolRoot : MonoBehaviour
{
    #region Singleton
    private static PatrolRoot instance;
    public static PatrolRoot Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
    }
    #endregion    
}
