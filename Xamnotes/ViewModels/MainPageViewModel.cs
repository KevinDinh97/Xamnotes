using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamnotes.Models;

namespace Xamnotes.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        // Mit dieser URI wird von picsum ein zufaelliges Bild geholt
        private const string picURI = "https://picsum.photos/200?";
        // Kollektion wo die Notizen gespeichert werden
        public ObservableCollection<Note> AllNotes { get; set; }
        // Handler der benachrichtigt wenn sich Eigenschaften aendern
        public event PropertyChangedEventHandler PropertyChanged;

        // Der Text der eingegeben wird
        public string editorContent;
        public string EditorContent
        {
            get
            {
                return editorContent;
            }
            set
            {
                if (editorContent != value)
                {
                    editorContent = value;
                    OnPropertyChanged("editorContent");
                }
            }
        }

        // Die Notiz
        public Note Note
        {
            get => Note;
            set
            {
                if (Note != value)
                {
                    Note = value;
                    OnPropertyChanged("Note");
                }
            }
        }

        // Fuegt eine Notiz hinzu
        public Command SaveCommand { get; }
        // Entfernt eine Notiz
        public Command EraseCommand { get; }

        // Kopierte Funktion um einem zufaelligen String zu erzeugen
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }  

        public MainPageViewModel()
        {
            AllNotes = new ObservableCollection<Note>();

            // Entfernt Notiz deleteMe die als CommandParameter uebergeben wird
            EraseCommand = new Command<Note>((deleteMe) =>
            {
                AllNotes.Remove(deleteMe);
            });

            // Fuege Notiz hinzu wenn die Eingabe nicht leer ist
            SaveCommand = new Command(() =>
            {
               if (EditorContent != string.Empty)
               {
                   Note newNote = new Note(EditorContent, DateTime.Now, picURI  + RandomString(10, true));
                   AllNotes.Add(newNote);
               }  
            }, () =>
            {
               if (EditorContent == string.Empty)
               {
                   return false;
               }else
               {
                   return true;
               }
            });

        }
    }
}
