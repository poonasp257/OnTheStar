using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {
    private Transform currentTutorial = null;
    private int currentIndex = 0;

    [SerializeField] private GameObject tutorialList;

    void Update() {
        if (Input.anyKeyDown) {
            if (!tutorialList.activeSelf) {
                tutorialList.SetActive(true);
            }
            
            int numOfTutorials = tutorialList.transform.childCount;
            if (numOfTutorials <= currentIndex) { 
                SceneManager.LoadScene("GameScene");
                return;
            }

            var tutorial = tutorialList.transform.GetChild(currentIndex++);
            currentTutorial?.gameObject.SetActive(false);
            currentTutorial = tutorial;
            currentTutorial.gameObject.SetActive(true);
        }
    }
}
