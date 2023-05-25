using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject playText;
    public GameObject playButton;

    public GameObject turretHead;
    private float angle;
    private bool isAdjusting = false;
    public float turnSpeed = 5f;
    public float deviance = 45f;

    private bool isLoading = false;

    void Start()
    {
        angle = -90f;
    }
    void Update()
    {
        if (angle >= -90f + deviance)
            isAdjusting = false;
        
        if (angle <= -90f - deviance)
            isAdjusting = true;

        if (isAdjusting)
        {
            angle += Time.deltaTime * turnSpeed;
        }
        else
        {
            angle -= Time.deltaTime * turnSpeed;
        }

        turretHead.transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    
    public void Play()
    {
        if (isLoading)
            return;

        isLoading = true;
        playButton.transform.Translate(0, -0.2f, 0);
        playText.transform.Translate(0, -0.2f, 0);
        Invoke(nameof(LoadLevel), 0.3f);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(SelectLevel.levelName);
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void DropProgress()
    {
        PlayerPrefs.SetInt("levelReached", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
