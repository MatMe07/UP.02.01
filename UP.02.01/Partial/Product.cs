using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP._02._01
{
    public partial class Product
    {
        public string CoverPathImg => CoverPath == null ? "\\images\\picture.png" : $"\\images\\{CoverPath}";
        public bool IsDiscountBig => Count > 0 && Discount > 15;

        public bool IsProd => Count > 0;
        public decimal totalPrice => Prise - (Prise * Discount / 100);

        public string CategoryNameAndName => $"{ProductsCategory.Name}  |  {Name}";
        public bool IsDiscount => IsProd && Discount > 0;
    }
}
