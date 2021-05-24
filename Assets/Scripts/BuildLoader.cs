using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildLoader : MonoBehaviour
{

    public string startScene;

    // Start is called before the first frame update
    void Awake()
    {
        if(!Application.isEditor){
            LoadPersistent();
        }
    }

    private void LoadPersistent(){
       Debug.Log(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync("Scenes/" + startScene, LoadSceneMode.Additive);
    }

}
