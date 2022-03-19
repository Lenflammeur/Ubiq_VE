using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public void startBtn() {
        SceneManager.LoadScene("Hello World");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > 3.0f)
        {
            SceneManager.LoadScene("Hello World");
        }
    }
}
