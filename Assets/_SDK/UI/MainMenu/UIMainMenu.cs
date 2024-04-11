using _Game.Scripts.GamePlay.Camera;
using _Game.Scripts.GamePlay.Input;
using _Game.Scripts.Setting.Sound;
using _SDK.UI.Base;
using _SDK.UI.Shop.SkinShop;
using _SDK.UI.Shop.WeaponShop;

namespace _SDK.UI.MainMenu
{
    public class UIMainMenu : UICanvas
    {
        public override void Open()
        {
            base.Open();

            CameraFollower.Ins.ChangeState(CameraFollower.State.MainMenu);
        }

        public void OnClickPlayBtn()
        {
            GameManager.ChangeState(GameState.GamePlay);
            
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<UIGamePlay>();
            InputManager.Ins.GetInputEntity();
            SoundManager.Ins.Play(SoundType.ClickButton);
        }
        
        public void OnClickWeaponShopBtn()
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIWeaponShop>();
            SoundManager.Ins.Play(SoundType.ClickButton);
        }
        
        public void OnClickSkinShopBtn()
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UISkinShop>();
            CameraFollower.Ins.ChangeState(CameraFollower.State.Shop);
            SoundManager.Ins.Play(SoundType.ClickButton);
        }
    }
}
