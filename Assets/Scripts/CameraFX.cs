using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraFX : MonoBehaviour {
    private Quaternion previousRotation;

    public void revertFX() {
        this.transform.rotation = previousRotation;
    }

    public void reverse(float duration) {
        previousRotation = transform.rotation;

        int rotation = Random.Range(0, 3);        
        switch(rotation) {
            case 0:
                this.transform.Rotate(0, 0, 90);
                break;
            case 1:
                this.transform.Rotate(0, 0, -90);
                break;
            case 2:
                this.transform.Rotate(0, 0, 180);
                break;
            case 3:
                this.transform.Rotate(0, 0, -180);
                break;
        }

        StartCoroutine(StartFX(duration));
    }

    private IEnumerator StartFX(float duration) {
        yield return new WaitForSeconds(duration);

        revertFX();
    }
}