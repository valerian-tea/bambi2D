using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Sprite fullHeart,
        halfHeart,
        emptyHeart;
    private Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartStatus(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                break;
        }
    }
}

public enum HeartStatus
{
    Full = 2,
    Half = 1,
    Empty = 0,
}
