using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//���ذ�ť��ʵ��
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
        // ��ȡ��������
        int sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
    */
        // ���س���
        SceneManager.LoadScene(0);
    }
}
