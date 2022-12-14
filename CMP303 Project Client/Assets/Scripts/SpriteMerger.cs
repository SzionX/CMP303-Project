using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMerger : MonoBehaviour
{
    //initialize sprites
    [SerializeField] private Sprite[] spritesToMerge = null;
    [SerializeField] private SpriteRenderer endSpriteRenderer = null;

    private void Start()
    {
        Merge();
    }

    //sprite merging function
    private void Merge()
    {
        Resources.UnloadUnusedAssets();
        var newTexture = new Texture2D(256, 256);

        //for loops for background transparency
        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                newTexture.SetPixel(x, y, new Color(1, 1, 1, 0));
            }
        }

        //for loops for sprite pixels
        for (int i = 0; i < spritesToMerge.Length; i++)
        {
            for (int x = 0; x < spritesToMerge[i].texture.width; x++)
            {
                for (int y = 0; y < spritesToMerge[i].texture.height; y++)
                {
                    var color = spritesToMerge[i].texture.GetPixel(x, y).a == 0 ?
                        newTexture.GetPixel(x, y) :
                        spritesToMerge[i].texture.GetPixel(x, y);

                    newTexture.SetPixel(x, y, color);
                }
            }
        }

        newTexture.Apply();
        var endSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f));
        endSprite.name = "New Sprite";
        endSpriteRenderer.sprite = endSprite;
    }
}
