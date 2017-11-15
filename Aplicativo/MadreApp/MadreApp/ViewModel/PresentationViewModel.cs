using MadreApp.Helpers;
using MadreApp.Pages;

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace MadreApp.ViewModel
{
    public class PresentationViewModel : BaseViewModel
    {
        private int _position;
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (value == _position) return;
                SetProperty(ref _position, value);
            }
        }

        private IList<DataTemplate> _pages;
        public IList<DataTemplate> Pages
        {
            get { return _pages; }
            set
            {
                if (_pages == value) return;
                SetProperty(ref _pages, value);
            }
        }

        public PresentationViewModel()
        {
            _pages = new List<DataTemplate>
            {
                new DataTemplate(() => {return new PresentationTemplateView("doctor1", "Consultas", "Encontre o seu médico" + Environment.NewLine + "com apenas um toque"); }),
                new DataTemplate(() => {return new PresentationTemplateView("doctor2", "Agendamentos", "Agende sua consulta" + Environment.NewLine + "com no máximo 10 dias"); }),
                new DataTemplate(() => {return new PresentationTemplateView("map", "Rede", "Conheça a maior rede" + Environment.NewLine + "de saúde do Brasil"); }),
                new DataTemplate(() => {return new PresentationLoginView(); })
            };
        }
    }
}