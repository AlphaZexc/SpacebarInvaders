using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void LoadSelectedScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisableObject(GameObject obj) 
    {
        obj.SetActive(false);
    }

    public void EnableHexagons()
    {
        WordManager.instance.EnableHexagons(true);
    }
}
