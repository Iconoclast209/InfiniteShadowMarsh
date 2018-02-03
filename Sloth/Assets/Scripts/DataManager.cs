using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Class designed to hold information for data persistence (between scenes and loading/saving)
/// <para>Class instead of struct to facilitate possible need for multiple constructors</para>
/// <para>Outside of GameController to facilitate instancing outside of GameController.</para>
/// </summary>
[System.Serializable]
public class GameData
{
    /// <summary>
    /// Level which is (or will be) loaded.
    /// </summary>
    public int currentLevel;
    /// <summary>
    /// Player's currently saved health.
    /// </summary>
    public int playerCurrentHealth;
    /// <summary>
    /// Default GameData constructor - Feeds in hardcoded values
    /// </summary>
	public GameData()
	{
		currentLevel = 1;
		playerCurrentHealth = 100;
	}

    /// <summary>
    /// GameData constructor - Requires feeding in values
    /// </summary>
    /// <param name="level">Level value to give GameData</param>
    /// <param name="health">Player's current health value</param>
    /// <param name="score">Value to set player's total score</param>
    /// <param name="upgradesPurchased">Upgrades purchased by player</param>
	public GameData(int level, int health)
	{
		currentLevel = level;
		playerCurrentHealth = health;
	}

    /// <summary>
    /// GameData constructor - Requires a GameData value
    /// </summary>
    /// <param name="data"></param>
	public GameData(GameData data)
	{
		currentLevel = data.currentLevel;
		playerCurrentHealth = data.playerCurrentHealth;
	}
}

/// <summary>
/// Manager class for game data.  Manages data persistence, saving/loading and changing scenes.
/// </summary>
public class DataManager : MonoBehaviour {

    /// <summary>
    /// BinaryFormatter responsible for (de)serializing for saving/loading data to file.
    /// </summary>
	private BinaryFormatter formatter;
    /// <summary>
    /// Filestream responsible for sending/retrieving data to/from a file.
    /// </summary>
	private FileStream file;
    /// <summary>
    /// String that defines the name of the saved data file.
    /// </summary>
	private string saveFileName = "/saveGameData.dat";
    /// <summary>
    /// GameData that stores current game information - used for data persistence, loading and saving game.
    /// </summary>
	private GameData gameData;

	/// <summary>
    /// Static reference to GameController singleton object
    /// </summary>
	static private DataManager singleton;
    /// <summary>
    /// Static method to access singleton object.
    /// </summary>
	static public DataManager Singleton
	{
		get{
			if (singleton == null)
				singleton = FindObjectOfType<DataManager> ();

			return singleton;
		}
	}

    /// <summary>
    /// Early set-up.  Establishes singleton pattern. Don't Destroy On Load.
    /// </summary>
	private void Awake() 
	{
		if (singleton == null) {
			singleton = this;
		} else if (singleton != this) {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);
	}

    /// <summary>
    /// Set-up.  Loads data if a save file exists.  Otherwise, creates new game.
    /// </summary>
	private void Start() 
	{
		if (DoesSaveFileExist ()) {
			LoadGameData ();
		} else {
			gameData = new GameData ();
		}
	}

    /// <summary>
    /// Initializes (gameData) for a new game
    /// </summary>
	public void CreateNewGame()
	{
		gameData = new GameData ();
	}

    /// <summary>
    /// Saves game in serialized file - Creates new file if necessary.
    /// </summary>
	public void SaveGameData()
	{
		formatter = new BinaryFormatter ();
		file = File.Create (Application.persistentDataPath + saveFileName);
		formatter.Serialize (file, gameData);
		file.Close ();
	}

    /// <summary>
    /// Loads currently saved data from file. - No error checking.
    /// </summary>
    public void LoadGameData()
	{
		formatter = new BinaryFormatter ();
		file = File.OpenRead (Application.persistentDataPath + saveFileName);
		gameData = new GameData ((GameData)formatter.Deserialize (file));
		file.Close ();
	}
    
    /// <summary>
    /// Checks if save-file exists.
    /// </summary>
    /// <returns>Returns true if file exists.  Otherwise, returns false.</returns>
    public bool DoesSaveFileExist()
	{
		return File.Exists (Application.persistentDataPath + saveFileName);
	}

	/// <summary>
    /// Gets currently stored level.
    /// </summary>
    /// <returns>Returns stored level value</returns>
	public int GetCurrentLevel()
	{
		return gameData.currentLevel;
	}

    /// <summary>
    /// Gets currently stored player health.
    /// </summary>
    /// <returns>Returns current health value.</returns>
    public int GetCurrentHealth()
	{
		return gameData.playerCurrentHealth;
	}

	/// <summary>
    /// Sets currently stored current level to increase by 1.
    /// </summary>
	public void IncrementCurrentLevel()
	{
		gameData.currentLevel++;
	}

    /// <summary>
    /// Sets currently stored current health to specified value.
    /// </summary>
    /// <param name="health">New current Health value.</param>
    public void SetCurrentHealth(int health)
	{
		gameData.playerCurrentHealth = health;
	}


		
}
