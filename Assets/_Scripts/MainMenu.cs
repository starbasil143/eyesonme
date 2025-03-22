using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button chapter1Button;
    public Button chapter2Button;
    public Button chapter3Button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.GetInt("part1_finished") != 1)
        {
            chapter2Button.interactable = false;
        }
        if (PlayerPrefs.GetInt("part2_finished") != 1)
        {
            chapter3Button.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Chapter1()
    {
        SceneManager.LoadScene("Curtain_1");
    }

    public void Chapter2()
    {
        SceneManager.LoadScene("Curtain_2");
    }

    public void Chapter3()
    {
        SceneManager.LoadScene("Curtain_3");
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Curtain_1");
    }

}
