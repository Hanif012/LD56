using UnityEngine;

[CreateAssetMenu(menuName = "LD56/EnemySO")]
public class EnemySO : ScriptableObject
{
    public Stats stats;
    public Assets assets;

    public void Death()
    {
        if (assets.audio.death.Length > 0)
        {
            int index = Random.Range(0, assets.audio.death.Length);
            AudioSource.PlayClipAtPoint(assets.audio.death[index], Vector3.zero);
        }
    }
    public void Attack()
    {
        if (assets.audio.attack.Length > 0)
        {
            int index = Random.Range(0, assets.audio.attack.Length);
            AudioSource.PlayClipAtPoint(assets.audio.attack[index], Vector3.zero);
        }
    }

    [System.Serializable]
    public class Stats
    {
        public float speed;
        public float health;
        public float damage;
    }

    [System.Serializable]
    public class Assets
    {
        public GameObject prefab;
        public Sprite sprite;
        public EnemyAudio audio;
    }
    [System.Serializable]
    public class EnemyAudio
    {
        public AudioClip[] attack;
        public AudioClip[] death;
    }
}