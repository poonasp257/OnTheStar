using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {
    enum MenuState {
        Play,
        Pause,
    }

    private Image currentImage;
    private MenuState currentState;

    [SerializeField] private GameRoot gameRoot;
    [SerializeField] private Sprite pause;
    [SerializeField] private Sprite play;

    private void Start() {
        currentImage = GetComponent<Image>();
        currentImage.sprite = pause;
        currentState = MenuState.Pause;
    }

    public void OnClick() {
        if(currentState == MenuState.Pause) {
            currentImage.sprite = play;
            currentState = MenuState.Play;
            gameRoot.Pause();
        }
        else {
            currentImage.sprite = pause;
            currentState = MenuState.Pause;
            gameRoot.Play();
        }
    }
}
