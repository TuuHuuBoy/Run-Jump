using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject obstacleLv2;
    public GameObject obstacleLv3;
    public Transform spawnPoint;

    int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public GameObject playButton;
    public GameObject player;
    public GameObject HighScoreText;

    bool spawningObstacleLv1 = true;
    bool spawningObstacleLv2 = true;
    bool spawningObstacleLv3 = true;

    public AudioSource music;
    public AudioClip musicClip;
    float speedMusic = 1f;
    bool isMusicSpeedUp = false;
    bool isMusicSpeedUpLv2 = false;

    void Start()
    {

        music.clip = musicClip;
    }

    
    void Update()
    {
        
        if (!music.isPlaying)
        {
            
            music.pitch = speedMusic;
            music.Play();
        }

        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = savedHighScore.ToString();

    }

    IEnumerator SpawnObstacle()
    {
        while (spawningObstacleLv1)
        {
            float waitTime = Random.Range(0.5f, 2);
            yield return new WaitForSeconds(waitTime);

            Instantiate(obstacle, spawnPoint.position, Quaternion.identity);

            if (score >= 15)
            {
                spawningObstacleLv1 = false;
                yield return new WaitForSeconds(5f);
                Instantiate(obstacleLv2, spawnPoint.position, Quaternion.identity);
                StartCoroutine("SpawnObstacleLv2");

                isMusicSpeedUp = true;
            }
            
        }
    }

    IEnumerator SpawnObstacleLv2()
    {
        while (spawningObstacleLv2) 
        {
            spawningObstacleLv1 = false;
            float waitTime = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(waitTime);

            Instantiate(obstacleLv2, spawnPoint.position, Quaternion.identity);

            if (isMusicSpeedUp)
            {
                speedMusic = 1.1f;
                float musicEndTime = (musicClip.length / speedMusic) + Time.time;
                music.SetScheduledEndTime(musicEndTime);
                isMusicSpeedUp = false;
            }

            if (score >= 30)
            {
                spawningObstacleLv2 = false;
                yield return new WaitForSeconds(5f);
                Instantiate(obstacleLv3, spawnPoint.position, Quaternion.identity);
                StartCoroutine("SpawnObstacleLv3");

                isMusicSpeedUpLv2 = true;
            }
            

        }

    }
    IEnumerator SpawnObstacleLv3()
    {
        while (spawningObstacleLv3) 
        {
            float waitTime = Random.Range(0.4f, 1f);
            yield return new WaitForSeconds(waitTime);

            Instantiate(obstacleLv3, spawnPoint.position, Quaternion.identity);

            if (isMusicSpeedUpLv2)
            {
                speedMusic = 1.3f;
                float musicEndTime = (musicClip.length / speedMusic) + Time.time;
                music.SetScheduledEndTime(musicEndTime);
                isMusicSpeedUpLv2 = false;
            }
        }
    }
    void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();

        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = score.ToString();
            PlayerPrefs.Save();
        }
    }

    public void GameStart()
    {
        //PlayerPrefs.DeleteKey("HighScore");

        player.SetActive(true);
        playButton.SetActive(false);
        scoreText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(false);
        HighScoreText.SetActive(false);

        StartCoroutine("SpawnObstacle");
        InvokeRepeating("ScoreUp", 2f, 1f);

        // start playing music
        music.pitch = speedMusic;
        music.PlayScheduled(AudioSettings.dspTime);
    }
}