using UnityEngine;
using UnityEngine.SceneManagement;

namespace CannonChallenge.Util
{
    /// <summary>
    /// Load game scenes
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {

        public void LoadGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }
        
        public void LoadSummary()
        {
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}