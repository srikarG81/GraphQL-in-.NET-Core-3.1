using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.ProductsStore.GraphQLSchema
{
    public class ProductStoreSchema : Schema
    {
        public ProductStoreSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ProductStoreQuery>();
        }
    }
}
