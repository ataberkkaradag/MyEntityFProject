using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductRemoved = "Ürün Silindi";
        public static string ProductNameInvalid = "Ürün İsmi Geçersiz ";
        public static string MaintanceTime="Sistem Bakımda";
        public static string ProductsListed="Ürün listelendi";
        public static string ProductCountOfCategoryError = "Bir Kategoride en fazla 10 ürün olabilir";
        public static string ProductNameAlreadyExist = "Bu isimde bir ürün zaten var";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
    }
}
