using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {


//This script handles all audio for the game engine. 

//It is a singleton so only one can exist in the game at anytime.




	/// <summary>
	/// Audio Inputs
	/// </summary>



	public AudioClip[] playerJumpClips; //These arrays are for player audio clips
	public AudioClip[] playerLandClips;
	public AudioClip[] playerAttackClips;
	public AudioClip[] playerHurtClips;
	public AudioClip[] playerPickUpClips;
	public AudioClip[] playerVictoryClips; 
	public AudioClip[] playerClimbClips;
	public AudioClip[] playerDeathClips;
	public AudioClip[] playerWalkClips;


	public AudioClip[] enemyJumpClips; //These arrays are for enemy audio clips
	public AudioClip[] enemyLandClips;
	public AudioClip[] enemyAttackClips;
	public AudioClip[] enemyDeathClips;
	public AudioClip[] enemyWalkClips;


	public AudioClip[] itemKeyPickUpClips; //These arrays are for item audio clips
	public AudioClip[] itemKeyDropClips;
	public AudioClip[] itemSlothBuckPickUpClips;


	public AudioClip[] gearSpinClips; //These arrays are for environment audio clips
	public AudioClip[] doorOpenClips; 


	public AudioClip[] bgmClips; //These arrays are for BGM clips

	public AudioClip[] menuSelectClips;
	public AudioClip[] menuStartClips;
	public AudioClip[] menuLoopClips;


	/// <summary>
	/// AudioOutputs
	/// </summary>


	public AudioMixerGroup PlayerJumpOutput; 		//These are the audio outputs for player audio. 
	public AudioMixerGroup PlayerLandOutput;	 	//You could simplify this to only use one output for SFX
	public AudioMixerGroup PlayerAttackOutput;		//But this allows more control.
	public AudioMixerGroup PlayerHurtOutput;
	public AudioMixerGroup PlayerPickUpOutput;
	public AudioMixerGroup PlayerVictoryOutput;
	public AudioMixerGroup PlayerClimbOutput;
	public AudioMixerGroup PlayerDeathOutput;
	public AudioMixerGroup PlayerWalkOutput;


	public AudioMixerGroup EnemyJumpOutput; 		//These are the audio outputs for enemy audio.
	public AudioMixerGroup EnemyLandOutput;
	public AudioMixerGroup EnemyAttackOutput;
	public AudioMixerGroup EnemyDeathOutput;
	public AudioMixerGroup EnemyWalkOutput;


	public AudioMixerGroup ItemKeyPickUpOutput; 	//These are the audio outputs for item audio.
	public AudioMixerGroup ItemKeyDropOutput;
	public AudioMixerGroup ItemSlothBucksPickUpOutput;


	public AudioMixerGroup GearSpinOutput;			//These are the audio outputs for environment audio.
	public AudioMixerGroup DoorOpenOutput; 


	public AudioMixerGroup BgmOutput;				//These are the audio outputs for BGM audio

	public AudioMixerGroup MenuSelectOutput;	
	public AudioMixerGroup MenuStartOutput;	
	public AudioMixerGroup MenuLoopOutput;	

	/// <summary>
	/// Variables
	/// </summary>

	public float minPitchJump = .95f;
	public float maxPitchJump = 1.05f;

	public float minPitchLand = .95f;
	public float maxPitchLand = 1.05f;

	bool IsPlayerWalkingSFX = false;
	private AudioSource PlayerWalkSource;

	bool IsEnemyWalkingSFX = false;
	private AudioSource EnemyWalkSource;

    private AudioSource menuLoopSource;


    /// <summary>
    /// These functions call Player SFX
    /// </summary>

    public void PlayerJump (){
        if (playerJumpClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, playerJumpClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip jumpClipsse
            source.clip = playerJumpClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = PlayerJumpOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchJump, maxPitchJump);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, playerJumpClips[randomClip].length);
        }
    }

	public void PlayerLand (){
        if (playerLandClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, enemyLandClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = enemyLandClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = PlayerLandOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, enemyLandClips[randomClip].length);
        }
	}

	public void PlayerAttack (){
        if (playerAttackClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, playerAttackClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = playerAttackClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = PlayerAttackOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, playerAttackClips[randomClip].length);
        }
	}

	public void PlayerHurt (){
        if (playerHurtClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, playerHurtClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = playerHurtClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = PlayerHurtOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, playerHurtClips[randomClip].length);
        }
	}

	public void PlayerPickUp (){
        if (playerPickUpClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, playerPickUpClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = playerPickUpClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = PlayerPickUpOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, playerPickUpClips[randomClip].length);
        }
	}

	public void PlayerWalking(bool IsPlayerWalking){
		
	

		if (playerPickUpClips.Length > 0) {
			if (IsPlayerWalkingSFX == false && IsPlayerWalking == true) {
				int randomClip = Random.Range (0, playerWalkClips.Length - 1);

				PlayerWalkSource = gameObject.AddComponent <AudioSource> ();

				PlayerWalkSource.clip = playerWalkClips [randomClip];

				IsPlayerWalkingSFX = true;

				PlayerWalkSource.loop = true;
			
				PlayerWalkSource.Play();
			}
			else if (IsPlayerWalkingSFX == true && IsPlayerWalking == false) {
	
				Destroy (PlayerWalkSource);
				IsPlayerWalkingSFX = false;
				}
			}
		}






