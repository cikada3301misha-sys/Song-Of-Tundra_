using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame() //Загрузка игры
    {
        SceneManager.LoadScene(1);
    }
    public void Quit() //Выход из игры (не работает в редакторе)
    {
        Application.Quit();
    }
    public void Settings() //Загрузка настроек
    {
        SceneManager.LoadScene(2);
    }
    public void ToMenu()// Загрузка меню
    {
        SceneManager.LoadScene(0);
    }
}
