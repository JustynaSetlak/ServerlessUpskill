using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using UpskillStore.Product.HttpRequests;

namespace UpskillStore.Product.Validators
{
    public class ListAllProductsHttpRequestValidator : AbstractValidator<SearchProductsHttpRequest>
    {
        private IReadOnlyList<int> possibleNumberOfElementsOnPage = new List<int> { 10, 20, 30 }.AsReadOnly();
        public ListAllProductsHttpRequestValidator()
        {
            RuleFor(x => x.PageNumber).NotEqual(0);

            RuleFor(x => x.NumberOfElementsOnPage)
                .Must(x => possibleNumberOfElementsOnPage.Contains(x))
                .WithMessage("Please only use: " + string.Join(",", possibleNumberOfElementsOnPage));
        }
    }
}
