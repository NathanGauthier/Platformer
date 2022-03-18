using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(Exit());
        }
        
    }



    IEnumerator Exit()
    {
        yield return new WaitForSeconds(2f);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings )
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<SceneRemind>().ResetSceneRemind();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
