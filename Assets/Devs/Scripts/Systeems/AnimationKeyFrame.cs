using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationKeyFrame : MonoBehaviour
{
   public void EndingGame()
    {
        SceneManager.LoadScene("Start");
    }
}
