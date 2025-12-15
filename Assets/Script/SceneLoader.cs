using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneLoader : MonoBehaviour
{
    // ★ 以下の行を追加
    public string sceneToLoad = "GameScene"; 

    public void LoadMainScene()
    {
        SceneManager.LoadScene(sceneToLoad); 
    }
}