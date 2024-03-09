using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CleanArchitecture.Application.Features.V1.Products.Queries.GetProducts
{
    public class GetProductsQuery : ICommand<Product>
    {
        /// <summary>
        /// ID product
        /// </summary>
        public int Id { get; set; }
    }
}
