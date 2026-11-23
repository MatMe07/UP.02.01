using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP._02._01
{
    public partial class Product 
    {
        public string CoverPathImg => CoverPath == null ? "\\images\\picture.png" : $"\\images\\{CoverPath}";
        public bool IsDiscountBig => Count > 0 && Discount > 15;

        private bool _isActive = true;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
            }
        }
        public bool IsProd => Count > 0;
        private decimal _totalPrice;

        public decimal totalPrice {
            get
            {
                if (IsDiscount)
                {
                    _totalPrice = Prise - (Prise * Discount / 100);
                }
                return _totalPrice;
            }
            set
            {
                _totalPrice = value ;
            }
        }

        public string CategoryNameAndName => $"{ProductsCategory?.Name}  |  {Name}";
        public bool IsDiscount => IsProd && Discount > 0;
    }
}
