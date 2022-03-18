using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRemind : MonoBehaviour
{
    void Awake()
    {
        int numSceneRemind = FindObjectsOfType<SceneRemind>().Length;
        if (numSceneRemind > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetSceneRemind()
    {
        Destroy(gameObject);
    }
}
