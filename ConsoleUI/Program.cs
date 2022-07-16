using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

ProductManager productManager=new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));  


foreach ( var product in productManager.GetAllByCategoryId(2).Data)
{
    Console.WriteLine(product.ProductName);
}


Console.WriteLine("------------------------------------");

CategoryManager categoryManager=new CategoryManager(new EfCategoryDal());

foreach (var category in categoryManager.GetAll().Data)
{
    Console.WriteLine(category.CategoryName);

}

Console.WriteLine("------------------------------------");

var result = productManager.GetProductDetails();
if (result.Success)
{
    foreach (var product in result.Data)
    {
        Console.WriteLine(product.ProductName + "/" + product.CategoryName);

    }

}
else
{
    Console.WriteLine(result.Message);
}
