using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ZwinnyCRUD.Mobile.Models;
using Xamarin.Forms;

namespace ZwinnyCRUD.Mobile.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        public int Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            var intId = Int32.Parse(itemId);
            try
            {
                var item = await DataStore.GetItemAsync(intId);
                Id = item.Id;
                Text = item.Title;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
