using System;
using _SDK.UI.Shop.SkinShop;

namespace _SDK.Observer.Message
{
    public class ItemSelectedMessage<T> where T : Enum
    {
        public SkinShopData<T> Data { get; private set; }
        public SkinShopItem Owner { get; private set; }
        
        public ItemSelectedMessage(SkinShopData<T> data, SkinShopItem owner)
        {
            Data = data;
            Owner = owner;
        }
    }
}