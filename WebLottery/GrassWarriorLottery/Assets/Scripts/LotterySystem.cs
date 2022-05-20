using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace KID
{
    /// <summary>
    /// 抽獎系統
    /// </summary>
    public class LotterySystem : MonoBehaviour
    {
        [SerializeField, Header("隨機音效")]
        private AudioClip soundRandom;
        [SerializeField, Header("中獎音效")]
        private AudioClip soundGetGift;

        /// <summary>
        /// 抽獎按鈕
        /// </summary>
        private Button btnLottery;
        /// <summary>
        /// 重新抽獎按鈕
        /// </summary>
        private Button btnLotteryReplay;
        /// <summary>
        /// 抽獎內容
        /// </summary>
        private TextMeshProUGUI tmpLotteryContent;
        /// <summary>
        /// 抽獎介面
        /// </summary>
        private CanvasGroup groupLottery;
        /// <summary>
        /// 抽獎提示
        /// </summary>
        private TextMeshProUGUI tmpLotteryTip;

        private AudioSource aud;

        private float volumeRandom { get => Random.Range(0.9f, 1.2f); }

        private string[] lotteryContent =
        {
            "口罩 * 1",
            "口罩 * 2",
            "口罩 * 3",
            "口罩 * 4",
            "口罩 * 5",
            "胸章任選",
            "杯帶"
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

            btnLottery = GameObject.Find("抽獎按鈕").GetComponent<Button>();
            btnLotteryReplay = GameObject.Find("重新抽獎按鈕").GetComponent<Button>();
            tmpLotteryContent = GameObject.Find("抽獎內容").GetComponent<TextMeshProUGUI>();
            groupLottery = GameObject.Find("抽獎介面").GetComponent<CanvasGroup>();
            tmpLotteryTip = GameObject.Find("抽獎提示").GetComponent<TextMeshProUGUI>();

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

