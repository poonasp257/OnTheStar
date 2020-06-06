using UnityEngine;

public enum StarType {
    None = -1,
    Fire = 0,
    Water,
    Wood
}

public class PlayerControl : MonoBehaviour {
    public static float ACCELERATION = 10.0f;
    public static float SPEED_MIN = 4.0f;
    public static float SPEED_MAX = 8.0f;
    public static float JUMP_HEIGHT_MAX = 3.0f;
    public static float JUMP_KEY_RELEASE_REDUCE = 0.5f;
    public static float NARAKU_HEIGHT = -5.0f;

    public enum STEP {
        NONE = -1,
        RUN = 0,
        JUMP, 
        MISS
    };

    private CharacterController characterController;
    public float jumpSpeed = 10.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

    public LevelControl level_control = null;
    public GameRoot game_root = null;

    private GameObject currentStar = null;
    public StarType consumedStar = StarType.Fire;
    private int numOfConsume = 0;

    private const float maxHp = 100.0f;
    private float hp = 0.0f;

    [SerializeField] private GameObject[] starPrefabs = null;
    [SerializeField] private GameObject[] projectiles = null;

    void Start() {
        currentStar = starPrefabs[(int)consumedStar];
        currentStar.SetActive(true);
        characterController = GetComponent<CharacterController>();
        hp = maxHp;
    }

    public void consumeStar(StarType state) {
        consumedStar = state;
        currentStar.SetActive(false);
        currentStar = starPrefabs[(int)consumedStar];
        currentStar.SetActive(true);

        ++numOfConsume;
        if (hp <= maxHp) {
            hp += 10.0f;
            if (hp > maxHp) hp = maxHp;
        }
    }

    public int getConsumeCount() {
        return numOfConsume;
    }

    public float getHp() {
        return hp; 
    }

    public bool isPlayEnd() {
        if(this.numOfConsume >= game_root.getGoalConsumeCount()) { 
            GameData.Instance.isWin = true;
            return true;
        }

        return this.transform.position.y < NARAKU_HEIGHT || hp <= 0;
    }

    private void fireProjectile() {
        Vector3 mousePos = new Vector3 {
            x = Input.mousePosition.x,
            y = Input.mousePosition.y,
            z = transform.position.z
        };

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit, 100f)) {
            Vector3 dir = (hit.point - transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x);
            dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

            var projectile = Instantiate(projectiles[(int)consumedStar], transform.position, Quaternion.identity);
            projectile.transform.LookAt(hit.point);
            projectile.GetComponent<Rigidbody>().velocity = dir * 1000f * Time.deltaTime;
            projectile.GetComponent<ProjectileControl>().ImpactNormal = hit.normal;
            projectile.GetComponent<ProjectileControl>().Type = consumedStar;
        }
    }

    void Update() {
        if (isPlayEnd() || Time.timeScale <= 0.0f) return;

        if (Input.GetMouseButtonDown(0)) fireProjectile();

        if (characterController.isGrounded) {
            moveDirection.y = 0;

            if (Input.GetButton("Jump")) {
                moveDirection.y = jumpSpeed;
            }
        }
        else moveDirection.y -= gravity * Time.deltaTime;

        moveDirection.x = this.level_control.getPlayerSpeed();
        characterController.Move(moveDirection * Time.deltaTime);

        hp -= Time.deltaTime * 4.0f;
        if (hp < 0.0f) hp = 0.0f;
    }
}