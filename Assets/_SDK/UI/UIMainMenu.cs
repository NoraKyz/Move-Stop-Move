using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Camera;
using _Game.Scripts.GamePlay.Input;
using _Game.Scripts.Level;
using _Game.Scripts.Sound;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using _SDK.UI.Shop.SkinShop;
using _SDK.UI.Shop.WeaponShop;

namespace _SDK.UI
{
    public class UIMainMenu : UICanvas
    {
        public override void Open()
        {
            base.Open();
            
            this.GetService<CameraFollower>().ChangeState(CameraFollower.State.MainMenu);
        }

        public void OnClickPlayBtn()
        {
            GameManager.ChangeState(GameState.GamePlay);
            
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<UIGamePlay>();
            this.GetService<InputManager>().GetInputEntity();
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
        
        public void OnClickWeaponShopBtn()
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIWeaponShop>();
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
        
        public void OnClickSkinShopBtn()
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UISkinShop>();
            this.GetService<CameraFollower>().ChangeState(CameraFollower.State.Shop);
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
    }
}
