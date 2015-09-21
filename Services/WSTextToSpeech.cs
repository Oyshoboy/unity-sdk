using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class WatsonTextToSpeech{

	#region Enumarations Watson Text to Speech

	public enum AudioFormatType{
		oggWithCodecOpus = 0,
		wav,
		flac,
		NaN
	};

	public enum VoiceType{
		en_US_Michael = 0,
		en_US_Lisa,
		en_US_Allison,
		en_GB_Kate,
		es_ES_Enrique,
		es_ES_Laura,
		es_US_Sofia,
		de_DE_Dieter,
		de_DE_Birgit,
		fr_FR_Renee,
		it_IT_Francesca,
		NaN
	};

	#endregion

	#region Contructor - Watson Text To Speech and Override ToString

	/// <summary>
	/// Initializes a new instance of the <see cref="WatsonTextToSpeech"/> class.
	/// </summary>
	/// <param name="textToSpeech">Text to speech. String paremeter</param>
	/// <param name="voice">Voice.</param>
	/// <param name="audioFormat">Audio format.</param>
	public WatsonTextToSpeech(string textToSpeech, WatsonTextToSpeech.VoiceType voice = WatsonTextToSpeech.VoiceType.en_US_Michael, WatsonTextToSpeech.AudioFormatType audioFormat = WatsonTextToSpeech.AudioFormatType.wav){
		this.textToSpeech = textToSpeech;
		this.audioFormat = audioFormat;
		this.voice = voice;
	}
	
	public override string ToString (){
		return string.Format ("Watson TextToSpeech: Text={0}, Voice={1}, Format={2}", textToSpeech, voiceString, audioFormatString);
	}

	#endregion

	#region Watson Text To Speech Variables

	/// <summary>
	/// The selected audio format. The default one is WAV
	/// </summary>
	public AudioFormatType audioFormat {get;internal set;}
	/// <summary>
	/// The voice. The default one is English US Michael
	/// </summary>
	public VoiceType voice {get;internal set;}
	/// <summary>
	/// The text to speech. String parameter
	/// </summary>
	public string textToSpeech {get;internal set;}

	/// <summary>
	/// Gets the audio format string.
	/// </summary>
	/// <value>The audio format string.</value>
	public string audioFormatString{
		get{
			string audioFormatSelected = "";
			switch (audioFormat) {
			case AudioFormatType.oggWithCodecOpus:		audioFormatSelected = "audio/ogg;codecs=opus";			break;
			case AudioFormatType.wav:					audioFormatSelected = "audio/wav";						break;
			case AudioFormatType.flac:					audioFormatSelected = "audio/flac";						break;
			default:
				audioFormatSelected = "audio/wav";
				Debug.LogError("Unknown AudioFormat appeared as "+audioFormat+" - Audio Format needs to be defined!");
				break;
			}
			return audioFormatSelected;
		}
	}

	/// <summary>
	/// Gets the audio format extension.
	/// Default is: wav
	/// </summary>
	/// <value>The audio format extension.</value>
	public string audioFormatExtension{
		get{
			string audioFormatSelectedExtension = "";
			switch (audioFormat) {
			case AudioFormatType.oggWithCodecOpus:		audioFormatSelectedExtension = "ogg";			break;
			case AudioFormatType.wav:					audioFormatSelectedExtension = "wav";			break;
			case AudioFormatType.flac:					audioFormatSelectedExtension = "flac";			break;
			default:
				audioFormatSelectedExtension = "wav";
				Debug.LogError("Unknown AudioFormat appeared as "+audioFormat+" - Audio Format needs to be defined!");
				break;
			}
			return audioFormatSelectedExtension;
		}
	}

	/// <summary>
	/// Gets the voice string. 
	/// Default is US English Michael Voice. 
	/// </summary>
	/// <value>The voice string.</value>
	public string voiceString{
		get{
			string voiceSelected = "";
			switch (voice) {
			case VoiceType.en_US_Michael: 		voiceSelected = "en-US_MichaelVoice"; 		break;
			case VoiceType.en_US_Lisa: 			voiceSelected = "en-US_LisaVoice"; 			break;
			case VoiceType.en_US_Allison:		voiceSelected = "en-US_AllisonVoice";		break;
			case VoiceType.en_GB_Kate:			voiceSelected = "en-GB_KateVoice";			break;
			case VoiceType.es_ES_Enrique:		voiceSelected = "es-ES_EnriqueVoice";		break;
			case VoiceType.es_ES_Laura:			voiceSelected = "es-ES_LauraVoice";			break;
			case VoiceType.es_US_Sofia:			voiceSelected = "es-US_SofiaVoice";			break;
			case VoiceType.de_DE_Dieter:		voiceSelected = "de-DE_DieterVoice";		break;
			case VoiceType.de_DE_Birgit:		voiceSelected = "de-DE_BirgitVoice";		break;
			case VoiceType.fr_FR_Renee:			voiceSelected = "fr-FR_ReneeVoice";			break;
			case VoiceType.it_IT_Francesca:		voiceSelected = "it-IT_FrancescaVoice";		break;
				
			default:
				voiceSelected = "en-US_MichaelVoice";
				Debug.LogError("Unknown Voice appeared as "+voice+" - Voice needs to be defined!");
				break;
			}
			return voiceSelected;
		}
	}
	
	#endregion

	#region Parameters from Bluemix

	public string textToSpeechForGETParameter{
		get{
			return textToSpeech.Replace(" ","%20");
		}
	}

	public string parameterAcceptedAudio{
		get{
			return string.Concat("accept", "=", audioFormatString);
		}
	}
	
	public string parameterVoice{
		get{
			return string.Concat("voice", "=", voiceString);
		}
	}
	
	public string parameterText{
		get{
			return string.Concat("text", "=", textToSpeechForGETParameter);
		}
	}
	
	#endregion


}

