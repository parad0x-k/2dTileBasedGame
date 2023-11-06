using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
         if (SceneBehaviour.Instance != null)
        {
            StartCoroutine(SceneBehaviour.Instance.LoadNextScene());
        }
        else
        {
            Debug.LogError("SceneBehaviour.Instance is not set.");
        }
    }
}