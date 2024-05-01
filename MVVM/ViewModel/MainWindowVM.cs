using LLMChat.Core;
using LLMChat.MVVM.Model;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;

namespace LLMChat.ViewModel
{
    class MainWindowVM : ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        // Commandos
        public RelayCommand SendCommand { get; set; }

        private ContactModel _selectedContact;

        public ContactModel SelectedContact
        {
            get { return _selectedContact; }
            set 
            { 
                _selectedContact = value;
                OnPropertyChanged();
            }
        }


        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public MainWindowVM()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                    FirstMessage = false
                });

                Message = "";
            });

            Messages.Add(new MessageModel
            {
                Username = "Dolphin",
                UsernameColor = "#409AFF",
                ImageSource = "https://cdn-uploads.huggingface.co/production/uploads/63111b2d88942700629f5771/ldkN1J0WIDQwU4vutGYiD.png",
                Message = "Los delfines dominaran el mundo",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true
            });

            for (int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Dolphin",
                    UsernameColor = "#409AFF",
                    ImageSource = "https://cdn-uploads.huggingface.co/production/uploads/63111b2d88942700629f5771/ldkN1J0WIDQwU4vutGYiD.png",
                    Message = "Los delfines dominaran el mundo jaja",
                    Time = DateTime.Now,
                    IsNativeOrigin = false,
                    FirstMessage = false
                });
            }

            for (int i = 0; i < 4; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Solar",
                    UsernameColor = "#409AFF",
                    ImageSource = "https://www.hardware-corner.net/wp-content/uploads/llm/images/Solar-logo.jpg",
                    Message = "Evaporare toda el agua del planeta muajaja",
                    Time = DateTime.Now,
                    IsNativeOrigin = true,
                });
            }

            Messages.Add(new MessageModel
            {
                Username = "Solar",
                UsernameColor = "#409AFF",
                ImageSource = "https://www.hardware-corner.net/wp-content/uploads/llm/images/Solar-logo.jpg",
                Message = "Maldito delfin",
                Time = DateTime.Now,
                IsNativeOrigin = true,
            });

            for (int i = 0; i < 4; i++)
            {
                Contacts.Add(new ContactModel
                {
                    Username = $"Delfin {i}",
                    ImageSource = "https://cdn-uploads.huggingface.co/production/uploads/63111b2d88942700629f5771/ldkN1J0WIDQwU4vutGYiD.png",
                    Messages = Messages
                });
            }

        }
    }
}