/// <summary>
/// These Functions call Enemy SFX
/// </summary>

	public void EnemyJump (){

        if (enemyJumpClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, enemyJumpClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = enemyJumpClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = EnemyJumpOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, enemyJumpClips[randomClip].length);
        }
	}
    
	public void EnemyLand (){

        if (enemyLandClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, enemyLandClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Source
            source.clip = enemyLandClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = EnemyLandOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, enemyLandClips[randomClip].length);
        }
	}

   	public void EnemyAttack (){
        if (enemyAttackClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, enemyAttackClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = enemyAttackClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = EnemyAttackOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, enemyAttackClips[randomClip].length);
        }
	}

	public void EnemyDeath (){
        if (enemyDeathClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, enemyDeathClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = enemyDeathClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = EnemyDeathOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, enemyDeathClips[randomClip].length);
        }
	}

	public void EnemyWalk(bool IsEnemyWalking){



		if (enemyWalkClips.Length > 0) {
		if (IsEnemyWalkingSFX == false && IsEnemyWalking == true) {
				int randomClip = Random.Range (0, playerWalkClips.Length - 1);

			EnemyWalkSource = gameObject.AddComponent <AudioSource> ();

			EnemyWalkSource.clip = playerWalkClips [randomClip];

			IsEnemyWalkingSFX = true;

			EnemyWalkSource.loop = true;

			EnemyWalkSource.Play();
		}
			else if (IsEnemyWalkingSFX == true && IsEnemyWalking == false) {

			Destroy (EnemyWalkSource);
			IsEnemyWalkingSFX = false;
			print ("Destroy");
			}
		}
	}




	//These functions are for item SFX
	public void KeyPickUp (){
        if (itemKeyPickUpClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, itemKeyPickUpClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = itemKeyPickUpClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = ItemKeyPickUpOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, itemKeyPickUpClips[randomClip].length);
        }
	}

	public void KeyDrop (){
        if (itemKeyDropClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, itemKeyDropClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = itemKeyDropClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = ItemKeyDropOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, itemKeyDropClips[randomClip].length);
        }
	}





	/// <summary>
	/// These functions are for environment sounds
	/// </summary>

	public void OpenDoor (){
        if (doorOpenClips.Length > 0)
        {
            // Randomise
            int randomClip = Random.Range(0, doorOpenClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = doorOpenClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = DoorOpenOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, doorOpenClips[randomClip].length);
        }
	}

	//public void GearStart(){

	//	int gearSpinClip = 0;
	//	int playerPosition = PlayerManager.Singleton.RB.transform.position;
	//	int enemyPosition = EnemyManager.transform.position;

	//	AudioSource source = gameObject.AddComponent<AudioSource> ();

	//	source.clip = gearSpinClips [gearSpinClip];

	//	source.loop = true;

	//	if (Rotator.rotationEnabled = true) {
	//		source.Play ();
	//	} 
	//	else
	//		Destroy (source);

	//	if 
	//		(playerPosition < enemyPosition + 10)
	//	{
	//		source.volume = 1;
	//	}

	//	else
	//	{
	//		source.volume = 0;
	//	}
	//	}

    //TODO: Fix Intro Functionality
	public IEnumerator BgmStart(){

        if (bgmClips.Length > 0)
        {
            int bgmClip = 0; //Choose First Clip

            AudioSource source = gameObject.AddComponent<AudioSource>(); //Add Audio Source 

            source.clip = bgmClips[bgmClip]; //Load Clip 0

            source.outputAudioMixerGroup = BgmOutput; //Choose Output

            source.Play(); // Play Clip

            yield return new WaitForSeconds(bgmClips[bgmClip].length);  // Wait until clip is finished playing...
            Destroy(source);                                           //...then destroy it!

            bgmClip = bgmClip + 1; // Choose 2nd Clip

            source = gameObject.AddComponent<AudioSource>(); // Add Audio Couse

            source.clip = bgmClips[bgmClip]; // Load Second Clip

            source.outputAudioMixerGroup = BgmOutput; //Set Output

            source.loop = true; //Set loop on

            source.Play(); // Play looped audio
        }
	}

	public void BgmStop(){

	}

	public void MenuSelect(){

        if (menuSelectClips.Length > 0)
        {
            int randomClip = Random.Range(0, menuSelectClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = menuSelectClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = MenuSelectOutput;

            //Set Pitch Randomisation
            source.pitch = Random.Range(minPitchLand, maxPitchLand);

            //Play Clip
            source.Play();

            //Destroy AudioSourc when finished
            Destroy(source, menuSelectClips[randomClip].length);
        }
}
    
	public IEnumerator MenuStartGame(){
        if (menuStartClips.Length > 0)
        {
            int randomClip = Random.Range(0, menuStartClips.Length - 1);
            //Create an Audio Source
            AudioSource source = gameObject.AddComponent<AudioSource>();
            //Load clip into Audio Sourse
            source.clip = menuStartClips[randomClip];

            //Set Output
            source.outputAudioMixerGroup = MenuStartOutput;

            //Play Clip
            source.Play();

            yield return new WaitForSeconds(menuStartClips[randomClip].length);

            //Destroy AudioSourc when finished
            Destroy(source);
            UIManager.Singleton.LoadNewGame();
        }
	}




    public void MenuLoop(bool playLoop)
    {

        if (menuLoopClips.Length > 0)
        {
            if (playLoop)
            {
                int randomClip = Random.Range(0, menuLoopClips.Length - 1);
                //Create an Audio Source
                menuLoopSource = gameObject.AddComponent<AudioSource>();
                //Load clip into Audio Sourse
                menuLoopSource.clip = menuLoopClips[randomClip];

                //Set Output
                menuLoopSource.outputAudioMixerGroup = MenuLoopOutput;
                menuLoopSource.loop = true;
                //Play Clip
                menuLoopSource.Play();
            }
            else
            {
                menuLoopSource.Stop();
                //Destroy AudioSourc when finished
                Destroy(menuLoopSource);
            }
        }
    }

	private void Awake()
	{
		IsPlayerWalkingSFX = false;

		PerformSingletonPattern();

	}

		private void PerformSingletonPattern()
		{
			if (singleton == null)
				singleton = this;
			else if (singleton != this)
				Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		}	

		

	static private AudioManager singleton;
	static public AudioManager Singleton
	{
		get
		{
			if (singleton == null)
				singleton = FindObjectOfType<AudioManager> ();	
		return singleton;
	
		}
	}

	}