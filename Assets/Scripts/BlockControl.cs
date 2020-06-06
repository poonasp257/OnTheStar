using UnityEngine;

public class BlockControl : MonoBehaviour {
	private MapCreator map_creator = null; 

	void Start() {
		map_creator = GameObject.Find("GameRoot").GetComponent<MapCreator>();
	}

	void Update() {
		if (this.map_creator.isDelete(this.gameObject)) {  
			GameObject.Destroy(this.gameObject); 
		}
	}
}
