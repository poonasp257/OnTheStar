using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScript : MonoBehaviour {
    [SerializeField] private Text resultText = null;
    [SerializeField] private Text mileageText = null;
    [SerializeField] private Text consumeText = null;

    private void Start() {
        resultText.text = GameData.Instance.isWin ? "Win" : "Lose";
        mileageText.text = string.Format("{0}M!!", GameData.Instance.mileage);
        consumeText.text = string.Format("X{0}", GameData.Instance.consume);
    }

    public void Restart() {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit() {
        Application.Quit();
    }
}