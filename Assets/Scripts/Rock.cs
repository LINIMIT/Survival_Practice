using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp;

    [SerializeField]
    private float destroyTime;

    [SerializeField]
    private SphereCollider col;

    [SerializeField]
    private GameObject go_rock;
    [SerializeField]
    private GameObject go_debris;
    [SerializeField]
    private GameObject go_effect_prefabs;

    /*  [SerializeField]
      private AudioSource audioSource;
      [SerializeField]
      private AudioClip effect_sound;
      [SerializeField]
      private AudioClip effect_sound2;*/


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

        SoundManager.instance.PlaySE(destroy_Sound);
        col.enabled = false;
        Destroy(go_rock);
        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }

}
