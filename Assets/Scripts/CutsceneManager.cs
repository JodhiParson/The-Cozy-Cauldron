using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [System.Serializable]
    public class CutsceneSlide
    {
        public Sprite image;
        [TextArea(3, 10)]
        public string dialogue;
    }

    public CutsceneSlide[] slides;
    public Image cutsceneImage;
    public TMP_Text dialogueText;

    public float typingSpeed = 0.04f;
    private int currentSlide = 0;
    private bool isTyping = false;

    void Start()
    {
        ShowSlide(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            NextSlide();
        }
    }

    void ShowSlide(int index)
    {
        if (index >= slides.Length)
        {
            EndCutscene();
            return;
        }

        cutsceneImage.sprite = slides[index].image;
        dialogueText.text = "";
        StartCoroutine(TypeText(slides[index].dialogue));
    }

    System.Collections.IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void NextSlide()
    {
        currentSlide++;
        ShowSlide(currentSlide);
    }

    void EndCutscene()
    {
        // Change this to your main gameplay scene name
        SceneManager.LoadScene("MainScene");
    }
}
