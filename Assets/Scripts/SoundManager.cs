using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

}


public class SoundManager : MonoBehaviour
{

    static public SoundManager instance;



    #region singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    #endregion

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    private void Start()
    {
        playSoundName = new string[audioSourceEffects.Length]; //직접 오디오 소스를 집어넣지 않아도 숫자가 알아서 늘어남
    }



    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[i].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name; //효과음의 이름을 넣어줌(재생중인 음악과 일치시킴)
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다");
                return;
            }
            Debug.Log(_name + "사운드가 SoundManager에 등록되지 않음");
        }
    }


    public void StopAllSE()//모든 음악 정지
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)//특정한 음악만 정지
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                return;
            }
        }
        Debug.Log("재생 중인" + _name + " 사운드가 없습니다");
    }


}
