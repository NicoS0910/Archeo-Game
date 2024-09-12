using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource _audiosource;
    public AudioClip[] songs;
    public AudioClip scooterMinigameSong;  // Song für das Scooter-Minispiel
    public float volume;
    [SerializeField] private int _songsPlayed; // Geändert zu int
    [SerializeField] private bool[] _beenPlayed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // MusicManager bleibt über Szenen hinweg erhalten
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
        _beenPlayed = new bool[songs.Length];

        // Starte den ersten Song aus der regulären Playlist
        ChangeSong(Random.Range(0, songs.Length));
    }

    void Update()
    {
        _audiosource.volume = volume;

        // Überprüfen, ob der aktuelle Song zu Ende ist
        if (!_audiosource.isPlaying)
        {
            ChangeSong(Random.Range(0, songs.Length));
        }
    }

    public void ChangeSong(int songPicked)
    {
        if (_songsPlayed >= songs.Length)
        {
            ResetPlaylist();
        }

        int attempts = 0;
        while (_beenPlayed[songPicked] && attempts < songs.Length)
        {
            songPicked = Random.Range(0, songs.Length);
            attempts++;
        }

        if (!_beenPlayed[songPicked])
        {
            _songsPlayed++;
            _beenPlayed[songPicked] = true;
            _audiosource.clip = songs[songPicked];
            _audiosource.Play();
        }
    }

    private void ResetPlaylist()
    {
        _songsPlayed = 0;
        for (int i = 0; i < _beenPlayed.Length; i++)
        {
            _beenPlayed[i] = false;
        }
    }

    // Methode zum Abspielen des Scooter-Minispiel-Songs im Loop
    public void PlayScooterMinigameSong()
    {
        _audiosource.clip = scooterMinigameSong;
        _audiosource.loop = true;
        _audiosource.Play();
    }

    // Methode zum Zurücksetzen auf die reguläre Playlist
    public void PlayRegularPlaylist()
    {
        _audiosource.loop = false;  // Beende das Looping des Minigame-Songs
        ChangeSong(Random.Range(0, songs.Length));
    }
}
