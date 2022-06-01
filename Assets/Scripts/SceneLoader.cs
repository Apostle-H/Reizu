using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button[] levelsBtns;

    private void OnEnable()
    {
        if (levelsBtns != null)
        {
            for (int i = 0; i < levelsBtns.Length; i++)
            {
                int tempI = i;
                levelsBtns[i].onClick.AddListener(() => LoadScene(tempI));
            }
        }
    }

    private void OnDisable()
    {
        if (levelsBtns != null)
        {
            foreach (var levelBtn in levelsBtns)
            {
                levelBtn.onClick.RemoveAllListeners();
            }
        }
    }

    private void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    private void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
