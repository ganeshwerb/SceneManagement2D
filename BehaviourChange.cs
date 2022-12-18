using UnityEngine;
using UnityEngine.SceneManagement;

public class BehaviourChange : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject startButton;
    private string activeQuadrant = null;
    private string url;

    private void Start()
    {
        transform.gameObject.SetActive(false);
        url = "https://ganeshwer.jimdosite.com/portfolio/";
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane * 20;
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuCanvas.activeInHierarchy != true)
            {
                startButton.SetActive(false);
                continueButton.SetActive(true);
                menuCanvas.SetActive(true);
                transform.gameObject.SetActive(false);
            }
            else
            {
                menuCanvas.SetActive(false);
            }
        }
        Vector2 mPos = Camera.main.WorldToViewportPoint(transform.position);
        if (mPos.x > 0 && mPos.y > 0 && mPos.x < 1 && mPos.y < 1)
        {
            if (mPos.x < 0.5 && mPos.y < 0.5 && !SceneManager.GetSceneByName("Q4").isLoaded)
            {
                activeQuadrant = "Q4";
                ChangeActiveQuadrant(activeQuadrant);
            }
            else if (mPos.x > 0.5 && mPos.y > 0.5 && !SceneManager.GetSceneByName("Q2").isLoaded)
            {
                activeQuadrant = "Q2";
                ChangeActiveQuadrant(activeQuadrant);
            }
            else if (mPos.x < 0.5 && mPos.y > 0.5 && !SceneManager.GetSceneByName("Q1").isLoaded)
            {
                activeQuadrant = "Q1";
                ChangeActiveQuadrant(activeQuadrant);
            }
            else if (mPos.x > 0.5 && mPos.y < 0.5 && !SceneManager.GetSceneByName("Q3").isLoaded)
            {
                activeQuadrant = "Q3";
                ChangeActiveQuadrant(activeQuadrant);
            }
        }
    }

    public void OnclickExit()
    {
#if PLATFORM_STANDALONE_WIN
        Application.Quit();
#endif

#if UNITY_EDITOR_WIN
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OnClickWebsite()
    {
        Application.OpenURL(url);
    }

    public void OnClickStart()
    {
        menuCanvas.SetActive(false);
        transform.gameObject.SetActive(true);
    }

    public void ChangeActiveQuadrant(string s)
    {
        if (s != null)
        {
            SceneManager.LoadScene(s, LoadSceneMode.Additive);
            int countLoaded = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];

            for (int i = 0; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
                if (loadedScenes[i].name != s && loadedScenes[i].name != "Primary")
                {
                    SceneManager.UnloadSceneAsync(loadedScenes[i]);
                }
            }
        }
    }
}