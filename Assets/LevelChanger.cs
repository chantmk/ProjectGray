using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    // Update is called once per frame
    void Update()
    {
    }
    

    public void OnFadeComplete()
    {
        //SceneManager.LoadScene(levelToLoad);
    }
}
