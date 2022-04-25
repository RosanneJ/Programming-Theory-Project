using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const float Gravity = -9.81f;

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
}