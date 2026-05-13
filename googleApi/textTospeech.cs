using Google.Cloud.TextToSpeech.V1;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.Networking;

// AIzaSyBGMbo3NXsuy4rP5v4I2tVFhM89ctXizJw

public class TextToSpeech : MonoBehaviour
{
    private TextToSpeechClient textToSpeechClient;

    private void Start()
    {
        textToSpeechClient = TextToSpeechClient.Create();
    }

    public async Task<AudioClip> SynthesizeSpeechAsync(string text)
    {
        var response = await textToSpeechClient.SynthesizeSpeechAsync(new SynthesizeSpeechRequest
        {
            Input = new SynthesisInput { Text = text },
            Voice = new VoiceSelectionParams
            {
                LanguageCode = "en-US",
                SsmlGender = SsmlVoiceGender.Female
            },
            AudioConfig = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Linear16
            }
        });

        return await ConvertToAudioClip(response.AudioContent.ToByteArray());
    }

    private async Task<AudioClip> ConvertToAudioClip(byte[] audioData)
    {
        string path = Path.Combine(Application.persistentDataPath, "temp.wav");
        File.WriteAllBytes(path, audioData);

        using (var www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.WAV))
        {
            await www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                return DownloadHandlerAudioClip.GetContent(www);
            }
            else
            {
                Debug.LogError("Error loading audio clip: " + www.error);
                return null;
            }
        }
    }
}