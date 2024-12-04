using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance; // Instance statique, pour pouvoir y acc‚der depuis n'importe quel script
    public AudioSource fxAudio;
    [SerializeField] private AudioSource menuTheme;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private AudioClip loadingSound;

    [SerializeField] private Animator loadingAnimation;
    [SerializeField] private GameObject loadingText;
    void Awake(){
        if(instance == null){ instance = this;} else{ Destroy(gameObject);}
    }
    void Start(){
        Cursor.lockState = CursorLockMode.None;
    }
    public string SceneToLoad;
    public void PlayGame()
    {
        Debug.Log("Loading");
        // Affichage du menu de chargement
        menuScreen.SetActive(false);
        loadingScreen.SetActive(true);
        menuTheme.Stop();
        fxAudio.PlayOneShot(loadingSound);
        StartCoroutine(LoadGame());
    }

    public void SetArrowPos(float y){

    }

    public void QuitGame()
    {
        // Quitter l'application
        Application.Quit();
    }

    IEnumerator LoadGame(){
        AsyncOperation op = SceneManager.LoadSceneAsync(SceneToLoad); // Charge la scŠne en asynchrone 
        op.allowSceneActivation = false;
        StartCoroutine(StartAnimation());
        yield return new WaitForSeconds(2.5f);
        // fade
        StartCoroutine(StartFade(1f));
        yield return new WaitForSeconds(4.5f);
        op.allowSceneActivation = true;
    }

    IEnumerator StartAnimation(){
        loadingAnimation.Play("start");
        yield return new WaitForSeconds(0.183f);
        loadingAnimation.gameObject.SetActive(false);
    }

    IEnumerator StartFade(float duration){
        CanvasRenderer cr = loadingText.GetComponent<CanvasRenderer>();
        float timeElapsed = 0f;

        while(timeElapsed < duration){
            timeElapsed += Time.deltaTime;
            cr.SetAlpha(Mathf.Lerp(1f, 0f, timeElapsed/duration));
            yield return null;
        }
    }
}
