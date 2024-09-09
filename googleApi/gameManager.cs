using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Windows.Speech;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public TextToSpeech textToSpeech;
    public SpeechToText speechToText;
    public AudioSource audioSource;

    private bool isListening = true;
    private DictationRecognizer dictationRecognizer;

    private void Start()
    {
        // Initialize the DictationRecognizer for speech input
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.DictationResult += OnDictationResult;
        dictationRecognizer.Start();
    }

    private void Update()
    {
        if (isListening)
        {
            // Capture audio input using the DictationRecognizer
            // Audio data will be processed in the OnDictationResult method
        }
    }

    private async void OnDictationResult(string text, ConfidenceLevel confidence)
    {
        isListening = false;
        Debug.Log("Player said: " + text);

        // Generate AI response based on player speech
        string aiResponseText = await GenerateAIResponseAsync(text);

        // Convert AI response to speech
        AudioClip audioClip = await textToSpeech.SynthesizeSpeechAsync(aiResponseText);
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        isListening = true;
    }

    private async Task<string> GenerateAIResponseAsync(string playerSpeech)
    {
        // Example AI interaction: Returning a static response for now
        // You can integrate with an AI service like OpenAI or other APIs
        return "Hello, how can I assist you today?";
    }

    private void OnDisable()
    {
        // Stop the DictationRecognizer when the object is disabled or destroyed
        if (dictationRecognizer != null && dictationRecognizer.Status == SpeechSystemStatus.Running)
        {
            dictationRecognizer.Stop();
            dictationRecognizer.Dispose();
        }
    }
}
