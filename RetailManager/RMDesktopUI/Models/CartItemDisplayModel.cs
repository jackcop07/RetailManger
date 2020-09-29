using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RMDesktopUI.Annotations;


namespace RMDesktopUI.Models
{
    public class CartItemDisplayModel : INotifyPropertyChanged
    {
        public ProductDisplayModel Product { get; set; }

        private int _quantityInCart;

        public int QuantityInCart
        {
            get { return _quantityInCart; }
            set
            {
                _quantityInCart = value;
                CallPropertyChanged("QuantityInCart");
                CallPropertyChanged("DisplayText");
            }
        }

        public string DisplayText => $"{Product.ProductName} ({QuantityInCart})";
        public event PropertyChangedEventHandler PropertyChanged;


        public void CallPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
