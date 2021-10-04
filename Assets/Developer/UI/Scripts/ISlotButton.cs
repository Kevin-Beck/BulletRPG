using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlotButton
{
    public void SetToolTipTitleAndText(string title, string text);
    public void SetToolTipEnabled(bool enabled);
    public void SetCounterText(string count);
    public void SetIconSpriteAndColor(Sprite sprite, Color color);
}
