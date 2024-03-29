﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp;

    [SerializeField]
    private float destroyTime; //파편 제거 시간

    [SerializeField]
    private SphereCollider col;

    [SerializeField]
    private GameObject go_rock; //일반 바위
    [SerializeField]
    private GameObject go_debris; // 깨진 바위
    [SerializeField]
    private GameObject go_effect_prefabs; // 채굴 이펙트
    [SerializeField]
    private GameObject  go_rock_item_prefab; // 돌멩이 아이템
                                      /*  [SerializeField]
                                        private AudioSource audioSource;
                                        [SerializeField]
                                        private AudioClip effect_sound;
                                        [SerializeField]
                                        private AudioClip effect_sound2;*/

    [SerializeField]
    private int count; //돌멩이 아이템 등장 개수

    /// <summary>
    /// 필요한 사운드 이름
    /// </summary>
    [SerializeField]
    private string strike_Sound;
    [SerializeField]
    private string destroy_Sound;

    public void Mining()
    {
        //  audioSource.clip = effect_sound;
        //  audioSource.Play();
        SoundManager.instance.PlaySE(strike_Sound);

        var clone = Instantiate(go_effect_prefabs, col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);
        hp--;
        if (hp <= 0)
        {
            Destruction();
        }
    }

    private void Destruction()
    {
        //     audioSource.clip = effect_sound2;
        //    audioSource.Play();
        col.enabled = false;

        for (int i = 0; i < count; i++)
        {
            Instantiate(go_rock_item_prefab, go_rock.transform.position , Quaternion.identity);
        }


        SoundManager.instance.PlaySE(destroy_Sound);
        Destroy(go_rock);
        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }

}
