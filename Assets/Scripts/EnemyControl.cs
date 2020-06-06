using System.Collections;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
	private float hp = 3;

	private GameObject consumableParticle = null;
	private ParticleSystem changeParticle = null;

	private bool isConsumable { get { return hp <= 0; } }

	[SerializeField] private StarType state;

	private void Start() {
		consumableParticle = transform.Find("Consumable Particle").gameObject;
		var changeParticleObj = transform.Find("Consumable Particle/Change Particle");
		changeParticle = changeParticleObj.GetComponent<ParticleSystem>();

		StartCoroutine("CheckRange");
	}

	private void Update() {
		if(isConsumable) {
			consumableParticle.SetActive(true);
		}
	}

	private void OnTriggerEnter(Collider collider) {
		if (collider.tag == "Player" && isConsumable) {
			PlayerControl controller = collider.GetComponent<PlayerControl>();
			changeParticle.Play();
			controller.consumeStar(this.state);
			Destroy(this.gameObject);
			++GameData.Instance.consume;
		}
		if(collider.tag == "Projectile") {
			ProjectileControl controller = collider.GetComponent<ProjectileControl>();
			if(hp > 0) hp -= controller.getDamage(state);
		}
	}

	private bool isOutOfRange() {
		Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
		return screenPos.x <= 0;
	}

	private IEnumerator CheckRange() {
		while(true) {
			yield return new WaitForSeconds(3.0f);

			if (isOutOfRange()) {
				Destroy(this.gameObject);
				yield break;
			}
		}
	}
}
