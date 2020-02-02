using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LOad : MonoBehaviour {

    private bool loadScene = false;

    public GameObject parent;
    public static bool loadin = false;
    public Text loadingText;
    public RawImage Bg;
    public Text Hint;
    private bool Changin = false;
    private string[] Tips = new string[] {"Between both timezones are different things. Try shifting through time to see the difference.", "Not every answer is in just one phase in time.", "As redundent as it is, 'timing' is everything", "Some blockage requires an item to open it. Be sure to look for one.", "The area can only be fixed when you jump into the middle of the time chamber.", "press the jump button to go to another area to fix after you've fixed the current area."};



    // Updates once per frame
    void Update() {

        
        if (loadin == true && loadScene == false) {
            print(Tips[Random.Range(1, Tips.Length)]);
            parent.SetActive(true);

            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;

            // ...change the instruction text to read "Loading..."
            loadingText.text = "Loading...";
            Hint.text = "Press Space to get a hint!";
            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());

        }

        // If the new scene has started loading...
        if (loadScene == true) {

            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

        }
        if (Input.GetButton("Jump") && Changin == false)
        {
            Changin = true;
            StartCoroutine(ChangeHint());
        }

    }


    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene() {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(5);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(PlayerController.Level);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone) {
            LOad.loadin = false;
            loadScene = false;
            yield return null;
        }

    }

    IEnumerator ChangeHint()
    {
        for (float i = 1; i > 0; i = i - .05f)
        {
            yield return new WaitForSecondsRealtime(.005f);
            Hint.color = new Color(Hint.color.r, Hint.color.g, Hint.color.b,i);
        }
        Hint.text = Tips[Random.Range(1, Tips.Length)];
        yield return new WaitForSecondsRealtime(.05f);
        for (float i = 0; i < 1; i = i + .05f)
        {
            yield return new WaitForSecondsRealtime(.005f);
            Hint.color = new Color(Hint.color.r, Hint.color.g, Hint.color.b, i);
        }
        yield return new WaitForSecondsRealtime(.5f);
        Changin = false;
    }



}