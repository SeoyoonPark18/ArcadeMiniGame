using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeStartUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoMainmenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitMenu()
    {
        Application.Quit();
    }
    public void ReroadGame()
    {
        SceneManager.LoadScene("ShootingGame");
    }
}
