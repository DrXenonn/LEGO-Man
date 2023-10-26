using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 8)
            {
                winPanel!.SetActive(true);
            }

            UnlockNewLevel();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(gameObject);
        }
    }

    private void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 2 < PlayerPrefs.GetInt("ReachedIndex")) return;
        PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("UnlockLevel", PlayerPrefs.GetInt("UnlockLevel", 1) + 1);
        PlayerPrefs.Save();
    }
}