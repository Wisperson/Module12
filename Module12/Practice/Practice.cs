using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module12.PracticeTask
{
    public class Practice
    {

        public static void Start()
        {
            ExampleClass exampleObject = new ExampleClass();

            // Подписываемся на событие изменения свойства
            exampleObject.PropertyChanged += HandlePropertyChange;

            // Изменяем свойство, чтобы вызвать событие
            exampleObject.Value = 42;
        }

        static void HandlePropertyChange(object sender, PropertyEventArgs e)
        {
            Console.WriteLine($"Свойство {e.PropertyName} объекта {sender.GetType().Name} было изменено.");
        }

        public class PropertyEventArgs : EventArgs
        {
            public string PropertyName { get; }

            public PropertyEventArgs(string propertyName)
            {
                PropertyName = propertyName;
            }
        }

        public interface IPropertyChanged
        {
            event PropertyEventHandler PropertyChanged;
        }

        public delegate void PropertyEventHandler(object sender, PropertyEventArgs e);

        public class ExampleClass : IPropertyChanged
        {
            private int _value;

            public int Value
            {
                get { return _value; }
                set
                {
                    if (_value != value)
                    {
                        _value = value;
                        OnPropertyChanged(nameof(Value));
                    }
                }
            }

            public event PropertyEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyEventArgs(propertyName));
            }
        }

    }
}
