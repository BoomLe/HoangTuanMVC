using App.Models;

namespace App.Services
{
    public class ProductService :List<ProductModel>
    {
        public ProductService()
        {
            this.AddRange(new ProductModel[]
            {
                new ProductModel(){Id =1, Name= "IphoneX", Price=200},
                 new ProductModel(){Id =2, Name= "Iphone11", Price=400},
                  new ProductModel(){Id =3, Name= "Iphone12", Price=600},
                   new ProductModel(){Id =4, Name= "Iphone13", Price=800},
                    new ProductModel(){Id =5, Name= "Iphone14", Price=1000},
            });
        }

    }
    
}