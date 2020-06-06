using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class ProjectileControl : MonoBehaviour {
    private bool isExploded = false;
    
    public Vector3 ImpactNormal { get; set; }
    public StarType Type { get; set; }

    [SerializeField] private GameObject impactParticle = null;
    [SerializeField] private GameObject projectileParticle = null;
    [SerializeField] private GameObject muzzleParticle = null;

    public float getDamage(StarType enemyType) {
        if(Type == StarType.Fire) {
            switch(enemyType) {
                case StarType.Fire: return 2;
                case StarType.Water: return 1;
                case StarType.Wood: return 3;
            }
        }
        else if(Type == StarType.Water) {
             switch(enemyType) {
                case StarType.Fire: return 3;
                case StarType.Water: return 2;
                case StarType.Wood: return 1;
            }
        }
        else if(Type == StarType.Wood) {
             switch(enemyType) {
                case StarType.Fire: return 1;
                case StarType.Water: return 3;
                case StarType.Wood: return 2;
            }
        }

        return 1;
    }

    private void Start() {
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation);
        projectileParticle.transform.parent = this.transform;
        if (muzzleParticle) {
            muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
            Destroy(muzzleParticle, 1.5f);
        }

        StartCoroutine(DestroyProjectile());
    }

    private GameObject Explode() {
        var particle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, ImpactNormal));
        return particle;
    }

    private IEnumerator DestroyProjectile() {
        while(true) {
            yield return new WaitForSeconds(4.0f);

            impactParticle = Explode();
            Destroy(impactParticle, 2.0f);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (isExploded ||  
            collider.tag == "Player" ||
            collider.tag == "Projectile") return;

        isExploded = true;
        impactParticle = Explode();
        Destroy(impactParticle, 2.0f);

        if (collider.tag == "Destructible") {
            Destroy(collider.gameObject);
        }

        Destroy(gameObject);
    }
}
