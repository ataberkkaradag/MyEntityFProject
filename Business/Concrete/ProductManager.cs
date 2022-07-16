using Business.Abstract;
using Entities.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.DTOs;
using Core.Utilities.Results;
using Business.Constants;
using FluentValidation;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
           IResult result=BusinessRules.Run(CheckIfProductNameExist(product.ProductName),CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckIfCategoryLimit());

            if (result!=null)
            {
               return result;

            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);


        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour==01)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintanceTime);

            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(p=>p.CategoryId==id));
        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId==id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice <= max && p.UnitPrice >= min));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        private IResult CheckIfProductNameExist(string productName)
        {
            var result=_productDal.GetAll(p=>p.ProductName==productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);

            }
            return new SuccessResult();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count();
            if (result>=10)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);

            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimit()
        {
            var categoryCount = _categoryService.GetAll();
            if (categoryCount.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
