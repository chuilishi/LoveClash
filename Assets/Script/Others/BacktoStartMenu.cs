using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//返回按钮的实现
public class BacktoStartMenu : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {/*
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            Debug.Log("Scene name: " + sceneName);
        }
        // 获取场景索引
        int sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
    */
        // 加载场景
        SceneManager.LoadScene(0);
    }
}
