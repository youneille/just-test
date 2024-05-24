using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    public int sceneindex;
    public void LoadLevel(int sceneindex)
    {
        StartCoroutine(LoadAsync(sceneindex));
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LoadAsync(sceneindex));
        }
    }

    IEnumerator LoadAsync(int sceneindex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneindex);
        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = progress * 100 + "%";
            yield return null;
        }
    }
}
