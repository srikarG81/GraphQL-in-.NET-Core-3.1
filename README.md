# GraphQL-in-.NET-Core-3.1
Avoid Overfetching or Underfetching in .net core APIs using GraphQl

### Scenario:
In this example, product store capability exposed to client over http and client can query either all attributes or subset of attributes belongs to a product. You can query the required details instead of retrieving entire object as shown below.![alt text](https://github.com/srikarG81/GraphQL-in-.NET-Core-3.1/blob/master/UIGraphQl.png "GraphQL query")

### Get Started:
Install following Nuget packages.
Install-Package GraphQL -Version 2.4.0
Install-Package GraphQL.Server.Transports.AspNetCore -V 3.4.0
Install-Package GraphQL.Server.Ui.Playground  -V 3.4.0

Define the productstore schema as follows:
``` C#
 public class ProductStoreSchema : Schema
    {
        public ProductStoreSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ProductStoreQuery>();
        }
    }
````
