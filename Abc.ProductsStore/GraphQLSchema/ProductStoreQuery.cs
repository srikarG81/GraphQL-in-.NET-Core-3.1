using Abc.ProductsStore.Data;
using Abc.ProductsStore.GraphQLSchema.Types;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.ProductsStore.GraphQLSchema
{
    public class ProductStoreQuery :  ObjectGraphType
    {
        public ProductStoreQuery(IProductStore productRepository)
        {
            Field<ListGraphType<ProductType>>(
               "products",
               resolve: context => productRepository.GetAllProducts()
           );

            Field<ProductType>(
                "product",
                 arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "Id", Description = "Product Id" }),
                 resolve: context =>
                 { 
                     var id= context.GetArgument<int>("id");
                     return productRepository.GetProductById(id);
                 }
            );

            Field<ListGraphType<ChildProductType>>(
             "childProducts",
             arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "Id" }),
             resolve: context =>
             {
                 var id = context.GetArgument<int>("id");
                 return productRepository.GetChildren(id);
             });
        }
    }
}
