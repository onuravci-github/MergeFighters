using UnityEngine;
using UnityEngine.UI;

public class QualitySetting : MonoBehaviour
{
    public Image[] sprites;
    void OnEnable() {
        SpriteColorUpdate(QualitySettings.GetQualityLevel());
    }

    public void GoodQuality(){
        QualitySettings.SetQualityLevel(0);
        SpriteColorUpdate(0);
    }
    public void HighQuality(){
        QualitySettings.SetQualityLevel(1);
        SpriteColorUpdate(1);
    }

    public void UltraQuality() {
        QualitySettings.SetQualityLevel(2);
        SpriteColorUpdate(2);
    }
    public void SpriteColorUpdate(int numb){
        if(sprites.Length != 0){
            for (int i = 0; i < 3; i++) {
                if(i == numb){
                    sprites[i].color = Color.white;
                } 
                else  
                    sprites[i].color = new Color(1,1,1,0.5f);
            }
        }
    }
}
