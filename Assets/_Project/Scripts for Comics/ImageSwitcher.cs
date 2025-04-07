using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class ImageSwitcher : MonoBehaviour
{
    public Image uiImage; 
    public Sprite[] images; 
    private int currentIndex = 0;

    public string nextSceneName; 

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchImage();
        }
    }

    void SwitchImage()
    {
        if (currentIndex < images.Length - 1)
        {
            currentIndex++; 
            uiImage.sprite = images[currentIndex]; 
        }
        else
        {
            LoadNextScene(); 
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}


