using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelection : MonoBehaviour
{
    [SerializeField] private Button[] levels;
    [SerializeField] private GameObject levelButtons;
    private void Awake() {

        ButtonToArray();

        int unlockLevel =  PlayerPrefs.GetInt("UnlockLevel",1);
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].interactable = false;
        }
        for (int i = 0; i < unlockLevel; i++)
        {
            levels[i].interactable = true;
        }
    }
    public void OpenLevel(int indexLevel){
        string levelName = "Level";
        SceneManager.LoadSceneAsync(levelName + indexLevel);
    }
    private void ButtonToArray(){
        int childCount = levelButtons.transform.childCount;
        levels = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            levels[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