[RequireComponent(typeof(AudioSource))]
public class WSTextToSpeech : WatsonService {

	#region Service URL , UserName, Password  - Each Service should be implement these variables!

	public override string serviceURL{
		get{
			if(String.IsNullOrEmpty(_serviceURL)){
				_serviceURL = "TEST";
			}
			return _serviceURL;
		}
		set{
			_serviceURL = value;
		}
	}

	public override string serviceCredentialUserName{
		get{
			if(String.IsNullOrEmpty(_serviceCredentialUserName)){
				_serviceCredentialUserName = "TEST";
			}
			return _serviceCredentialUserName;
		}
		set{
			_serviceCredentialUserName = value;
		}
	}

	public override string serviceCredentialPassword{
		get{
			if(String.IsNullOrEmpty(_serviceCredentialPassword)){
				_serviceCredentialPassword = "Test";
			}
			return _serviceCredentialPassword;
		}
		set{
			_serviceCredentialPassword = value;
		}
	}

	public override string serviceVersion{
		get{
			if(String.IsNullOrEmpty(_serviceVersion)){
				_serviceVersion = "v1";
			}
			return _serviceVersion;
		}
		set{
			_serviceVersion = value;
		}
	}

	#endregion

	#region Service Related URL and Parameters

	public string urlSynthesize{
		get{
			return getServiceFunctionURL("synthesize");
		}
	}

	public WatsonTextToSpeech textToSpeech{
		get{
			return null;
		}
	}

	#endregion

	#region Watson Service Text to Speech Singleton object
	private static WSTextToSpeech _instance;
	public static WSTextToSpeech Instance{
		get{
			if(_instance == null){
				_instance = WConfiguration.gameObjectWatson.AddComponent<WSTextToSpeech>();
			}
			return _instance;
		}
	}
	#endregion

	#region Text To Speech Functionality - StartSpeaking , ServiceRequest to Bluemix

