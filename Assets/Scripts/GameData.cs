public class GameData {
	private static GameData instance = null;

	public static GameData Instance {
		get {
			if(instance == null) {
				instance = new GameData();
				return instance;
			}

			return instance;
		}
	}

	public bool isWin = false;
	public int mileage = 0;
	public int consume = 0;
	
	public void clearAll() {
		mileage = 0;
		consume = 0;
	}
}
