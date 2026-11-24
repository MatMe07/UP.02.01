using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UP._02._01
{
    public partial class Product 
    {
        private string _coverPathImg = "\\images\\picture.png";
        public string CoverPathImg
        {
            get
            {
                return CoverPath == null ? _coverPathImg : CoverPath.StartsWith("C:") ? CoverPath : $"\\images\\{CoverPath}";
            }
            set
            {
                _coverPathImg = value;
            }
        }
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
