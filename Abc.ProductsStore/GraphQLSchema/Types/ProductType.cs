using Abc.ProductsStore.Data;
using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.ProductsStore.GraphQLSchema.Types
{
    public class ProductType :  ObjectGraphType<Product>
    {
        public ProductType(IProductStore productStore, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(t => t.Id).Description("Product Id.");
            Field(t => t.Name).Description("Product Name.");
            Field(t => t.Description);
            Field(t => t.Price);
            Field<ListGraphType<ChildProductType>>(
                "AssociatedProducts",
                resolve: context =>
                {
                    var loader =
                        dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, ChildProducts>(
                            "GetReviewsByProductId", productStore.GetChildren);
                    return loader.LoadAsync(context.Source.Id);
                });
        }
    }


    public class ChildProductType : ObjectGraphType<ChildProducts>
    {
        public ChildProductType()
        {
            Field(t => t.Id).Description("Child Product Id.");
            Field(t => t.Name).Description("Child Product Name.");
            Field(t => t.Description);
            Field(t => t.Price);
        }
    }
}
