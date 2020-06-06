using UnityEngine;

public class Block {
	public enum TYPE {
		NONE = -1,
		FLOOR = 0,
		HOLE,
		NUM,
	}
};

public class MapCreator : MonoBehaviour {
	public static float BLOCK_WIDTH = 1.0f; 
	public static float BLOCK_HEIGHT = 0.2f;
	public static int BLOCK_NUM_IN_SCREEN = 24; 

	private struct FloorBlock { 
		public bool is_created; 
		public Vector3 position;
	};

	private FloorBlock last_block;
	private PlayerControl player = null;
	private BlockCreator block_creator;
	private LevelControl level_control = null;
	private GameRoot game_root = null;

	public TextAsset level_data_text = null;

	void Start() {
		this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		this.last_block.is_created = false;
		this.block_creator = this.gameObject.GetComponent<BlockCreator>();
		this.level_control = new LevelControl();
		this.level_control.initialize();
		this.level_control.loadLevelData(this.level_data_text);
		this.game_root = this.gameObject.GetComponent<GameRoot>();
		this.game_root.level_control = this.level_control;
		this.player.level_control = this.level_control;
		this.player.game_root = this.game_root;
	}

	private void create_floor_block() {
		Vector3 block_position; 
		if(!this.last_block.is_created) { 
			block_position = this.player.transform.position;
			block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
			block_position.y = 0.0f;
		} 
		else {
			block_position = this.last_block.position;
		}
		block_position.x += BLOCK_WIDTH;
		this.level_control.update(this.game_root.getPlayTime());
		block_position.y = level_control.current_block.height * BLOCK_HEIGHT;
		LevelControl.CreationInfo current = this.level_control.current_block;
		if (current.block_type == Block.TYPE.FLOOR) {
			this.block_creator.createBlock(block_position);
		}

		this.last_block.position = block_position;
		this.last_block.is_created = true;
	}

	public bool isDelete(GameObject block_object) {
		bool ret = false; 
		float left_limit = this.player.transform.position.x - BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
		
		if (block_object.transform.position.x < left_limit) {
			ret = true; 
		}
		return (ret); 
	}

	void Update() { 
		float block_generate_x = this.player.transform.position.x;
		block_generate_x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f;
		while (this.last_block.position.x < block_generate_x) {
			this.create_floor_block();
		}
	}
}