	public static void StartSpeaking(string textToSpeech, WatsonTextToSpeech.VoiceType voice = WatsonTextToSpeech.VoiceType.en_US_Michael, WatsonTextToSpeech.AudioFormatType audioFormat = WatsonTextToSpeech.AudioFormatType.wav){
		WatsonTextToSpeech watsonTextToSpeech = new WatsonTextToSpeech(textToSpeech, voice, audioFormat);
		Instance.StartSpeaking(watsonTextToSpeech, Instance.AsyncTextToSpeechRequestResult);
	}

	private void StartSpeaking(WatsonTextToSpeech watsonTextToSpeech, Action < WatsonTextToSpeech, TimeSpan> callback)
	{
		string urlTextToSpeechServiceGETRequest = getGETRequestUrl(urlSynthesize, watsonTextToSpeech.parameterAcceptedAudio, watsonTextToSpeech.parameterVoice, watsonTextToSpeech.parameterText);
		
		WWWForm form = new WWWForm();
		Dictionary<string, string> headers = form.headers.AddAuthorizationHeader(serviceCredentialUserName, serviceCredentialPassword);
		WWW uri = new WWW (urlTextToSpeechServiceGETRequest, null, headers );

		Debug.Log("Bluemix Request: " + watsonTextToSpeech.ToString());
		StartCoroutine(AsyncTextToSpeechRequestToBluemix(uri, watsonTextToSpeech, callback));
	}

	private IEnumerator AsyncTextToSpeechRequestToBluemix(WWW wwwRequestToBluemixForTextToSpeech, WatsonTextToSpeech watsonTextToSpeech,  Action < WatsonTextToSpeech, TimeSpan> callback)
	{
		DateTime timeRequestStart = System.DateTime.Now;
		yield return wwwRequestToBluemixForTextToSpeech;
		TimeSpan timeRequestElapsed = System.DateTime.Now.Subtract(timeRequestStart);

		if (wwwRequestToBluemixForTextToSpeech.error == null && wwwRequestToBluemixForTextToSpeech.isDone) 
		{
			if(watsonTextToSpeech.audioFormat == WatsonTextToSpeech.AudioFormatType.wav){
				PlayAudioWAV(wwwRequestToBluemixForTextToSpeech.bytes);
			}
			else{
				//TODO: add all supported file playing audio files!
				Debug.LogError("File type : " + watsonTextToSpeech.audioFormatString + " is not supported!" );
			}

			if(callback != null){
				callback.Invoke(watsonTextToSpeech, timeRequestElapsed);
			}

		} 
		else 
		{
			Debug.Log ("Error: " + wwwRequestToBluemixForTextToSpeech.error + " - url: " + wwwRequestToBluemixForTextToSpeech.url);
		}
		
		wwwRequestToBluemixForTextToSpeech.Dispose();
	}

	private void PlayAudioWAV(byte[] audio){
		WAudioWAV wav = new WAudioWAV(audio);
		AudioClip audioClip = AudioClip.Create("WatsonTextToSpeech_AudioClip", wav.sampleCount, 1,wav.frequency, false);
		audioClip.SetData(wav.leftChannel, 0);	//We are using left channel - as 2D sound
		
		AudioSource attachedAudioSource = this.transform.GetComponent<AudioSource>();
		if(attachedAudioSource == null){
			attachedAudioSource = this.gameObject.AddComponent<AudioSource>();
		}
		
		attachedAudioSource.spatialBlend = 0.0f; //2D Sound
		attachedAudioSource.PlayOneShot(audioClip);
		attachedAudioSource.Play();
	}

	private void PlayAudioOGG(byte[] audio){

	}

	private void PlayAudioAFF(byte[] audio){
		
	}

	private void AsyncTextToSpeechRequestResult(WatsonTextToSpeech watsonTextToSpeech, TimeSpan timeRequestElapsed){
		Debug.Log(string.Format( "Bluemix Response: {0} - Request time: {1}:{2} ",  watsonTextToSpeech.ToString(), timeRequestElapsed.Seconds, timeRequestElapsed.Milliseconds));
	}

	#endregion

	
}
