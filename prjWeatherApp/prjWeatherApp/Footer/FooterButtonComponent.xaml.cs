using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prjWeatherApp.Footer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FooterButtonComponent : ContentView
    {

        // Define the bindable properties for the button
        public static readonly BindableProperty ButtonTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(FooterButtonComponent), string.Empty);
        public static readonly BindableProperty ButtonIconProperty = BindableProperty.Create(nameof(IconSource), typeof(string), typeof(FooterButtonComponent), string.Empty);
        public static readonly BindableProperty ActionCommandProperty = BindableProperty.Create(nameof(ActionCommand), typeof(ICommand), typeof(FooterButtonComponent), null);

        // Define the properties for the button
        public string LabelText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public string IconSource
        {
            get { return (string)GetValue(ButtonIconProperty); }
            set { SetValue(ButtonIconProperty, value); }
        }

        public ICommand ActionCommand
        {
            get { return (ICommand)GetValue(ActionCommandProperty); }
            set { SetValue(ActionCommandProperty, value); }
        }

        public FooterButtonComponent()
        {
            InitializeComponent();

            // Bind the properties to the UI elements
            lblIcon.SetBinding(Label.TextProperty, new Binding(nameof(LabelText), source: this));
            imgIcon.SetBinding(Image.SourceProperty, new Binding(nameof(IconSource), source: this));

            // Add a tap gesture recognizer to the button
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTapped;
        }

        private void OnTapped(Object sender, EventArgs e)
        {
            // Handle the event
            if (ActionCommand != null && ActionCommand.CanExecute(null))
            {
                ActionCommand.Execute(null);
            }
        }

        
    }
}