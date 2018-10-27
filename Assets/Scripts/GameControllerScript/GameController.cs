using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	[HideInInspector] public bool gamePlaying;

	[SerializeField] private GameObject tile;
	[SerializeField] private Material tileMat;
	[SerializeField] private Light dayLight;
    [SerializeField] private Text texto;
    private Vector3 currentTilePosition;
	private AudioSource audio;

	private Camera mainCamera;
	private bool camColorLerp;
	private Color cameraColor;
	private Color[] tileColor_Day;
	private Color tileColor_Night;
	private int tileColor_Index;
	private Color tileTrueColor;
	private float timer;
	private float timerInterval = 10f;
	private float camLerpTimer;
	private float camLerpInterval = 1f;
	private int direction = 1;
    private int puntos = 0;
	public static GameController instance;

	void Awake () {
		MakeSingleton ();
		currentTilePosition = new Vector3 (-2, 0, 2);
		audio = GetComponent<AudioSource> ();

		mainCamera = Camera.main;
		cameraColor = mainCamera.backgroundColor;
		tileTrueColor = tileMat.color;
		tileColor_Index = 0;
		tileColor_Day = new Color[3];
		tileColor_Day [0] = new Color (10 / 256f, 139 / 256f, 203 / 256f);
		tileColor_Day [1] = new Color (10 / 256f, 200 / 256f, 20 / 256f);
		tileColor_Day [2] = new Color (220 / 256f, 170 / 256f, 45 / 256f);
		tileColor_Night = new Color (0, 8 / 256f, 11 / 256f);
		tileMat.color = tileColor_Day[0];
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 20; i++) {
			CreateTile ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		CheckLerpTimer ();
        actualizarPuntuacion();
	}

	void MakeSingleton (){
		if (instance == null) {
			instance = this;
		}
	}
    void actualizarPuntuacion(){
        texto.text = "PUNTUACION ---> " + puntos;
    }
	void OnDisable () {
		instance = null;
		tileMat.color = tileTrueColor;
	}

	void CreateTile (){
		Vector3 newTilePosition = currentTilePosition;
		int rand = Random.Range (0, 100);
		if (rand < 50) {
			newTilePosition.x -= 1f;
		} else {
			newTilePosition.z += 1f;
		}
		currentTilePosition = newTilePosition;
		Instantiate (tile, currentTilePosition, Quaternion.identity);
	}

	IEnumerator SpawnNewTiles (){
		yield return new WaitForSeconds (0.3f);
		CreateTile ();

		if (gamePlaying == true) {
			StartCoroutine (SpawnNewTiles ());
		}
	}

	public void ActiveTileSpawner () {
		StartCoroutine (SpawnNewTiles ());
	}

	public void PlayCollectableSound(){
		audio.Play();
        puntos++;
	}

	void CheckLerpTimer(){
		timer += Time.deltaTime;

		if (timer > timerInterval) {
			timer -= timerInterval;
			camColorLerp = true;
			camLerpTimer = 0;
		}

		if (camColorLerp == true) {// se hace de noche 
			camLerpTimer += Time.deltaTime;
			float percent = camLerpTimer / camLerpInterval; // controla el pocentaje de dia - noche que hay en este momento

			if (direction == 1) {// si llego a 1 que es  igual a la noche 
				mainCamera.backgroundColor = Color.Lerp (cameraColor, Color.black, percent); // Cambia el color de el fondo de la camara 
				tileMat.color = Color.Lerp (tileColor_Day [tileColor_Index], tileColor_Night, percent);
				dayLight.intensity = 1f - percent;
			} else { // es de dia y cambia el color de fondo al igual que en el codigo anterior
				mainCamera.backgroundColor = Color.Lerp (Color.black, cameraColor, percent);
				tileMat.color = Color.Lerp (tileColor_Night, tileColor_Day [tileColor_Index], percent);
				dayLight.intensity = percent;
			}

			if (percent > 0.98f) { // contador de tiempo 
				camLerpTimer = 1f;
				direction *= -1;
				camColorLerp = false;
				if (direction == -1) {
					tileColor_Index = Random.Range (0, tileColor_Day.Length);
				}
			}

		}
	}
}
