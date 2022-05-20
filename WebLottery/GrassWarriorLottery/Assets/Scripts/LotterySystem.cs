using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace KID
{
    /// <summary>
    /// ����t��
    /// </summary>
    public class LotterySystem : MonoBehaviour
    {
        [SerializeField, Header("�H������")]
        private AudioClip soundRandom;
        [SerializeField, Header("��������")]
        private AudioClip soundGetGift;

        /// <summary>
        /// ������s
        /// </summary>
        private Button btnLottery;
        /// <summary>
        /// ���s������s
        /// </summary>
        private Button btnLotteryReplay;
        /// <summary>
        /// ������e
        /// </summary>
        private TextMeshProUGUI tmpLotteryContent;
        /// <summary>
        /// �������
        /// </summary>
        private CanvasGroup groupLottery;
        /// <summary>
        /// �������
        /// </summary>
        private TextMeshProUGUI tmpLotteryTip;

        private AudioSource aud;

        private float volumeRandom { get => Random.Range(0.9f, 1.2f); }

        private string[] lotteryContent =
        {
            "�f�n * 1",
            "�f�n * 2",
            "�f�n * 3",
            "�f�n * 4",
            "�f�n * 5",
            "�ݳ�����",
            "�M�a"
        };

        private float[] probiblity =
        {
            0.8f,
            0.1f,
            0.05f,
            0.02f,
            0.01f,
            0.01f,
            0.01f
        };

        private bool getGift;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();

            btnLottery = GameObject.Find("������s").GetComponent<Button>();
            btnLotteryReplay = GameObject.Find("���s������s").GetComponent<Button>();
            tmpLotteryContent = GameObject.Find("������e").GetComponent<TextMeshProUGUI>();
            groupLottery = GameObject.Find("�������").GetComponent<CanvasGroup>();
            tmpLotteryTip = GameObject.Find("�������").GetComponent<TextMeshProUGUI>();

            btnLottery.onClick.AddListener(() => { StartCoroutine(Lottery()); });
            btnLotteryReplay.onClick.AddListener(() => { StartCoroutine(LotteryReplay()); });
        }

        private IEnumerator Lottery()
        {
            getGift = false;

            groupLottery.interactable = true;
            groupLottery.blocksRaycasts = true;

            for (int i = 0; i < 10; i++)
            {
                groupLottery.alpha += 0.1f;
                yield return new WaitForSeconds(0.0f);
            }

            for (int i = 0; i < 20; i++)
            {
                tmpLotteryContent.text = lotteryContent[i % lotteryContent.Length];
                aud.PlayOneShot(soundRandom, volumeRandom);
                yield return new WaitForSeconds(0.1f);
            }

            while (!getGift)
            {
                for (int i = 0; i < lotteryContent.Length; i++)
                {
                    tmpLotteryContent.text = lotteryContent[i];

                    float random = Random.value;
                    aud.PlayOneShot(soundRandom, volumeRandom);

                    if (random <= probiblity[i])
                    {
                        getGift = true;
                        btnLotteryReplay.interactable = true;
                        tmpLotteryTip.enabled = true;
                        print(random);
                        aud.PlayOneShot(soundGetGift, 1.3f);
                        StopAllCoroutines();
                    }

                    yield return new WaitForSeconds(0.1f);
                }
            }
        }

        private IEnumerator LotteryReplay()
        {
            btnLotteryReplay.interactable = false;
            tmpLotteryTip.enabled = false;
            getGift = false;

            groupLottery.interactable = false;
            groupLottery.blocksRaycasts = false;

            for (int i = 0; i < 10; i++)
            {
                groupLottery.alpha -= 0.1f;
                yield return new WaitForSeconds(0.0f);
            }
        }
    }
}

