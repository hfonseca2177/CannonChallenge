using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CannonChallenge.Util
{
    /// <summary>
    /// Load game scenes
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {

        private const string _summarySceneName = "Summary"; 
        public void LoadMenu()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
        
        public void LoadGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

        public void LoadMoonGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: 3);
        }

        public void LoadDotsShowCase()
        {
            SceneManager.LoadScene(sceneBuildIndex: 4);
        }

        public void LoadSceneByIndex(int index)
        {
            SceneManager.LoadScene(sceneBuildIndex: index);
        }

        public void LoadSummaryAdditive()
        {
            StartCoroutine(LoadAdditiveSceneAsync(_summarySceneName));
        }

        public void UnloadSummary()
        {
            StartCoroutine(UnloadSceneAsync(_summarySceneName));
        }
        
        //load async additive scene
        private IEnumerator LoadAdditiveSceneAsync(string sceneName)
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return load;
        }
        
        //unload async scene
        private IEnumerator UnloadSceneAsync(string sceneName)
        {
            AsyncOperation unload = SceneManager.UnloadSceneAsync(sceneName);
            yield return unload;
        }


        public void QuitGame()
        {
            Application.Quit();
        }
    }
}