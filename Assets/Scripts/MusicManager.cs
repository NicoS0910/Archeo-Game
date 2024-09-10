using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audiosource;
    public AudioClip[] songs;
    public float volume;
    [SerializeField] private int _songsPlayed; // Geändert zu int
    [SerializeField] private bool[] _beenPlayed;

    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
        _beenPlayed = new bool[songs.Length];

        // Starte den ersten Song
        ChangeSong(Random.Range(0, songs.Length));
    }

    void Update()
    {
        _audiosource.volume = volume;

        // Springe zum nächsten Song bei Leertastendruck
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Stoppe den aktuellen Song und spiele den nächsten
            _audiosource.Stop();
            ChangeSong(Random.Range(0, songs.Length));
        }

        // Überprüfen, ob der aktuelle Song zu Ende ist
        if (!_audiosource.isPlaying)
        {
            ChangeSong(Random.Range(0, songs.Length));
        }
    }

    public void ChangeSong(int songPicked)
    {
        // Wenn alle Songs gespielt wurden, setze die Liste zurück
        if (_songsPlayed >= songs.Length)
        {
            ResetPlaylist();
        }

        // Suche einen ungespielten Song
        int attempts = 0;
        while (_beenPlayed[songPicked] && attempts < songs.Length)
        {
            songPicked = Random.Range(0, songs.Length);
            attempts++;
        }

        // Spiele den Song, falls er noch nicht gespielt wurde
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
        // Setzt die gespielten Songs zurück, um die Playlist zu wiederholen
        _songsPlayed = 0;
        for (int i = 0; i < _beenPlayed.Length; i++)
        {
            _beenPlayed[i] = false;
        }
    }
}
