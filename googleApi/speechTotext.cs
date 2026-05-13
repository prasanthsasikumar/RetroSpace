using Google.Cloud.Speech.V1;
using UnityEngine;
using System.Threading.Tasks;

public class SpeechToText : MonoBehaviour
{
    private SpeechClient speechClient;

    private void Start()
    {
        speechClient = SpeechClient.Create();
    }

    public async Task<string> RecognizeSpeechAsync(byte[] audioData)
    {
        var response = await speechClient.RecognizeAsync(new RecognitionConfig
        {
            Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
            SampleRateHertz = 16000,
            LanguageCode = "en-US",
        }, RecognitionAudio.FromBytes(audioData));

        return response.Results[0].Alternatives[0].Transcript;
    }
}