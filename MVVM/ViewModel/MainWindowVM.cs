using LLMChat.Core;
using LLMChat.MVVM.Model;
using System.Collections.ObjectModel;

namespace LLMChat.ViewModel
{
    class MainWindowVM : ObservableObject
    {
        public ObservableCollection<MessageModel> MessagesDolphin { get; set; }
        public ObservableCollection<MessageModel> MessagesSolar { get; set; }
        public ObservableCollection<MessageModel> MessagesAuto { get; set; }


        public ObservableCollection<ContactModel> Contacts { get; set; }
        private LLM _llm; 


        // Commandos
        public RelayCommand SendCommand { get; set; }
        private string modelMessage;

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
            MessagesDolphin = new ObservableCollection<MessageModel>();
            MessagesSolar = new ObservableCollection<MessageModel>();
            MessagesAuto = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            _llm = new LLM(); 

            SendCommand = new RelayCommand(async o =>
            {
                if (SelectedContact.Username == "Dolphin")
                {
                    LLM.BaseUrl = "http://localhost:5678/v1"; 

                    MessagesDolphin.Add(new MessageModel
                    {
                        Username = "Username",
                        UsernameColor = "#4DB31F",
                        ImageSource = "https://biblioteca.acropolis.org/wp-content/uploads/2014/12/verde.png",
                        Message = Message,
                        Time = DateTime.Now,
                        IsNativeOrigin = true
                    });

                    modelMessage = Message;
                    Message = "";

                    string response = await _llm.PostAPIAsync(modelMessage, "TheBloke/dolphin-2.6-mistral-7B-GGUF");

                    MessagesDolphin.Add(new MessageModel
                    {
                        Username = "Dolphin",
                        UsernameColor = "#409AFF",
                        ImageSource = "https://cdn-uploads.huggingface.co/production/uploads/63111b2d88942700629f5771/ldkN1J0WIDQwU4vutGYiD.png",
                        Message = response,
                        Time = DateTime.Now,
                        IsNativeOrigin = true
                    });
                }
                else if (SelectedContact.Username == "Solar")
                {
                    LLM.BaseUrl = "http://localhost:1234/v1";

                    MessagesSolar.Add(new MessageModel
                    {
                        Username = "Username",
                        UsernameColor = "#4DB31F",
                        ImageSource = "https://biblioteca.acropolis.org/wp-content/uploads/2014/12/verde.png",
                        Message = Message,
                        Time = DateTime.Now,
                        IsNativeOrigin = true
                    });

                    modelMessage = Message;
                    Message = "";

                    string response = await _llm.PostAPIAsync(modelMessage, "TheBloke/SOLAR-10.7B-Instruct-v1.0-uncensored-GGUF");

                    MessagesSolar.Add(new MessageModel
                    {
                        Username = "Solar",
                        UsernameColor = "#DFCC27",
                        ImageSource = "https://www.hardware-corner.net/wp-content/uploads/llm/images/Solar-logo.jpg",
                        Message = response,
                        Time = DateTime.Now,
                        IsNativeOrigin = true
                    });
                }
                else if (SelectedContact.Username == "Autochat")
                {
                    MessagesAuto.Add(new MessageModel
                    {
                        Username = "Username",
                        UsernameColor = "#4DB31F",
                        ImageSource = "https://biblioteca.acropolis.org/wp-content/uploads/2014/12/verde.png",
                        Message = Message,
                        Time = DateTime.Now,
                        IsNativeOrigin = true
                    });

                    modelMessage = Message;
                    Message = "";

                    while(modelMessage != ".")
                    {
                        LLM.BaseUrl = "http://localhost:5678/v1";
                        string response = await _llm.PostAPIAsync(modelMessage, "TheBloke/dolphin-2.6-mistral-7B-GGUF");

                        MessagesAuto.Add(new MessageModel
                        {
                            Username = "Dolphin",
                            UsernameColor = "#409AFF",
                            ImageSource = "https://cdn-uploads.huggingface.co/production/uploads/63111b2d88942700629f5771/ldkN1J0WIDQwU4vutGYiD.png",
                            Message = response,
                            Time = DateTime.Now,
                            IsNativeOrigin = true
                        });

                        modelMessage = response;

                        LLM.BaseUrl = "http://localhost:1234/v1";
                        response = await _llm.PostAPIAsync(modelMessage, "TheBloke/SOLAR-10.7B-Instruct-v1.0-uncensored-GGUF");

                        MessagesAuto.Add(new MessageModel
                        {
                            Username = "Solar",
                            UsernameColor = "#DFCC27",
                            ImageSource = "https://www.hardware-corner.net/wp-content/uploads/llm/images/Solar-logo.jpg",
                            Message = response,
                            Time = DateTime.Now,
                            IsNativeOrigin = true
                        });
                    }
                    
                }

            });

            // Mensaje inicial Dolphin
            MessagesDolphin.Add(new MessageModel
            {
                Username = "Dolphin",
                UsernameColor = "#409AFF",
                ImageSource = "https://cdn-uploads.huggingface.co/production/uploads/63111b2d88942700629f5771/ldkN1J0WIDQwU4vutGYiD.png",
                Message = "Hola, ¿que tal estas?",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true
            });

            // Mensaje inicial Solar
            MessagesSolar.Add(new MessageModel
            {
                Username = "Solar",
                UsernameColor = "#DFCC27",
                ImageSource = "https://www.hardware-corner.net/wp-content/uploads/llm/images/Solar-logo.jpg",
                Message = "Soy el sol",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true
            });

            // Mensaje inicial Autochat
            MessagesAuto.Add(new MessageModel
            {
                Username = "Autochat",
                UsernameColor = "#63DF27",
                ImageSource = "https://getchat.app/assets/img/whatsapp-large.png",
                Message = "Escribe para iniciar.",
                Time = DateTime.Now,
                IsNativeOrigin = false,
                FirstMessage = true
            });

            Contacts.Add(new ContactModel
            {
                Username = "Dolphin",
                ImageSource = "https://cdn-uploads.huggingface.co/production/uploads/63111b2d88942700629f5771/ldkN1J0WIDQwU4vutGYiD.png",
                Messages = MessagesDolphin
            });

            Contacts.Add(new ContactModel
            {
                Username = "Solar",
                ImageSource = "https://www.hardware-corner.net/wp-content/uploads/llm/images/Solar-logo.jpg",
                Messages = MessagesSolar
            });
            
            Contacts.Add(new ContactModel
            {
                Username = "Autochat",
                ImageSource = "https://getchat.app/assets/img/whatsapp-large.png",
                Messages = MessagesAuto
            });

        }
    }
}
