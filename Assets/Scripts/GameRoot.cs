using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameRoot : MonoBehaviour {
    public LevelControl level_control = null;
    private PlayerControl player = null;
    private CameraFX cameraFX = null;
    
    private float step_timer = 0.0f;
    private float cameraFXTimer = 0.0f;
    private float cameraFXInterval = 0.0f;
    private const float cameraFXDuration = 3.0f;

    [SerializeField] private int goalConsumeCount = 0; 
    [SerializeField] private Text hpText = null;
    [SerializeField] private Text mileageText = null;
    [SerializeField] private Text consumeText = null;

    void Awake() {
        GameData.Instance.clearAll();
    }

    void Start() {
        this.player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        this.cameraFX = Camera.main.GetComponent<CameraFX>();
        cameraFXInterval = Random.Range(level_control.getCameraIntervalMin(), level_control.getCameraIntervalMax());
    }

    void Update() {
        this.step_timer += Time.deltaTime;
        cameraFXTimer += Time.deltaTime;

        if(cameraFXTimer >= cameraFXInterval) {
            cameraFX.reverse(cameraFXDuration);
            cameraFXTimer = 0.0f;
            cameraFXInterval = Random.Range(level_control.getCameraIntervalMin(), level_control.getCameraIntervalMax());
        }

        if (this.player.isPlayEnd()) {
            SceneManager.LoadScene("ResultScene");
        }

        hpText.text = string.Format("{0:0}%", player.getHp());

        int mileage = (int)(player.transform.position.x * 0.1);
        mileageText.text = string.Format("{0}m", mileage);
        GameData.Instance.mileage = mileage;

        consumeText.text = string.Format("X{0}/{1}", player.getConsumeCount(), goalConsumeCount);
        GameData.Instance.consume = player.getConsumeCount();
    }

    public void Pause() {
        Time.timeScale = 0.0f;
    }

    public void Play() {
        Time.timeScale = 1.0f;
    }

    public float getPlayTime() {
        return this.step_timer;
    }

    public int getGoalConsumeCount() {
        return goalConsumeCount;
    }
}
